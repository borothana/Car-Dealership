using GuildCars.Models;
using GuildCars.Models.Interface;
using SCMS.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class StateController : Controller
    {
        ICar _repo = CarFactory.Create();

        [HttpGet]
        public ActionResult List()
        {
            List<State> model = _repo.GetStateList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new State());
        }

        [HttpPost]
        public ActionResult Add(State model)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddState(model))
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("State", "Cannot add state");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string stateAbbreviation)
        {
            State model = _repo.GetStateById(stateAbbreviation);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(State model)
        {
            if (ModelState.IsValid)
            {
                if (_repo.UpdateState(model))
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("State", "Cannot edit state");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(string stateAbbreviation)
        {
            State model = _repo.GetStateById(stateAbbreviation);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(State model)
        {
            if (_repo.DeleteState(model.StateAbbreviation))
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("State", "Cannot delete state");
                return View(model);
            }
        }
    }
}