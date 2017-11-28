using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GuildCars.Models.ViewModels
{
    public class UserVM : IdentityUser
    {
        public UserVM()
        {
            RoleItems = new List<SelectListItem>();
        }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Passwords must be 8 characters")]
        public override string PasswordHash { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("PasswordHash", ErrorMessage = "Confirm password doesn't match")]
        public string ConfrimPassword { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public Response Result { get; set; }

        public List<SelectListItem> RoleItems { get; set; }

        public void GetRoleItems()
        {
            RoleItems.Add(new SelectListItem() { Value = "admin", Text = "Administrator" });
            RoleItems.Add(new SelectListItem() { Value = "sale", Text = "Sale" });
        }
    }
}
