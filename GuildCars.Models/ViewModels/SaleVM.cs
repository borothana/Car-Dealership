using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GuildCars.Models.ViewModels
{
    public class SaleVM
    {
        public SaleVM()
        {
            StateItems = new List<SelectListItem>();
            PurchaseTypeItems = new List<SelectListItem>();
        }
        public int SaleId { get; set; }
        public int CarId { get; set; }
        //[Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Invalid phone no")]
        public string Phone { get; set; }
        //[RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        //[Required(ErrorMessage = "Zip code is Required")]
        //[RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip code")]
        public string ZipCode { get; set; }
        public string State { get; set; }
        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal PurchasePrice { get; set; }
        public string PurchaseType { get; set; }
        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }

        public User AddUser { get; set; }
        public Car car { get; set; }

        public List<SelectListItem> StateItems { get; set; }
        public List<SelectListItem> PurchaseTypeItems { get; set; }

        public void SetStateItems()
        {
            StateItems.Add(new SelectListItem() { Text = "AK", Value = "AK" });
            StateItems.Add(new SelectListItem() { Text = "MN", Value = "MN" });
        }

        public void SetPurchaseTypeItems()
        {
            PurchaseTypeItems.Add(new SelectListItem() { Text = "Bank Finance", Value = "B" });
            PurchaseTypeItems.Add(new SelectListItem() { Text = "Cash", Value = "C" });
            PurchaseTypeItems.Add(new SelectListItem() { Text = "Dealer Finance", Value = "D" });
        }
    }
}
