using Core.BaseModels;
using Core.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Constants;

namespace Data.Entities.Identity
{
    public class PermissionCategoryPermission : HelperModel, IEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        [ForeignKey("Permission")]
        public string PermissionId { get; set; }
        [StringLength(GeneralConstants.StringLengthMd)]
        public string Description { get; set; }
        public PermissionCategory Category { get; set; }
        public Permission Permission { get; set; }
        public ICollection<RolePermissionCategory> Roles { get; set; } = new Collection<RolePermissionCategory>();

    }
}
