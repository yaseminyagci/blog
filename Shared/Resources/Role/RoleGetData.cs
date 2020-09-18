using System.Collections.Generic;
using Shared.Resources.Permission;
using Shared.Resources.PermissionCategory;
using Shared.Resources.User;

namespace Shared.Resources.Role
{
    public class RoleGetData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string Description { get; set; }
        public List<RolePermissionCategoryGetData> Permissions { get; set; }
        public List<UserGetData> Users { get; set; }
    }
}
