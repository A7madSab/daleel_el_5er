using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.About;
using DaleelElkheir.BLL.Services.AboutUs;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class AboutController : Controller
    {
        private readonly IAboutService aboutService;
        public AboutController(IAboutService _aboutService)
        {
            this.aboutService = _aboutService;    
        }

        [HttpGet]
        public ActionResult UpdateAbout()
        {
            var _about = aboutService.GetAbout().FirstOrDefault();
            var aboutModel = new AboutModel()
            {
                ID = _about.ID,
                Email = _about.Email,
                Mobile = _about.Mobile,
                ContactNumber = _about.ContactNumber,
                EmergencyNumber = _about.EmergencyNumber,
                FacebookCount=_about.FacebookCount,
                WebSite=_about.WebSite,
                BloodBankHelpsAcount=_about.BloodBankHelpsAcount,
                BriefEn = _about.BriefEn,
                BriefAr = _about.BriefAr,
                VisionEn=_about.VisionEn,
                VisionAr=_about.VisionAr,
                Address=_about.Address,
                Message=_about.Message
            };
            return View(aboutModel);
        }

        public ActionResult UpdateAbout(AboutModel model)
        {
            var _about = new About()
            {
                ID = model.ID,
                Email = model.Email,
                Mobile = model.Mobile,
                ContactNumber = model.ContactNumber,
                EmergencyNumber = model.EmergencyNumber,
                FacebookCount = model.FacebookCount,
                WebSite = model.WebSite,
                BloodBankHelpsAcount = model.BloodBankHelpsAcount,
                BriefEn = model.BriefEn,
                BriefAr = model.BriefAr,
                VisionEn = model.VisionEn,
                VisionAr = model.VisionAr,
                Address = model.Address,
                Message = model.Message
            };
            aboutService.UpdateAbout(_about);
            return RedirectToAction("Index","User");
        }

    }
}