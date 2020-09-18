using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Shared;

namespace Application.Middlewares
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public readonly PermissionRequirementModel[] Permission;

        public PermissionRequirement(params PermissionRequirementModel[] permission)
        {
            Permission = permission;
        }
    }


    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserService _userService;

        public PermissionHandler(UserService userService)
        {

            _userService = userService;

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var user = await _userService.GetUserByNameAsync(context.User.Identity.Name,
                "Roles.Role.PermissionCategory.PermissionCategoryPermission.Permission", "DirectivePermissions.Permission").ConfigureAwait(false);
            if (user != null)
            {
                var userRole = new List<Role>();
                foreach (var c in user.Roles) userRole.Add(c.Role);

                var rolePermissions = userRole.SelectMany(c => c.PermissionCategory.Select(m => m.PermissionCategoryPermission)).ToList();

                var directivePermissions = user.DirectivePermissions.Select(c => c.Permission);


                if (
                    directivePermissions.Any(dp => requirement.Permission.Any(req => req.IsEqual(dp.Label)))
                    ||
                    rolePermissions.Any(rp =>
                        requirement.Permission.Any(req => req.IsEqual(rp.PermissionId, rp.CategoryId)))
                )
                {
                    context.Succeed(requirement);
                }
            }

            await Task.CompletedTask.ConfigureAwait(false);

        }
    }
}
