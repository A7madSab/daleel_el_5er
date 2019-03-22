using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.BloodBanks;
using DaleelElkheir.Admin.Models.Hospitals;
using DaleelElkheir.BLL.Services.BloodBanks;
using DaleelElkheir.BLL.Services.Hospitals;
using DaleelElkheir.BLL.Services.Regions;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class HospitalController : Controller
    {
        private readonly IHospitalService hospitalService;
        private readonly IRegionService regionService;
        public HospitalController(IHospitalService _hospitalService, IRegionService _regionService)
        {
            this.hospitalService = _hospitalService;
            this.regionService = _regionService;
        }
        public ActionResult HospitalList()
        {

            var hospitals = hospitalService.GetHospitals();

            for (int i = 0; i < hospitals.Count(); i++)
            {
                hospitals[i].DescriptionAr = hospitals[i].DescriptionAr != null ? Regex.Replace(hospitals[i].DescriptionAr, @"<[^>]*>", "") : "";
                hospitals[i].DescriptionEn = hospitals[i].DescriptionEn != null ? Regex.Replace(hospitals[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(hospitals);
        }

        [HttpGet]
        public ActionResult CreateHospital()
        {
            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;

           // IList<SelectListItem> regionList = regionService.GetRegions().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
           // regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = "";


            return View();
        }

        public ActionResult CreateHospital(HospitalModel model)
        {
            if (ModelState.IsValid)
            {
                var hospital = new Hospital()
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    TitleEn = model.TitleEn,
                    TitleAr = model.TitleAr,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionAr = model.DescriptionAr,
                    CityID = model.CityID
                };

                hospitalService.InsertHospital(hospital);
                return RedirectToAction("HospitalList");
            }
            return RedirectToAction("CreateHospital");
        }

        [HttpGet]
        public ActionResult UpdateHospital(int hosID)
        {
            var bloodBank = hospitalService.GetHospital(hosID);

            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;


            IList<SelectListItem> regionList = regionService.GetRegions(x=>x.GovernorateID== bloodBank.City.GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = regionList;

            

            var bank = new HospitalModel()
            {
                ID=bloodBank.ID,
                NameEn = bloodBank.NameEn,
                NameAr = bloodBank.NameAr,
                TitleEn = bloodBank.TitleEn,
                TitleAr = bloodBank.TitleAr,
                DescriptionEn = bloodBank.DescriptionEn,
                DescriptionAr = bloodBank.DescriptionAr,
                CityID = bloodBank.CityID,
                GovernorateID= bloodBank.City.GovernorateID
            };
            return View(bank);
        }

        public ActionResult UpdateHospital(HospitalModel model)
        {
            var bank = new Hospital()
            {
                ID=model.ID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                TitleEn = model.TitleEn,
                TitleAr = model.TitleAr,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                CityID = model.CityID
            };
            hospitalService.UpdateHospital(bank);
            return RedirectToAction("HospitalList");
        }

        [HttpGet]
        public ActionResult GetRegions(int GovernorateID)
        {
            IList<SelectListItem> regionsList = regionService.GetRegions(x => x.GovernorateID == GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionsList.Insert(0, new SelectListItem { Text = "select Region", Value = "" });
            ViewBag.region = regionsList;
            return Json(regionsList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAreas(int CityID)
        {
            IList<SelectListItem> areaList = regionService.GetAreas(CityID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            areaList.Insert(0, new SelectListItem { Text = "select Area", Value = "" });
            ViewBag.area = areaList;
            return Json(areaList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteHospital(int hosID)
        {
            
            var bloodBank = hospitalService.GetHospitalContacts(x => x.HospitalID == hosID);

            if (bloodBank.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                hospitalService.DeleteHospital(hosID);
                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                //  return RedirectToAction("HospitalList");
            }
        }
    }
}