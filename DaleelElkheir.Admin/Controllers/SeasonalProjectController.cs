using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.SeasonalProjects;
using DaleelElkheir.BLL.Services.SeasonalProjects;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class SeasonalProjectController : Controller
    {
        private readonly ISeasonalProjectService seasonalProjectService; 
        public SeasonalProjectController(ISeasonalProjectService _seasonalProjectService)
        {
            this.seasonalProjectService = _seasonalProjectService;
        }

        public ActionResult SeasonalProjectList()
        {

            var seasonalProjects = seasonalProjectService.GetSeasonalProjects();
            return View(seasonalProjects);
        }

        [HttpGet]
        public ActionResult CreateSeasonalProject()
        {

            return View();
        }

        public ActionResult CreateSeasonalProject(SeasonalProjectModel model)
        {
            if (ModelState.IsValid)
            {
                var _SeasonalProjects = new SeasonalProject()
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                };

                seasonalProjectService.InsertSeasonalProject(_SeasonalProjects);
                return RedirectToAction("SeasonalProjectList");
            }
            return RedirectToAction("CreateSeasonalProject");
        }

        [HttpGet]
        public ActionResult UpdateSeasonalProject(int seasonalProjectID)
        {
            var _SeasonalProject = seasonalProjectService.GetSeasonalProject(seasonalProjectID);


            var ProjectModel = new SeasonalProjectModel()
            {
                ID = _SeasonalProject.ID,
                NameEn = _SeasonalProject.NameEn,
                NameAr = _SeasonalProject.NameAr,
            };
            return View(ProjectModel);
        }

        public ActionResult UpdateSeasonalProject(SeasonalProjectModel model)
        {
            var _SeasonalProject = new SeasonalProject()
            {
                ID = model.ID,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
            };
            seasonalProjectService.UpdateSeasonalProject(_SeasonalProject);
            return RedirectToAction("SeasonalProjectList");
        }

        public ActionResult DeleteSeasonalProject(int SeasonalProjectID)
        {
            var ProjectActivities = seasonalProjectService.GetSeasonalProjectActivity(x => x.SeasonalProjectID == SeasonalProjectID);

            if (ProjectActivities.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                seasonalProjectService.DeleteSeasonalProject(SeasonalProjectID);
                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("SeasonalProjectList");
            }


        }

        public ActionResult SeasonalProjectActivity(int SeasonalProjectID)
        {
            try
            {
                var currentUser = (Session["User"] as User);
                ViewBag.CurrentUserOrgID = currentUser.OrganizationID;
                var ProjectActivities = seasonalProjectService.GetSeasonalProjectActivity(x => x.SeasonalProjectID == SeasonalProjectID && x.JoinStatus == 1);
                ViewBag.ProjectID = SeasonalProjectID;
                return View(ProjectActivities);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.InnerException != null ? ex.Message : ex.InnerException.Message;
                return View("Error");
              }
           
        }

        public ActionResult AcceptActivity(int ActivityID)
        {
            var Activity = seasonalProjectService.GetSeasonalProjectActivity(ActivityID);
            Activity.Approval = 1;
            seasonalProjectService.UpdateSeasonalProjectActivity(Activity);
            return RedirectToAction("SeasonalProjectActivity",new { SeasonalProjectID=Activity.SeasonalProjectID });

        }
        public ActionResult RejectActivity(int ActivityID)
        {
            var Activity = seasonalProjectService.GetSeasonalProjectActivity(ActivityID);
            Activity.Approval = 2;
            seasonalProjectService.UpdateSeasonalProjectActivity(Activity);
            return RedirectToAction("SeasonalProjectActivity", new { SeasonalProjectID = Activity.SeasonalProjectID });
        }
    }
}