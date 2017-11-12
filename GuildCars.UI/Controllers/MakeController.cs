using GuildCars.Models;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using SCMS.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class MakeController : Controller
    {
        ICar _repo = CarFactory.Create();

        [HttpGet]
        public ActionResult List()
        {
            List<MakeVM> model = _repo.GetMakeVMList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(_repo.ConvertMakeToVM(new Make()));
        }

        [HttpPost]
        public ActionResult Add(MakeVM model)
        {
            if (!string.IsNullOrEmpty(model.Description))
            {
                model.AddUserId = CurrentUser.User.Id;
                model.AddDate = DateTime.Now;
                model = _repo.AddMake(model);
                if (model.Result.Success)
                {
                    return RedirectToAction("List");
                }                
            }
            else
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Description is required";
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int makeId)
        {
            MakeVM model = _repo.ConvertMakeToVM(_repo.GetMakeById(makeId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MakeVM model)
        {
            if (!string.IsNullOrEmpty(model.Description))
            {
                model.EditUserId = CurrentUser.User.Id;
                model.EditDate = DateTime.Now;
                model = _repo.UpdateMake(model);
                if (model.Result.Success)
                {
                    return RedirectToAction("List");
                }
            }
            else
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Description is required";
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int makeId)
        {
            MakeVM model = _repo.ConvertMakeToVM(_repo.GetMakeById(makeId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(MakeVM model)
        {
            model.Result = _repo.DeleteMake(model);
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