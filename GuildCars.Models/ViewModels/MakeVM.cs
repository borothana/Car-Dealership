using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.ViewModels
{
    public class MakeVM
    {
        public int MakeId { get; set; }
        public string Description { get; set; }
        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }        
        public User AddUser { get; set; }
        public User EditUser { get; set; }

        public Response Result { get; set; }
    }
}
