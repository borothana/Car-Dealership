using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GuildCars.Models.ViewModels
{
    public class SaleReportVMPage
    {
        public SaleReportVMPage()
        {
            UserItems = new List<SelectListItem>();
        }

        public List<SaleReportVM> SaleReportVM { get; set; }
        public string UserId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/2000", "12/31/2050", ErrorMessage = "Date must be between 01/01/2000 and 12/31/2050")]
        public DateTime? FDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/2000", "12/31/2050", ErrorMessage = "Date must be between 01/01/2000 and 12/31/2050")]
        public DateTime? TDate { get; set; }

        public List<SelectListItem> UserItems { get; set; }

        public void SetUserItems(IEnumerable<User> users)
        {
            foreach (var c in users)
            {
                UserItems.Add(new SelectListItem()
                {
                    Value = c.Id,
                    Text = c.UserName
                });
            }
        }
    }
}
