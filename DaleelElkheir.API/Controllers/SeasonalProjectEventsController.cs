using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.SeasonProjectEvents;
using DaleelElkheir.BLL.Services.SeasonalProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class SeasonalProjectEventsController : ApiController
    {
        private readonly ISeasonalProjectService seasonalProjectService;
        public SeasonalProjectEventsController(ISeasonalProjectService _seasonalProjectService)
        {
            this.seasonalProjectService = _seasonalProjectService;
        }

        [HttpPost]
        public IHttpActionResult GetSeasonalProjectEvents(BaseRequest request)
        {
            if (ModelState.IsValid)
            {

                var events = seasonalProjectService.GetEventActivities();
                List<SeasonProjectEventModel> eventList = new List<SeasonProjectEventModel>();
                foreach (var item in events)
                {
                    var eventModel = new SeasonProjectEventModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        StartDate= item.StartDate.HasValue? item.StartDate.Value.ToString():"",
                        EndDate= item.EndDate.HasValue? item.EndDate.Value.ToString():"",
                       // Activity= request.Lang == "ar" ? item.Activity.NameAr : item.Activity.NameEn,
                        Project = request.Lang == "ar" ? item.Activity.SeasonalProject.NameAr : item.Activity.SeasonalProject.NameEn,
                    };
                    eventList.Add(eventModel);
                }
                return Ok(new BaseResponse(eventList));
            }
            return BadRequest(ModelState);
        }

    }
}
