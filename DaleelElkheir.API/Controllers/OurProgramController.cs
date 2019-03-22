using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Cases;
using DaleelElkheir.API.Models.Events;
using DaleelElkheir.API.Models.OurPrograms;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.BLL.Services.OurPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class OurProgramController : ApiController
    {
        private readonly IOurProgramService OurProgramService;
        private readonly ICaseService CaseServices;
        private readonly IEventService EventService;
        public OurProgramController(IOurProgramService _OurProgramService, ICaseService _CaseServices, IEventService _EventService)
        {
            this.OurProgramService = _OurProgramService;
            this.CaseServices = _CaseServices;
            this.EventService = _EventService;
        }

        [HttpPost]
        public IHttpActionResult GetOurProgram(BaseRequest request)
        {
            if (ModelState.IsValid)
            {

                var OurPrograms = OurProgramService.GetOurPrograms();
                List<OurProgramModel> OurProgramList = new List<OurProgramModel>();
                foreach (var item in OurPrograms)
                {
                    var ProgramModel = new OurProgramModel()
                    {
                        ID= item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                    };
                    OurProgramList.Add(ProgramModel);
                }
                return Ok(new BaseResponse(OurProgramList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetCasesForProgram(CaseProgramRequest requst)
        {
            if (ModelState.IsValid)
            {
                var cases = CaseServices.GetCases(x=>x.OurProgramID== requst.ProgramID);
                List<CaseModel> CasesList = new List<CaseModel>();

                foreach (var item in cases)
                {
                    var caseModel = new CaseModel
                    {
                        ID = item.ID,
                        Name = requst.Lang == "ar" ? item.NameAr : item.NameEn,
                        Organization = requst.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Category = requst.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        CaseType = requst.Lang == "ar" ? item.CaseType.NameAr : item.CaseType.NameEn,
                        CaseStatus = item.CaseStatu.Name,
                        City = requst.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Governorate = requst.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        DueDate = item.DueDate != null ? item.DueDate.Value.ToShortDateString() : "",
                        Description = requst.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                    };
                    CasesList.Add(caseModel);
                }
                return Ok(new BaseResponse(CasesList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetEventsForProgram(CaseProgramRequest request)
        {
            if (ModelState.IsValid)
            {
                var events = EventService.GetEvent(x=>x.OurProgramID== request.ProgramID);
                List<EventModel> eventList = new List<EventModel>();
                foreach (var item in events)
                {
                    var eventModel = new EventModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Address = request.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        StartDate = item.StartDate.HasValue ? item.StartDate.Value.ToString() : "",
                        EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToString() : "",
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                    };
                    eventList.Add(eventModel);

                }
                return Ok(eventList);
            }
            return BadRequest(ModelState);
        }

    }
}
