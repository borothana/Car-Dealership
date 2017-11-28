using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.ViewModels
{
    public class InventoryReportVM
    {
        public int ReleaseYear { get; set; }
        public Make CarMake { get; set; }
        public Model CarModel { get; set; }
        public int QTY { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }
}
