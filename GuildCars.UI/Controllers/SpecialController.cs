using GuildCars.Models;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using GuildCars.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class SpecialController : Controller
    {
        ICar _repo = CarFactory.Create();

        [HttpGet]
        public ActionResult Show()
        {
            List<SpecialVM> model = _repo.GetSpecialVMList();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult List()
        {
            List<SpecialVM> model = _repo.GetSpecialVMList();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Add()
        {
            SpecialVM model = _repo.ConvertSpecialToVM(new Special());
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Add(SpecialVM model)
        {
            if (!String.IsNullOrEmpty(model.Description) && !String.IsNullOrEmpty(model.Title))
            {
                model = _repo.AddSpecial(model);
                if (model.Result.Success)
                {
                    return RedirectToAction("List");
                }
            }
            else if (String.IsNullOrEmpty(model.Title))
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Title is required";
            }
            else
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Description is required";
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int specialId)
        {
            SpecialVM model = _repo.ConvertSpecialToVM(_repo.GetSpecialById(specialId));
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(SpecialVM model)
        {
            if (!String.IsNullOrEmpty(model.Description) && !String.IsNullOrEmpty(model.Title))
            {
                model = _repo.UpdateSpecial(model);
                if (model.Result.Success)
                {
                    return RedirectToAction("List");
                }
            }
            else if (string.IsNullOrEmpty(model.Title))
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Title is required";
            }
            else
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Description is required";
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int specialId)
        {
            SpecialVM model = _repo.ConvertSpecialToVM(_repo.GetSpecialById(specialId));
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(SpecialVM model)
        {
            model.Result = _repo.DeleteSpecial(model);
            if (model.Result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }
    }
}