using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.BloodBanks;
using DaleelElkheir.BLL.Services.BloodBanks;
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
    public class BloodBankController : Controller
    {
        private readonly IBloodBankService bloodBankService;
        private readonly IRegionService regionService;
        public BloodBankController(IBloodBankService _bloodBankService, IRegionService _regionService)
        {
            this.bloodBankService = _bloodBankService;
            this.regionService = _regionService;
        }
        public ActionResult BloodBankList()
        {

            var bloodBanks = bloodBankService.GetBloodBanks();

            for (int i = 0; i < bloodBanks.Count(); i++)
            {
                bloodBanks[i].DescriptionAr = bloodBanks[i].DescriptionAr != null ? Regex.Replace(bloodBanks[i].DescriptionAr, @"<[^>]*>", "") : "";
                bloodBanks[i].DescriptionEn = bloodBanks[i].DescriptionEn != null ? Regex.Replace(bloodBanks[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(bloodBanks);
        }

        [HttpGet]
        public ActionResult CreateBloodBank()
        {
            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;

           // IList<SelectListItem> regionList = regionService.GetRegions().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
           // regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = "";


            return View();
        }

        public ActionResult CreateBloodBank(BloodBankModel model)
        {
            if (ModelState.IsValid)
            {
                var bank = new BloodBank()
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    TitleEn = model.TitleEn,
                    TitleAr = model.TitleAr,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionAr = model.DescriptionAr,
                    CityID = model.CityID
                };

                bloodBankService.InsertBloodBank(bank);
                return RedirectToAction("BloodBankList");
            }
            return RedirectToAction("CreateBloodBank");
        }

        [HttpGet]
        public ActionResult UpdateBloodBank(int bankID)
        {
            var bloodBank = bloodBankService.GetBloodBank(bankID);

            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;


            IList<SelectListItem> regionList = regionService.GetRegions(x=>x.GovernorateID== bloodBank.City.GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = regionList;

            

            var bank = new BloodBankModel()
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

        public ActionResult UpdateBloodBank(BloodBankModel model)
        {
            var bank = new BloodBank()
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
            bloodBankService.UpdateBloodBank(bank);
            return RedirectToAction("BloodBankList");
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

        public ActionResult DeleteBloodBank(int bankID)
        {
            
            var bloodBank = bloodBankService.GetBloodBankContacts(x => x.BloodBankID == bankID);

            if (bloodBank.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bloodBankService.DeleteBloodBank(bankID);
                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                //  return RedirectToAction("BloodBankList");
            }
        }
    }
}