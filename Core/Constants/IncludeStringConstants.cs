using System.Collections.Generic;

namespace Core.Constants
{
    public static class IncludeStringConstants
    {
        public static string[] UserRolePermissionIncludeArray = new string[] { "Roles.Role.PermissionCategory.PermissionCategoryPermission.Category", "Roles.Role.PermissionCategory.PermissionCategoryPermission.Permission", "DirectivePermissions.Permission" };
        //public static string[] RolePermissionIncludeArray = new string[] { "PermissionCategory.PermissionCategoryPermission.Category", "PermissionCategory.PermissionCategoryPermission.Permission" };

        public static List<string> RolePermissionIncludeList = new List<string>()
        {
            "PermissionCategory.PermissionCategoryPermission.Category",
                "PermissionCategory.PermissionCategoryPermission.Permission"
        };
    }
}
