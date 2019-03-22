using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Categories;
using DaleelElkheir.Admin.Models.Governorates;
using DaleelElkheir.BLL.Services.Regions;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class GovernorateController : Controller
    {
        private readonly IRegionService regionService;
        public GovernorateController(IRegionService _regionService)
        {
            this.regionService = _regionService;
        }
        public ActionResult GovernorateList()
        {

            var Governorates = regionService.GetGovernorates();
            return View(Governorates);
        }

        [HttpGet]
        public ActionResult CreateGovernorate()
        {

            return View();
        }

        public ActionResult CreateGovernorate(GovernorateModel model)
        {
            if (ModelState.IsValid)
            {
                var _governorates = new Governorate()
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                };

                regionService.InsertGovernorate(_governorates);
                return RedirectToAction("GovernorateList");
            }
            return RedirectToAction("CreateGovernorate");
        }

        [HttpGet]
        public ActionResult UpdateGovernorate(int governorateID)
        {
            var _governorate = regionService.GetGovernorate(governorateID);


            var governorateModel = new GovernorateModel()
            {
                ID = _governorate.ID,
                NameEn = _governorate.NameEn,
                NameAr = _governorate.NameAr,
            };
            return View(governorateModel);
        }

        public ActionResult UpdateGovernorate(GovernorateModel model)
        {
            var _governorate = new Governorate()
            {
                ID = model.ID,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
            };
            regionService.UpdateGovernorate(_governorate);
            return RedirectToAction("GovernorateList");
        }

        //public ActionResult DeleteGovernorate(int governorateID)
        //{
        //    regionService.DeleteGovernorate(governorateID);
        //    return RedirectToAction("GovernorateList");
        //}

        public ActionResult DeleteGovernorate(int governorateID)
        {
            var regions = regionService.GetRegions(x=>x.GovernorateID == governorateID);

            if (regions.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                regionService.DeleteGovernorate(governorateID);
                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
              //  return RedirectToAction("GovernorateList");
            }
        }
    }
}