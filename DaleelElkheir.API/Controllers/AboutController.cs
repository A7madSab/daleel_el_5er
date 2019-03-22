using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.About;
using DaleelElkheir.BLL.Services.AboutUs;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DaleelElkheir.API.Controllers
{
    
    public class AboutController : ApiController
    {
        private readonly IAboutService aboutService;
        private readonly ICaseService CaseServices;
        private readonly IEventService EventService;
        private readonly IOrganizationService OrganizationService;

        public AboutController(IAboutService _aboutService, ICaseService _CaseServices, IEventService _EventService, IOrganizationService _OrganizationService)
        {
            this.aboutService = _aboutService;
            this.CaseServices = _CaseServices;
            this.EventService = _EventService;
            this.OrganizationService = _OrganizationService;
        }

        [HttpPost, AllowAnonymous]
        public IHttpActionResult GetAbout(BaseRequest request)
        {
            if(ModelState.IsValid)
            {
                var about = aboutService.GetAbout().FirstOrDefault();
                var companyRes = aboutService.getCompanyResponsibility();
                List<SocailResponsibilityModel> companyList = new List<SocailResponsibilityModel>();
                foreach(var com in companyRes)
                {
                    var comModel = new SocailResponsibilityModel {
                        Name = request.Lang == "ar" ? com.NameAr : com.NameEn,
                        Image = com.FileData != null ? com.FileData.Extenstion : null
                    };
                    companyList.Add(comModel);
                }

                if (about != null)
                {
                    var aboutModel = new AboutModel()
                    {
                        Beif = request.Lang == "ar" ? about.BriefAr : about.BriefEn,
                        Vision = request.Lang == "ar" ? about.VisionAr : about.VisionEn,
                        Message= about.Message,
                        CasesAcount= CaseServices.GetCases(x=>x.ConfirmationID==2).Count(),
                        EventAcount= EventService.GetEvent(x=>x.ConfirmationID==2).Count(),
                        OrganizationAcount=OrganizationService.GetOrganizations(OrgStatus.Approved).Count(),
                        BloodBankHelpCount= aboutService.GetAbout().FirstOrDefault().BloodBankHelpsAcount,
                        SocailResponsibilities = companyList
                    };
                    return Ok(new BaseResponse(aboutModel));
                }
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AllowAnonymous]
        public IHttpActionResult GetCharityInfo(BaseRequest request)
        {
            if (ModelState.IsValid)
            {
                var about = aboutService.GetAbout().FirstOrDefault();
                if (about != null)
                {
                    var aboutModel = new CharityInfoModel()
                    {
                        Email = about.Email,
                        FacebookCount = about.FacebookCount,
                        Mobile = about.Mobile,
                        WebSite = about.WebSite,
                        Address=about.Address
                        
                    };
                    return Ok(new BaseResponse(aboutModel));
                }
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetBloodBankHelpsAcount()
        {
            if (ModelState.IsValid)
            {
                var BloodBankHelps = aboutService.GetAbout().FirstOrDefault().BloodBankHelpsAcount;
                return Ok(new BaseResponse(BloodBankHelps));
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetEmergencyNumber()
        {
            if (ModelState.IsValid)
            {
                var emergencyNumber = aboutService.GetAbout().FirstOrDefault().EmergencyNumber;
                return Ok(new BaseResponse(emergencyNumber));
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetContactNumber()
        {
            if (ModelState.IsValid)
            {
                var contactNumber = aboutService.GetAbout().FirstOrDefault().ContactNumber;
                return Ok(new BaseResponse(contactNumber));
            }
            return BadRequest(ModelState);
        }

    }
}
