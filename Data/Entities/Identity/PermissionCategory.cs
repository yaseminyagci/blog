using Core.BaseModels;
using Core.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Identity
{
    public class PermissionCategory : HelperModel, IEntity
    {
        [Key]
        [Required]
        [StringLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string Label { get; set; }
        [StringLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string VisibleLabel { get; set; }
        [StringLength(512)]
        [Column(TypeName = "varchar(512)")]
        public string Description { get; set; }

        public ICollection<PermissionCategoryPermission> PossiblePermissions { get; set; }

        public PermissionCategory()
        {
            PossiblePermissions = new Collection<PermissionCategoryPermission>();
        }
    }
}
