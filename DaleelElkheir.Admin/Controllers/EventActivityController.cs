using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.SeasonalProjects;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.SeasonalProjects;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir,Organization")]
    public class EventActivityController : Controller
    {
        private readonly ISeasonalProjectService eventService;
        private readonly IFileDataService FileData_Service;
        private readonly ISeasonalProjectService ProjectActivityService;
        public EventActivityController(ISeasonalProjectService _eventService, IFileDataService _FileDataService, ISeasonalProjectService _ProjectActivityService)
        {
            this.eventService = _eventService;
            this.FileData_Service = _FileDataService;
            this.ProjectActivityService = _ProjectActivityService;
        }
        public ActionResult EventActivityList()
        {
            CultureInfo customCulture = new CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;

            var events = eventService.GetEventActivities();

            for (int i = 0; i < events.Count(); i++)
            {
                events[i].DescriptionAr = events[i].DescriptionAr != null ? Regex.Replace(events[i].DescriptionAr, @"<[^>]*>", "") : "";
                events[i].DescriptionEn = events[i].DescriptionEn != null ? Regex.Replace(events[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(events);
        }

        [HttpGet]
        public ActionResult CreateEventActivity()
        {
            IList<SelectListItem> ProjectList = ProjectActivityService.GetSeasonalProjects().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            ProjectList.Insert(0, new SelectListItem { Text = "select Project", Value = "" });
            ViewBag.Project = ProjectList;

            //IList<SelectListItem> Activity_List = eventService.GetSeasonalProjectActivities().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            //Activity_List.Insert(0, new SelectListItem { Text = "select Activity", Value = "" });
            ViewBag.ActivityList = "";

            return View();
        }

        public ActionResult CreateEventActivity(EventActivityModel model)
        {
            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy h:mm tt";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            if (ModelState.IsValid)
            {
               
                var _event = new EventForActivity();

                _event.TitleEn = model.TitleEn;
                _event.TitleAr = model.TitleAr;
                _event.DescriptionEn = model.DescriptionEn;
                _event.DescriptionAr = model.DescriptionAr;
                _event.StartDate = Convert.ToDateTime(model.StartDate);
                _event.EndDate = Convert.ToDateTime(model.EndDate);
                _event.ActivityID = model.ActivityID;

                eventService.InsertEventActivity(_event);
                return RedirectToAction("EventActivityList");
            }
            return RedirectToAction("CreateEventActivity");
        }

        [HttpGet]
        public ActionResult UpdateEventActivity(int eventID)
        {
            IList<SelectListItem> ProjectList = ProjectActivityService.GetSeasonalProjects().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            ProjectList.Insert(0, new SelectListItem { Text = "select Project", Value = "" });
            ViewBag.Project = ProjectList;

            var _event = eventService.GetEventActivity(eventID);

            IList<SelectListItem> Activity_List = eventService.GetSeasonalProjectActivity(x=>x.SeasonalProjectID==_event.Activity.SeasonalProjectID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            Activity_List.Insert(0, new SelectListItem { Text = "select Activity", Value = "" });
            ViewBag.ActivityList = Activity_List;

            

            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            var eventModel = new EventActivityModel()
            {
                ID = _event.ID,
                TitleEn = _event.TitleEn,
                TitleAr = _event.TitleAr,
                DescriptionEn = _event.DescriptionEn,
                DescriptionAr = _event.DescriptionAr,
                StartDate = _event.StartDate.ToString(),
                EndDate = _event.EndDate.ToString(),
                ActivityID=_event.ActivityID,
                SeasonalProjectID=_event.Activity.SeasonalProjectID
            };
            return View(eventModel);
        }

        public ActionResult UpdateEventActivity(EventActivityModel model)
        {
            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy h:mm tt";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            var _event = new EventForActivity()
            {
                ID = model.ID,
                TitleEn = model.TitleEn,
                TitleAr = model.TitleAr,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                StartDate = Convert.ToDateTime(model.StartDate),
                EndDate = Convert.ToDateTime(model.EndDate),
                ActivityID=model.ActivityID
            };
            eventService.UpdateEventActivity(_event);
            return RedirectToAction("EventActivityList");
        }

        public ActionResult DeleteEventActivity(int eventID)
        {
         
            eventService.DeleteEventActivity(eventID);
            return RedirectToAction("EventActivityList");
        }

        [HttpGet]
        public ActionResult GetActivities(int SeasonalProjectID)
        {
            IList<SelectListItem> ActivityList = eventService.GetSeasonalProjectActivity(x => x.SeasonalProjectID == SeasonalProjectID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            ActivityList.Insert(0, new SelectListItem { Text = "select Activity", Value = "" });
            ViewBag.ActivityList = ActivityList;
            return Json(ActivityList, JsonRequestBehavior.AllowGet);
        }

    }
}