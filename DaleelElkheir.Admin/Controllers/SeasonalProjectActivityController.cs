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
    //[AuthorizeUser(Roles = "Organization")]
    [AuthorizeUser(Roles = "Organization,DaleelElkheir")]
    public class SeasonalProjectActivityController : Controller
    {
        private readonly ISeasonalProjectService ProjectActivityService;

        public SeasonalProjectActivityController(ISeasonalProjectService _ProjectActivityService)
        {
            this.ProjectActivityService = _ProjectActivityService;
        }

        public ActionResult SeasonalProjectActivityList()
        {
            var user = (Session["User"] as User);
            var ProjectActivities = ProjectActivityService.GetSeasonalProjectActivity(x=>x.OrganizationID==user.OrganizationID );
            return View(ProjectActivities);
        }

        [HttpGet]
        public ActionResult CreateSeasonalProjectActivity(int projectID=0)
        {
            IList<SelectListItem> ProjectList = ProjectActivityService.GetSeasonalProjects().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn, Selected = x.ID == projectID ? true : false }).ToList();
            //ProjectList.Insert(0, new SelectListItem { Text = "select Project", Value = "" });
            ViewBag.Project = ProjectList;

            return View();
        }

        public ActionResult CreateSeasonalProjectActivity(SeasonalProjectActivityModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = (Session["User"] as User);
                    var _ProjectActivity = new Activity()
                    {
                        Price = model.Price,
                        Region = model.Region,
                        Target = model.Target,
                        OrganizationID = (int)currentUser.OrganizationID,

                        SeasonalProjectID = model.SeasonalProjectID
                    };
                    //approving activity if the created user is a daleel elkhier user
                    if (currentUser.UserTypeID == 1)
                    {
                        _ProjectActivity.Approval = 1;
                        _ProjectActivity.JoinStatus = 1;
                    }

                    ProjectActivityService.InsertSeasonalProjectActivity(_ProjectActivity);
                    if (currentUser.UserTypeID == 1)
                    {
                        //if the user is daleel elkhier admin, it will be redirected to the whole activities page
                        return RedirectToAction("SeasonalProjectActivity", "SeasonalProject", new { SeasonalProjectID = _ProjectActivity.SeasonalProjectID });
                    }
                    return RedirectToAction("SeasonalProjectActivityList");
                }
                return RedirectToAction("CreateSeasonalProjectActivity");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.ErrorInnerMessage = ex.InnerException!=null? ex.InnerException.Message:"";
                return View("Error");
            }
        
        }

        [HttpGet]
        public ActionResult UpdateSeasonalProjectActivity(int SeasonalProjectActivityID)
        {
            IList<SelectListItem> ProjectList = ProjectActivityService.GetSeasonalProjects().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            ProjectList.Insert(0, new SelectListItem { Text = "select Project", Value = "" });
            ViewBag.Project = ProjectList;

            var _ProjectActivity = ProjectActivityService.GetSeasonalProjectActivity(SeasonalProjectActivityID);


            var ProjectActivityModel = new SeasonalProjectActivityModel()
            {
                ID = _ProjectActivity.ID,
                Price = _ProjectActivity.Price,
                Region = _ProjectActivity.Region,
                Target = _ProjectActivity.Target,
                OrganizationID = _ProjectActivity.OrganizationID,
                SeasonalProjectID =_ProjectActivity.SeasonalProjectID,
                Approval=_ProjectActivity.Approval,
                JoinStatus=_ProjectActivity.JoinStatus
            };
            return View(ProjectActivityModel);
        }

        public ActionResult UpdateSeasonalProjectActivity(SeasonalProjectActivityModel model)
        {
            var _SeasonalProject = new Activity()
            {
                ID = model.ID,
                Price = model.Price,
                Region = model.Region,
                Target = model.Target,
                OrganizationID = model.OrganizationID,
                SeasonalProjectID =model.SeasonalProjectID,
                Approval=model.Approval,
                JoinStatus=model.JoinStatus
            };
            ProjectActivityService.UpdateSeasonalProjectActivity(_SeasonalProject);
            var currentUser = (Session["User"] as User);
            if (currentUser.UserTypeID == 1)
            {
                //if the user is daleel elkhier admin, it will be redirected to the whole activities page
                return RedirectToAction("SeasonalProjectActivity", "SeasonalProject", new { SeasonalProjectID = _SeasonalProject.SeasonalProjectID });
            }
            return RedirectToAction("SeasonalProjectActivityList");
        }

        public ActionResult JoinActivity(int ActivityID)
        {
            var Activity = ProjectActivityService.GetSeasonalProjectActivity(ActivityID);
            Activity.JoinStatus = 1;
            ProjectActivityService.UpdateSeasonalProjectActivity(Activity);
            return RedirectToAction("SeasonalProjectActivityList");

        }
        public ActionResult LeaveActivity(int ActivityID)
        {
            var Activity = ProjectActivityService.GetSeasonalProjectActivity(ActivityID);
            Activity.JoinStatus = 2;
            ProjectActivityService.UpdateSeasonalProjectActivity(Activity);
            return RedirectToAction("SeasonalProjectActivityList");
        }

        public ActionResult DeleteSeasonalProjectActivity(int SeasonalProjectActivityID)
        {
            var EventActivity = ProjectActivityService.GetEventActivity(x => x.ActivityID == SeasonalProjectActivityID);

            if (EventActivity.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ProjectActivityService.DeleteSeasonalProjectActivity(SeasonalProjectActivityID);

                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("SeasonalProjectActivityList");
            }

            
            
        }
    }
}