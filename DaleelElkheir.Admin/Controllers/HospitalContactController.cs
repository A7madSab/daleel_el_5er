using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.BloodBanks;
using DaleelElkheir.Admin.Models.Hospitals;
using DaleelElkheir.BLL.Services.BloodBanks;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Hospitals;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class HospitalContactController : Controller
    {
        private readonly IHospitalService Hospital_Service;
        private readonly IFileDataService FileData_Service;
        public HospitalContactController(IHospitalService _HospitalService, IFileDataService _FileDataService)
        {
            this.Hospital_Service = _HospitalService;
            this.FileData_Service = _FileDataService;
        }
        public ActionResult HospitalContactList()
        {

            var bloodBankContacts = Hospital_Service.GetHospitalContacts();
            return View(bloodBankContacts);
        }

        [HttpGet]
        public ActionResult CreateHospitalContact()
        {
            IList<SelectListItem> HospitalList = Hospital_Service.GetHospitals().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            HospitalList.Insert(0, new SelectListItem { Text = "select Hospital", Value = "" });
            ViewBag.Hospitals = HospitalList;

            return View();
        }

        public ActionResult CreateHospitalContact(HospitalContactModel model)
        {
            if (ModelState.IsValid)
            {
              
                var BankContact = new HospitalContact();

                BankContact.ContactName = model.ContactName;
                BankContact.ContactNumber = model.ContactNumber;
                BankContact.HospitalID = model.HospitalID;

                 Hospital_Service.InsertHospitalContact(BankContact);
                return RedirectToAction("HospitalContactList");
            }
            return RedirectToAction("CreateHospitalContact");
        }

        [HttpGet]
        public ActionResult UpdateHospitalContact(int HospitalContactID)
        {
            IList<SelectListItem> HospitalList = Hospital_Service.GetHospitals().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            HospitalList.Insert(0, new SelectListItem { Text = "select Hospital", Value = "" });
            ViewBag.Hospitals = HospitalList;

            var _BankContact = Hospital_Service.GetHospitalContact(HospitalContactID);

            var BankContactModel = new HospitalContactModel()
            {
                ID = _BankContact.ID,
                ContactName = _BankContact.ContactName,
                ContactNumber = _BankContact.ContactNumber,
                HospitalID= _BankContact.HospitalID
            };
            return View(BankContactModel);
        }

        public ActionResult UpdateHospitalContact(HospitalContactModel model)
        {
        
            var BankContact = new HospitalContact()
            {
                ID = model.ID,
                ContactName = model.ContactName,
                ContactNumber = model.ContactNumber,
                HospitalID = model.HospitalID
            };
            Hospital_Service.UpdateHospitalContact(BankContact);
            return RedirectToAction("HospitalContactList");
        }

        public ActionResult DeleteHospitalContact(int HospitalContactID)
        {
            Hospital_Service.DeleteHospitalContact(HospitalContactID);
            return RedirectToAction("HospitalContactList");
        }
    }
}