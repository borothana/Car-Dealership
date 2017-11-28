using GuildCars.Datas;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class VehicleController : Controller
    {
        ICar _repo = CarFactory.Create();

        [HttpGet]
        public ActionResult List()
        {
            CarVM model = new CarVM();
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            CarVM model = new CarVM();
            model.SetMakeItems(_repo.GetMakeList());
            model.SetBodyStyleItems();
            model.SetColorItems();
            model.SetInteriorItems();
            model.SetTransmissionItems();
            model.SetTypeItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(CarVM model)
        {
            if (model.Type == "N" && model.Mileage > 1000)
            {
                ModelState.AddModelError("Mileage", "Mileage of new vehicle must be between 0 and 1000");
            }
            if (ModelState.IsValid)
            {
                var tmp = _repo.AddCar(model);
                if (tmp.Result.Success)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("Vehicle", "Cannot add vehicle");
                }
            }
            model.SetMakeItems(_repo.GetMakeList());
            model.SetBodyStyleItems();
            model.SetColorItems();
            model.SetInteriorItems();
            model.SetTransmissionItems();
            model.SetTypeItems();
            return View(model);           
        }

        [HttpGet]
        public ActionResult Edit()
        {
            string query = Request.QueryString["id"];
            int id = 0;
            int.TryParse(query, out id);
            CarVM model = _repo.GetCarVMById(id);
            model.SetMakeItems(_repo.GetMakeList());
            model.SetBodyStyleItems();
            model.SetColorItems();
            model.SetInteriorItems();
            model.SetTransmissionItems();
            model.SetTypeItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CarVM model, string Delete, string Save)
        {
            if (!string.IsNullOrEmpty(Delete))
            {
                _repo.DeleteCar(model.CarId);
                return RedirectToAction("List");
            }
            else
            {
                if (model.Type == "N" && model.Mileage > 1000)
                {
                    ModelState.AddModelError("Mileage", "Mileage of new vehicle must be between 0 and 1000");
                }
                if (ModelState.IsValid)
                {
                    var tmp = _repo.UpdateCar(model);
                    if (tmp.Result.Success)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ModelState.AddModelError("Vehicle", "Cannot edit vehicle");
                    }
                }
                model.SetMakeItems(_repo.GetMakeList());
                model.SetBodyStyleItems();
                model.SetColorItems();
                model.SetInteriorItems();
                model.SetTransmissionItems();
                model.SetTypeItems();
                return View(model);
            }            
        }
    }
}