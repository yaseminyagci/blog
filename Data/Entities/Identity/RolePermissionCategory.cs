using Core.BaseModels;
using Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Identity
{
    public class RolePermissionCategory : HelperModel, IEntity
    {
        [ForeignKey("PermissionCategoryPermission")]
        public int PermissionCategoryPermissionId { get; set; }
        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public PermissionCategoryPermission PermissionCategoryPermission { get; set; }
        public Role Role { get; set; }
    }
}
