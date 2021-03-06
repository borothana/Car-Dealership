﻿using System;
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
        public string Type { get; set; }
        public string BodyStyle { get; set; }
        public int ReleaseYear { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public string Interior { get; set; }
        public int Mileage { get; set; }
        public string VinNo { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public bool IsFeature { get; set; }
        public byte[] Picture { get; set; }
        
        public Model model { get; set; }
        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }
    }
}
