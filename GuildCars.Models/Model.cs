using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models
{
    public class Model
    {
        public int ModelId { get; set; }
        public string Description { get; set; }
        public int MakeId { get; set; }
        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }

        public Make Make { get; set; }
    }
}
