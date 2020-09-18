using Shared.Resources.Permission;

namespace Shared.Resources.PermissionCategory
{
    public class RolePermissionCategoryGetData
    {
        public int RelationId { get; set; }
        public PermissionCategoryGetData Category { get; set; }
        public PermissionGetData Permission { get; set; }

    }
}
