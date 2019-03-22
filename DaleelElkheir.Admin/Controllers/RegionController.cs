using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Regions;
using DaleelElkheir.BLL.Services.BloodBanks;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.BLL.Services.Organizations;
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
    public class RegionController : Controller
    {
        private readonly IRegionService regionService;
        private readonly IOrganizationService organizationService;
        private readonly IEventService eventService;
        private readonly ICaseService caseService;
        private readonly IBloodBankService bloodBankService;
        public RegionController(IRegionService _regionService, IOrganizationService _organizationService, IEventService _eventService, ICaseService _caseService, IBloodBankService _bloodBankService)
        {
            this.regionService = _regionService;
            this.organizationService = _organizationService;
            this.eventService = _eventService;
            this.caseService = _caseService;
            this.bloodBankService = _bloodBankService;
        }
        public ActionResult RegionList()
        {

            var Regions = regionService.GetRegions();
            return View(Regions);
        }

        [HttpGet]
        public ActionResult CreateRegion()
        {
            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;

            return View();
        }

        public ActionResult CreateRegion(RegionModel model)
        {
            if (ModelState.IsValid)
            {
                var _region = new City()
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    GovernorateID=model.GovernorateID
                };

                regionService.InsertRegion(_region);
                return RedirectToAction("RegionList");
            }
            return RedirectToAction("CreateRegion");
        }

        [HttpGet]
        public ActionResult UpdateRegion(int regionID)
        {
            IList<SelectListItem> CategoryList = new List<SelectListItem>();
            CategoryList.Add(new SelectListItem { Value = "School", Text = "School" });
            CategoryList.Add(new SelectListItem { Value = "Company", Text = "Company" });
            CategoryList.Add(new SelectListItem { Value = "University", Text = "University" });

            var _region = regionService.GetRegion(regionID);


            var regionModel = new RegionModel()
            {
                ID = _region.ID,
                NameEn = _region.NameEn,
                NameAr = _region.NameAr,
                GovernorateID=_region.GovernorateID
            };
            return View(regionModel);
        }

        public ActionResult UpdateRegion(RegionModel model)
        {
            var _region = new City()
            {
                ID = model.ID,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                GovernorateID=model.GovernorateID
            };
            regionService.UpdateRegion(_region);
            return RedirectToAction("RegionList");
        }

        public ActionResult DeleteRegion(int regionID)
        {
            var orgs = organizationService.GetOrganizations(x => x .CityID== regionID);
            var events = eventService.GetEvent(x => x.CityID == regionID);
            var cases = caseService.GetCases(x => x.CityID == regionID);
            var blood = bloodBankService.GetBloodBanks(x => x.CityID == regionID);

            if (orgs.Count > 0 || events.Count > 0|| cases.Count > 0 || blood.Count > 0 )
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                regionService.DeleteRegion(regionID);
                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("RegionList");
            } 
            
        }

        public ActionResult AreaList(int regionID)
        {
            var areas = regionService.GetAreas(regionID).ToList();
            ViewBag.RegionID = regionID;
            ViewBag.RegionName= regionService.GetRegion(regionID).NameEn;
            return View(areas.Select(s=>new AreaSimpleModel()
            {
                ID=s.ID,
                NameEn=s.NameEn,
                NameAr=s.NameAr,
                CityID=(Int32)s.CityID
            }));
        }

        public ActionResult CreateArea(int regionID)
        {
            return View(new AreaSimpleModel() { CityID = regionID });
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CreateArea(AreaSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                regionService.InsertArea(new Area()
                {
                    NameEn=model.NameEn,
                   NameAr=model.NameAr,
                   CityID=model.CityID
                });
                return RedirectToAction("AreaList", new { regionID = model.CityID });
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult EditArea(int areaID)
        {
            var area = regionService.GetArea(areaID);
            return View(new AreaSimpleModel()
            {
                ID=area.ID,
                NameEn=area.NameEn,
                NameAr=area.NameAr,
                CityID=(Int32)area.CityID
            });
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult EditArea(AreaSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                regionService.UpdateArea(new Area()
                {
                    ID=model.ID,
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    CityID = model.CityID
                });
                return RedirectToAction("AreaList", new { regionID = model.CityID });
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult DeleteArea(int areaID)
        {
            var orgs = organizationService.GetOrganizations(x => x.AreaID == areaID);
            var cityID = regionService.GetArea(areaID).CityID;
            if (orgs.Count > 0 )
            {
                ViewBag.Errors = "This record is already in use";
            }
            else
            {
                regionService.DeleteArea(areaID);
            }
            return RedirectToAction("AreaList", new { regionID = cityID });
        }
    }
}