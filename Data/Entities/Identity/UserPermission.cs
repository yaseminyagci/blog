using Core.BaseModels;
using Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Identity
{
    public class UserPermission : HelperModel, IEntity
    {
        [ForeignKey("Role")]
        [Required]
        public string UserId { get; set; }
        [ForeignKey("Permission")]
        public string PermissionId { get; set; }
        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}
