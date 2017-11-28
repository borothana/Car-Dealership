using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.ViewModels
{
    public class ContactVM
    {
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }

        public Response Result { get; set; }
    }
}
