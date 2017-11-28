using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GuildCars.Models.ViewModels
{
    public class SaleReportVM
    {
        public User User { get; set; }
        public int QTY { get; set; }
        public decimal Amount { get; set; }
    }
}
