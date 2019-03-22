using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Cases;
using DaleelElkheir.BLL.Services.Cases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DaleelElkheir.API.Controllers
{
    
    public class CaseController : ApiController
    {
        private readonly ICaseService CaseServices;

        public CaseController(ICaseService _CaseServices)
        {
            this.CaseServices = _CaseServices;
        }

        [HttpPost]
        public IHttpActionResult GetCases(UserIDModel requst)
        {
            if(ModelState.IsValid)
            {
                var cases = CaseServices.GetCases(x=>x.ConfirmationID==2).OrderByDescending(x=>x.CreateDate);
                List<CaseModel> CasesList = new List<CaseModel>();
                var sharedURL = ConfigurationManager.AppSettings["Image_URL"];

                foreach (var item in cases)
                {
                     var caseModel = new CaseModel {
                         ID=item.ID,
                         Name=requst.Lang=="ar"?item.NameAr:item.NameEn,
                         CaseCode=item.CaseCode,
                         Organization=requst.Lang=="ar"?item.Organization.NameAr:item.Organization.NameEn,
                         Category=requst.Lang=="ar"?item.Category.NameAr:item.Category.NameEn,
                         CaseType= requst.Lang == "ar" ?item.CaseType.NameAr:item.CaseType.NameEn,
                         CaseStatus=item.CaseStatu.Name,
                         City=requst.Lang=="ar"?item.City.NameAr:item.City.NameEn,
                         Governorate=requst.Lang=="ar"?item.City.Governorate.NameAr: item.City.Governorate.NameEn,
                         Image =item.FileData!=null?item.FileData.Extenstion:null,
                         DueDate=item.DueDate!=null?item.DueDate.Value.ToShortDateString():"",
                         Description = requst.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                         RequiredAmount = item.RequiredAmount,
                         CurrentAmount = item.CurrentAmount,
                         SharedURL = (sharedURL+item.ID),
                         Joined=(requst.UserID==null?false:item.UserCases.Any(w=>w.UserID==requst.UserID)),
                         ProgramDescription = item.DescriptionProgram,
                     };
                    CasesList.Add(caseModel);
                }
                return Ok(new BaseResponse(CasesList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetCaseDetail(BaseRequestID requst)
        {
            if (ModelState.IsValid)
            {
                var caseObj = CaseServices.GetCase(requst.ID);
                var sharedURL = ConfigurationManager.AppSettings["Image_URL"];

                if (caseObj != null)
                {
                    var caseModel = new CaseModelDetail
                    {
                        Name = requst.Lang == "ar" ? caseObj.NameAr : caseObj.NameEn,
                        CaseCode=caseObj.CaseCode,
                        ContactNumber = caseObj.ContactNumber,
                        Organization = requst.Lang == "ar" ? caseObj.Organization.NameAr : caseObj.Organization.NameEn,
                        Description = requst.Lang == "ar" ? caseObj.DescriptionAr : caseObj.DescriptionEn,
                        Category = requst.Lang == "ar" ? caseObj.Category.NameAr : caseObj.Category.NameEn,
                        CaseType = requst.Lang == "ar" ? caseObj.CaseType.NameAr : caseObj.CaseType.NameEn,
                        CaseStatus = caseObj.CaseStatu.Name,
                        City = requst.Lang == "ar" ? caseObj.City.NameAr : caseObj.City.NameEn,
                        Governorate = requst.Lang == "ar" ? caseObj.City.Governorate.NameAr : caseObj.City.Governorate.NameEn,
                        Image = caseObj.FileData != null ? caseObj.FileData.Extenstion : null,
                        DueDate = caseObj.DueDate != null ? caseObj.DueDate.Value.ToShortDateString() : "",
                        RequiredAmount = caseObj.RequiredAmount,
                        CurrentAmount=caseObj.CurrentAmount,
                        SharedURL = (sharedURL + caseObj.ID),
                        CategoryID=caseObj.CategoryID,
                        ProgramDescription = caseObj.DescriptionProgram,
                        
                    };

                    return Ok(new BaseResponse(caseModel));
                }
                else
                {
                    return Ok(new BaseResponse(HttpStatusCode.NotFound,"case not found" ));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetUrgentCases(UserIDModel requst)
        {
            if (ModelState.IsValid)
            {
                var cases = CaseServices.GetCases(x=>x.CaseTypeID==2 && x.ConfirmationID == 2).OrderByDescending(x => x.DueDate);
                var sharedURL = ConfigurationManager.AppSettings["Image_URL"];

                List<CaseModel> CasesList = new List<CaseModel>();

                foreach (var item in cases)
                {
                    var caseModel = new CaseModel
                    {
                        ID = item.ID,
                        Name = requst.Lang == "ar" ? item.NameAr : item.NameEn,
                        CaseCode=item.CaseCode,
                        Organization = requst.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Category = requst.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        CaseType = requst.Lang == "ar" ? item.CaseType.NameAr : item.CaseType.NameEn,
                        CaseStatus = item.CaseStatu.Name,
                        City = requst.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Governorate = requst.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        DueDate = item.DueDate != null ? item.DueDate.Value.ToShortDateString() : "",
                        RequiredAmount = item.RequiredAmount,
                        CurrentAmount = item.CurrentAmount,
                        Description = requst.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        SharedURL = (sharedURL + item.ID),
                        Joined = (requst.UserID == null ? false : item.UserCases.Any(w => w.UserID == requst.UserID)),
                        ProgramDescription = item.DescriptionProgram,
                    };
                    CasesList.Add(caseModel);
                }
                return Ok(new BaseResponse(CasesList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetRecentCases(UserIDModel requst)
        {
            if (ModelState.IsValid)
            {
                var cases = CaseServices.GetCases(x => x.ConfirmationID == 2).OrderByDescending(x=>x.CreateDate).Take(6);
                List<CaseModel> CasesList = new List<CaseModel>();
                var sharedURL = ConfigurationManager.AppSettings["Image_URL"];

                foreach (var item in cases)
                {
                    var caseModel = new CaseModel
                    {
                        ID = item.ID,
                        Name = requst.Lang == "ar" ? item.NameAr : item.NameEn,
                        CaseCode=item.CaseCode,
                        Organization = requst.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Category = requst.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        CaseType = requst.Lang == "ar" ? item.CaseType.NameAr : item.CaseType.NameEn,
                        CaseStatus = item.CaseStatu.Name,
                        City = requst.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Governorate = requst.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        DueDate = item.DueDate != null ? item.DueDate.Value.ToShortDateString() : "",
                        RequiredAmount = item.RequiredAmount,
                        CurrentAmount = item.CurrentAmount,
                        Description = requst.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        SharedURL = (sharedURL + item.ID),
                        Joined = (requst.UserID == null ? false : item.UserCases.Any(w => w.UserID == requst.UserID)),
                        ProgramDescription = item.DescriptionProgram,
                    };
                    CasesList.Add(caseModel);
                }
                return Ok(new BaseResponse(CasesList));
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetCasesAcount()
        {
            if (ModelState.IsValid)
            {
                var casesAcount = CaseServices.GetCases().Count();
                return Ok(new BaseResponse(casesAcount));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetFilteredCases(CaseFilterRequest requst)
        {
            if (ModelState.IsValid)
            {
                var cases = CaseServices.GetCases(x => x.ConfirmationID == 2);
                var sharedURL = ConfigurationManager.AppSettings["Image_URL"];

                if (requst.CategoryID != null)
                {
                    cases = cases.Where(x => x.CategoryID == requst.CategoryID).ToList();
                }
                if (requst.GovernorateID != null)
                {
                    cases = cases.Where(x => x.City.GovernorateID == requst.GovernorateID).ToList();
                }
                if (requst.OrganizationID != null)
                {
                    cases = cases.Where(x => x.OrgID == requst.OrganizationID).ToList();
                }
                if (requst.RegionID != null)
                {
                    cases = cases.Where(x => x.CityID == requst.RegionID).ToList();
                }


                List<CaseModel> CasesList = new List<CaseModel>();

                foreach (var item in cases)
                {
                    var caseModel = new CaseModel
                    {
                        ID = item.ID,
                        Name = requst.Lang == "ar" ? item.NameAr : item.NameEn,
                        CaseCode=item.CaseCode,
                        Organization = requst.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Category = requst.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        CaseType = requst.Lang == "ar" ? item.CaseType.NameAr : item.CaseType.NameEn,
                        CaseStatus = item.CaseStatu.Name,
                        City = requst.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Governorate = requst.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        DueDate = item.DueDate != null ? item.DueDate.Value.ToShortDateString() : "",
                        Description = requst.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        SharedURL = (sharedURL + item.ID),
                        Joined = (requst.UserID == null ? false : item.UserCases.Any(w => w.UserID == requst.UserID))
                    };
                    CasesList.Add(caseModel);
                }
                return Ok(new BaseResponse(CasesList));
            }
            return BadRequest(ModelState);
        }

    }
}
