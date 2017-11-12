using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GuildCars.Models.ViewModels
{
    public class ModelVM
    {
        public ModelVM()
        {
            MakeItems = new List<SelectListItem>();
        }

        public int ModelId { get; set; }
        public string Description { get; set; }
        public int MakeId { get; set; }
        public string AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; }
        public DateTime EditDate { get; set; }

        public Make Make { get; set; }
        public User AddUser { get; set; }
        public User EditUser { get; set; }

        public Response Result { get; set; }

        public List<SelectListItem> MakeItems { get; set; }

        public void SetMakeItems(IEnumerable<Make> makes)
        {
            foreach (var c in makes)
            {
                MakeItems.Add(new SelectListItem()
                {
                    Value = c.MakeId.ToString(),
                    Text = c.Description
                });
            }
        }
    }
}
