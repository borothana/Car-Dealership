using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GuildCars.Models.ViewModels
{
    public class SpecialVM
    {
        public int SpecialId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Invalid Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/2000", "12/31/2050", ErrorMessage = "Date must be between 01/01/2000 and 12/31/2050")]
        public DateTime? FDate { get; set; }
        [Required(ErrorMessage = "Invalid End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/2000", "12/31/2050", ErrorMessage = "Date must be between 01/01/2000 and 12/31/2050")]
        public DateTime? TDate { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
        public Response Result { get; set; }
    }
}
