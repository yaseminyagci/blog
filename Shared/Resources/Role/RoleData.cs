using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Constants;

namespace Shared.Resources.Role
{
    public class RoleData
    {

        public string Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(GeneralConstants.StringLengthLg)]
        public string Description { get; set; }
        public List<int> PermissionCategories { get; set; }

        public RoleData()
        {
            PermissionCategories = new List<int>();
        }
    }
}
