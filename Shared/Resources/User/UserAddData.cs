using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Constants;

namespace Shared.Resources.User
{
    public class UserAddData : UserRegisterData
    {
        [Required]
        [StringLength(GeneralConstants.StringLengthXs)]
        public string FullName { get; set; }
       // public List<string> Roles { get; set; }
        public int Status { get; set; }

        public int UserTypeId { get; set; }
        public UserAddData()
        {
            //Roles = new List<string>();
        }

    }
}
