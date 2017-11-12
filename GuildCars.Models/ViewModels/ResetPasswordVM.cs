using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.ViewModels
{
    public class ResetPasswordVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Passwords must be 8 characters")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Confirm password doesn't match")]
        public string NewPasswordRetype { get; set; }
        public string ReturnUrl { get; set; }

        public Response Result { get; set; }
    }
}
