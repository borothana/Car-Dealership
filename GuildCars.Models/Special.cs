﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models
{
    public class Special
    {
        public int SpecialId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FDate { get; set; }
        public DateTime TDate { get; set; }
        public byte[] image { get; set; }
    }
}
