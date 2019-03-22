using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.SeasonalProjects;
using DaleelElkheir.BLL.Services.SeasonalProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class SeasonalProjectController : ApiController
    {
        private readonly ISeasonalProjectService seasonalProjectService;

        public SeasonalProjectController(ISeasonalProjectService _SeasonalProjectService)
        {
            this.seasonalProjectService = _SeasonalProjectService;
        }

        [HttpPost]
        public IHttpActionResult GetSeasonalProjects(BaseRequest request)
        {
            if (ModelState.IsValid)
            {
                var projects = seasonalProjectService.GetSeasonalProjects();
                List<SeasonalProjectListModel> projectList = new List<SeasonalProjectListModel>();
                foreach (var item in projects)
                {
                    var projectModel = new SeasonalProjectListModel()
                    {
                        ID = item.ID,
                        Name=request.Lang.ToLower()=="ar"?item.NameAr:item.NameEn,
                        activities=item.Activities.Where(w=>w.JoinStatus==1)?.Select(m=>new SeasonalProjectActivityModel
                        {
                            OrgID=m.OrganizationID,
                            OrgName= request.Lang.ToLower() == "ar" ? m.Organization.NameAr : m.Organization.NameEn,
                            Price=m.Price.ToString(),
                            Region=m.Region,
                            Target=m.Target
                        }).ToList()
                    };
                    projectList.Add(projectModel);
                }
                return Ok(new BaseResponse(projectList));
            }
            return BadRequest(ModelState);
        }
    }
}
