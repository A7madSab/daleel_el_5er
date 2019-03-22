using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.BloodBanks;
using DaleelElkheir.BLL.Services.BloodBanks;
using DaleelElkheir.BLL.Services.FilesData;
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
    public class BloodBankContactController : Controller
    {
        private readonly IBloodBankService BloodBank_Service;
        private readonly IFileDataService FileData_Service;
        public BloodBankContactController(IBloodBankService _BloodBankService, IFileDataService _FileDataService)
        {
            this.BloodBank_Service = _BloodBankService;
            this.FileData_Service = _FileDataService;
        }
        public ActionResult BloodBankContactList()
        {

            var bloodBankContacts = BloodBank_Service.GetBloodBankContacts();
            return View(bloodBankContacts);
        }

        [HttpGet]
        public ActionResult CreatebloodBankContact()
        {
            IList<SelectListItem> BloodBankList = BloodBank_Service.GetBloodBanks().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            BloodBankList.Insert(0, new SelectListItem { Text = "select Blood Bank", Value = "" });
            ViewBag.BloodBanks = BloodBankList;

            return View();
        }

        public ActionResult CreatebloodBankContact(BloodBankContactModel model)
        {
            if (ModelState.IsValid)
            {
              
                var BankContact = new BloodBankContact();

                BankContact.ContactName = model.ContactName;
                BankContact.ContactNumber = model.ContactNumber;
                BankContact.BloodBankID = model.BloodBankID;

                 BloodBank_Service.InsertBloodBankContact(BankContact);
                return RedirectToAction("BloodBankContactList");
            }
            return RedirectToAction("CreateBloodBankContact");
        }

        [HttpGet]
        public ActionResult UpdateBloodBankContact(int BankContactID)
        {
            IList<SelectListItem> BloodBankList = BloodBank_Service.GetBloodBanks().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            BloodBankList.Insert(0, new SelectListItem { Text = "select Blood Bank", Value = "" });
            ViewBag.BloodBanks = BloodBankList;

            var _BankContact = BloodBank_Service.GetBloodBankContact(BankContactID);

            var BankContactModel = new BloodBankContactModel()
            {
                ID = _BankContact.ID,
                ContactName = _BankContact.ContactName,
                ContactNumber = _BankContact.ContactNumber,
                BloodBankID= _BankContact.BloodBankID
            };
            return View(BankContactModel);
        }

        public ActionResult UpdateBloodBankContact(BloodBankContactModel model)
        {
        
            var BankContact = new BloodBankContact()
            {
                ID = model.ID,
                ContactName = model.ContactName,
                ContactNumber = model.ContactNumber,
                BloodBankID = model.BloodBankID
            };
            BloodBank_Service.UpdateBloodBankContact(BankContact);
            return RedirectToAction("BloodBankContactList");
        }

        public ActionResult DeleteBloodBankContact(int BankContactID)
        {
            BloodBank_Service.DeleteBloodBankContact(BankContactID);
            return RedirectToAction("BloodBankContactList");
        }
    }
}