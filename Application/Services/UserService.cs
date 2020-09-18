using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Process;

namespace Application.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<int> GetUserTypeId(string userId)
        {
            if (userId.IsNullOrEmpty())
                return 0;
            var user =await _userManager.FindByNameAsync(userId);

            return user.Detail.UserTypeId;
        }

        public async Task<string> GetAuthorizedUserIdAsync(IPrincipal principal)
        {
            var name = principal.Identity.Name;

            var user = await GetUserByNameAsync(name).ConfigureAwait(false);
            return user?.Id;
        }

        public Task<User> GetUserByNameAsync(string userName)
        {
            if (userName.IsNullOrEmpty())
                return Task.FromResult<User>(null);
            return _userManager.FindByNameAsync(userName);
        }
        public Task<User> GetUserByNameAsync(string userName, params Expression<Func<User, object>>[] includeProperties)
        {
            if (userName.IsNullOrEmpty())
                return Task.FromResult<User>(null);
            var query = _userManager.Users.IncludeAll(includeProperties);
            return query.FirstOrDefaultAsync(c => c.UserName == userName);
        }
        public Task<User> GetUserByNameAsync(string userName, params string[] includeProperties)
        {
            if (userName.IsNullOrEmpty())
                return Task.FromResult<User>(null);
            var query = _userManager.Users.IncludeAll(includeProperties);
            return query.FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public Task<User> GetUserByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<User> GetUserByIdAsync(string userId, params Expression<Func<User, object>>[] includeProperties)
        {
            var query = _userManager.Users.IncludeAll(includeProperties);
            return query.FirstOrDefaultAsync(c => c.Id == userId);
        }


        public Task<User> GetUserByIdAsync(string userId, params string[] includeProperties)
        {
            var query = _userManager.Users.IncludeAll(includeProperties);

            return query.FirstOrDefaultAsync(c => c.Id == userId);
        }

        public IQueryable<User> FindBy(Expression<Func<User, bool>> predicate)
        {
            return _userManager.Users.Where(predicate);
        }
        public IQueryable<User> FindBy(Expression<Func<User, bool>> predicate, params string[] includeProperties)
        {
            return _userManager.Users.IncludeAll(includeProperties).Where(predicate);
        }
        public IQueryable<User> FindBy(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeProperties)
        {
            return _userManager.Users.IncludeAll(includeProperties).Where(predicate);
        }
        public IQueryable<User> GetAll()
        {
            return _userManager.Users;
        }
        public IQueryable<User> GetAll(params string[] includeProperties)
        {
            return _userManager.Users.IncludeAll(includeProperties);
        }
        public IQueryable<User> GetAll(params Expression<Func<User, object>>[] includeProperties)
        {
            return _userManager.Users.IncludeAll(includeProperties);
        }



        public Task<bool> IsExistAsync(Expression<Func<User, bool>> predicate)
        {
            return _userManager.Users.AnyAsync(predicate);
        }
        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }
        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }
        public Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
        public Task<IdentityResult> DeleteAsync(User user)
        {
            return _userManager.DeleteAsync(user);
        }

        public async Task<bool> UserIsInRoleAsync(User user, string roleName)
        {
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            return roles.Any(s => s.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }

        public bool UserIsInPermission(User user, string permissionName)
        {
            var directivePermissions = user.DirectivePermissions.Select(c => c.Permission.Label).ToList();
            var userRole = user.Roles.Select(c => c.Role).ToList();
            var permissions = userRole.SelectMany(c => c.PermissionCategory.Select(e => e.PermissionCategoryPermission.Permission.Label)).ToList();

            return directivePermissions.Any(c => c.Equals(permissionName, StringComparison.OrdinalIgnoreCase)) ||
                   permissions.Any(c => c.Equals(permissionName, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<bool> UserIsInPermissionAsync(string userId, string permissionName)
        {
            var user = await GetUserByIdAsync(userId).ConfigureAwait(false);
            var directivePermissions = user.DirectivePermissions.Select(c => c.Permission.Label).ToList();
            var userRole = user.Roles.Select(c => c.Role).ToList();
            var permissions = userRole.SelectMany(c => c.PermissionCategory.Select(e => e.PermissionCategoryPermission.Permission.Label)).ToList();

            return directivePermissions.Any(c => c.Equals(permissionName, StringComparison.OrdinalIgnoreCase)) ||
                   permissions.Any(c => c.Equals(permissionName, StringComparison.OrdinalIgnoreCase));
        }
    }
}