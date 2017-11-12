using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int CarId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string ZipCode { get; set; }
        public string StateAbbreviation { get; set; }
        public decimal PurchasePrice { get; set; }
        public char PurchaseType { get; set; }
        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }

        public PostalCode PostalCode { get; set; }
        public State State { get; set; }
        public Car car { get; set; }
    }
}
