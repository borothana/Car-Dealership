using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.Models.ViewModels
{
    public class CarVM
    {
        public CarVM()
        {
            MakeItems = new List<SelectListItem>();
            ModelItems = new List<SelectListItem>();
            TypeItems = new List<SelectListItem>();
            BodyStyleItems = new List<SelectListItem>();
            TransmissionItems = new List<SelectListItem>();
            ColorItems = new List<SelectListItem>();
            InteriorItems = new List<SelectListItem>();
        }

        public int CarId { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public string Type { get; set; }
        public string BodyStyle { get; set; }
        [Range(minimum: 2000, maximum: 2050, ErrorMessage = "Year must be between 2000 and 2050")]
        public int ReleaseYear { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public string Interior { get; set; }
        public int Mileage { get; set; }
        [Required(ErrorMessage = "Vin no is require")]
        [RegularExpression("[A-HJ-NPR-Z0-9]{13}[0-9]{4}", ErrorMessage = "Invalid vin number")]
        public string VinNo { get; set; }
        [Required(ErrorMessage = "MSRP is require")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(0, Int32.MaxValue, ErrorMessage = "MSRP must greater than 0")]
        public decimal MSRP { get; set; }
        [Required(ErrorMessage = "Sale price is require")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Sale price must greater than 0")]
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public bool IsFeature { get; set; }
        public byte[] Picture { get; set; }
        public string PictureUrl { get; set; }
        public string PictureName { get; set; }
        public bool IsPurchase { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
        public Response Result { get; set; }

        public Model Model { get; set; }
        public Make Make { get; set; }
        public User AddUser { get; set; }
        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }

        public List<SelectListItem> MakeItems;
        public List<SelectListItem> ModelItems;
        public List<SelectListItem> TypeItems;
        public List<SelectListItem> BodyStyleItems;
        public List<SelectListItem> TransmissionItems;
        public List<SelectListItem> ColorItems;
        public List<SelectListItem> InteriorItems;

        public void SetMakeItems(IEnumerable<Make> makeList)
        {
            foreach (var m in makeList)
            {
                MakeItems.Add(new SelectListItem { Text = m.Description, Value = m.MakeId.ToString() });
            }
        }

        public void SetModelItems(IEnumerable<Model> modelList)
        {
            foreach (var m in modelList)
            {
                ModelItems.Add(new SelectListItem { Text = m.Description, Value = m.ModelId.ToString() });
            }
        }


        public void SetTypeItems()
        {
            TypeItems.Add(new SelectListItem { Text = "New", Value = "N" });
            TypeItems.Add(new SelectListItem { Text = "Used", Value = "U" });
        }


        public void SetBodyStyleItems()
        {
            BodyStyleItems.Add(new SelectListItem { Text = "Car", Value = "C" });
            BodyStyleItems.Add(new SelectListItem { Text = "SUV", Value = "S" });
            BodyStyleItems.Add(new SelectListItem { Text = "Truck", Value = "T" });
            BodyStyleItems.Add(new SelectListItem { Text = "Van", Value = "V" });
        }

        public void SetTransmissionItems()
        {
            TransmissionItems.Add(new SelectListItem { Text = "Automatic", Value = "A" });
            TransmissionItems.Add(new SelectListItem { Text = "Manual", Value = "M" });
        }

        public void SetColorItems()
        {
            ColorItems.Add(new SelectListItem { Text = "Red", Value = "RED" });
            ColorItems.Add(new SelectListItem { Text = "Blue", Value = "BLU" });
            ColorItems.Add(new SelectListItem { Text = "Black", Value = "BLK" });
            ColorItems.Add(new SelectListItem { Text = "White", Value = "WHT" });
        }

        public void SetInteriorItems()
        {
            InteriorItems.Add(new SelectListItem { Text = "Red", Value = "RED" });
            InteriorItems.Add(new SelectListItem { Text = "Blue", Value = "BLU" });
            InteriorItems.Add(new SelectListItem { Text = "Black", Value = "BLK" });
            InteriorItems.Add(new SelectListItem { Text = "White", Value = "WHT" });
        }
    }
}
