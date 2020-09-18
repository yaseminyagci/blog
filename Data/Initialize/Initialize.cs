using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities.Identity;
using Data.Initialize.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Initialize
{
    public static class Initialize
    {

        public static async Task SeedAsync(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            context.Database.EnsureCreated();


            await SeedPermissionAsync(context).ConfigureAwait(false);
            await SeedPermissionCategoryAsync(context).ConfigureAwait(false);

            await SeedUserDataAsync(userManager).ConfigureAwait(false);
            await SeedRoleDataAsync(roleManager).ConfigureAwait(false);
            //await seedFakeEventData(context).ConfigureAwait(false);

            await context.SaveChangesAsync().ConfigureAwait(false);

            await JoinUserRoleAsync(userManager, context).ConfigureAwait(false);
            await JoinRolePermissionAsync(roleManager, context).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        

        private static async Task SeedPermissionAsync(ApplicationDbContext context)
        {
            var data = InitializeData.BuildPermissionsList();
            var dbData = context.Permissions.ToList();
            var diffData = data.Where(c => dbData.All(e => e.Label != c.Label)).ToList();
            await context.Permissions.AddRangeAsync(diffData).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
        private static async Task SeedPermissionCategoryAsync(ApplicationDbContext context)
        {
            var data = InitializeData.BuildPermissionCategories(context);
            var dbData = context.PermissionCategories;
            var diffData = data.Where(c => dbData.All(e => e.Label != c.Label)).ToList();
            await context.PermissionCategories.AddRangeAsync(diffData).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private static async Task SeedUserDataAsync(UserManager<User> userManager)
        {
            var defaultPassword = "Aa12345!";

            var data = InitializeData.BuildUserList();
            var dbData = userManager.Users.ToList();
            var diffData = data.Where(c => dbData.All(e => e.UserName != c.UserName)).ToList();
            foreach (var user in diffData)
            {
                await userManager.CreateAsync(user, defaultPassword).ConfigureAwait(false);

            }
        }
        private static async Task SeedRoleDataAsync(RoleManager<Role> roleManager)
        {
            var data = InitializeData.BuildRoleList();
            var dbData = roleManager.Roles.ToList();
            var diffData = data.Where(c => dbData.All(e => e.Name != c.Name)).ToList();
            foreach (var role in diffData)
            {
                await roleManager.CreateAsync(role).ConfigureAwait(false);

            }
        }
        private static async Task JoinUserRoleAsync(UserManager<User> userManager, ApplicationDbContext context)
        {
            if (userManager.Users.Any() && !context.UserRoles.Any())
            {
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin").ConfigureAwait(false), "ADMIN").ConfigureAwait(false);
            }

        }
        

        private static async Task JoinRolePermissionAsync(RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            if (roleManager.Roles.Any() && !context.RolePermissionCategories.Any())
            {
                var adminRole = await roleManager.FindByNameAsync("ADMIN").ConfigureAwait(false);
                var allPermission = context.PermissionCategoryPermissions;
                foreach (var permission in allPermission)
                {
                    adminRole.PermissionCategory.Add(new RolePermissionCategory()
                    {
                        PermissionCategoryPermission = permission
                    });
                }
                //await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin").ConfigureAwait(false), "ADMIN").ConfigureAwait(false);
            }

        }
    }
}
