using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int ModelId { get; set; }
        public char Type { get; set; }
        public char BodyStyle { get; set; }
        public int ReleaseYear { get; set; }
        public char Transmision { get; set; }
        public string Color { get; set; }
        public string Interior { get; set; }
        public int Mileage { get; set; }
        public string VinNo { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }
    }
}
