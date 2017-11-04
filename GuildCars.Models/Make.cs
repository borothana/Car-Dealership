using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models
{
    public class Make
    {
        public int MakeId { get; set; }
        public string Description { get; set; }
        public string AddUser { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUser { get; set; }
        public DateTime EditDate { get; set; }
    }
}
