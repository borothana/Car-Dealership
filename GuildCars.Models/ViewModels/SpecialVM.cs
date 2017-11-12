using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.ViewModels
{
    public class SpecialVM
    {
        public int SpecialId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Response Result { get; set; }
    }
}
