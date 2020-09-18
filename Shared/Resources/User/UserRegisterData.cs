using Shared.Resources.UserType;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Resources.User
{
    public class UserRegisterData
    {
        [Required]
        //[EmailAddress]
        [RegularExpression(@"(([^<>()\[\]\\.,;:\s@""]+(\.[^<>()\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$",
            ErrorMessage = "Email is not in valid format.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{6,30}$",
            ErrorMessage = "Username is not in valid format.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,100}$",
        //    ErrorMessage = "Password is not in valid format.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

    }
}
