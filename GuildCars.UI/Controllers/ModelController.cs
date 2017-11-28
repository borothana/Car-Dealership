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
    [Authorize(Roles = "admin")]
    public class ModelController : Controller
    {
        ICar _repo = CarFactory.Create();

        [HttpGet]
        public ActionResult List()
        {
            List<ModelVM> model = _repo.GetModelVMList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ModelVM model = _repo.ConvertModelToVM(new Model());
            model.SetMakeItems(_repo.GetMakeList());
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ModelVM model)
        {
            if (!String.IsNullOrEmpty(model.Description) && model.MakeId > 0)
            { 
                model.AddUserId = CurrentUser.User.Id;
                model.AddDate = DateTime.Now;
                model = _repo.AddModel(model);
                if (model.Result.Success)
                {
                    return RedirectToAction("List");
                }
            }
            else if (String.IsNullOrEmpty(model.Description))
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Description is required";
            }
            else if (model.MakeId <= 0)
            {
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Please choose make";
            }

            model.SetMakeItems(_repo.GetMakeList());
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int modelId)
        {
            ModelVM model = _repo.ConvertModelToVM( _repo.GetModelById(modelId));
            model.SetMakeItems(_repo.GetMakeList());
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ModelVM model)
        {
            if (!String.IsNullOrEmpty(model.Description) && model.MakeId > 0)
            {
                model.EditUserId = CurrentUser.User.Id;
                model.EditDate = DateTime.Now;
                model = _repo.UpdateModel(model);
                if (model.Result.Success)
                {
                    return RedirectToAction("List");
                }
            }
            else if(String.IsNullOrEmpty(model.Description)){
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Description is required";
            }
            else if(model.MakeId <= 0){
                model.Result = _repo.ReturnSuccess();
                model.Result.Success = false;
                model.Result.ErrorMessage = "Please choose make";
            }
            model.Result.Success = false;
            model.SetMakeItems(_repo.GetMakeList());
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int modelId)
        {
            ModelVM model = _repo.ConvertModelToVM(_repo.GetModelById(modelId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ModelVM model)
        {
            model.Result = _repo.DeleteModel(model);
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