using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Constants;

namespace Shared.Resources.User
{
    public class UserEditData
    {

        [StringLength(128, ErrorMessage = "Id is not in valid format.")]
        public string Id { get; set; }

        [Required]
        [StringLength(GeneralConstants.StringLengthXs)]
        public string FullName { get; set; }

       // public List<string> Roles { get; set; }
       // public int Status { get; set; }

        public UserEditData()
        {
           // Roles = new List<string>();
        }
    }
}
