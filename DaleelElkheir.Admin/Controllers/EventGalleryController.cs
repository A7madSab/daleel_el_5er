using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Events;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class EventGalleryController : Controller
    {
        readonly private IEventService eventService;

        public EventGalleryController(IEventService _eventService)
        {
            this.eventService = _eventService;
        }

        public ActionResult EventGalleryList()
        {
            var EventGallerys = eventService.GetEventGallerys();
            return View(EventGallerys);
        }

        [HttpGet]
        public ActionResult CreateEventGallery()
        {
            IList<SelectListItem> eventList = eventService.GetEvents().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.TitleEn }).ToList();
            eventList.Insert(0, new SelectListItem { Text = "select event", Value = "" });
            ViewBag.EventGallery = eventList;

            return View(new EventGalleryModel());
        }

        [HttpPost]
        public ActionResult CreateEventGallery(EventGalleryModel model, HttpPostedFileBase file)
        {

            var Gallery_model = new EventGallery()
            {
                EventID=model.EventID,
                DescriptionEn=model.DescriptionEn,
                DescriptionAr=model.DescriptionAr
            };


            if (file != null)
            {
                string dir = Guid.NewGuid().ToString();
                var originalName = Path.GetFileName(file.FileName);
                Gallery_model.Name = originalName;
                var root = Server.MapPath("~/UploadedFiles");
                root += "/" + dir;
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                else
                {
                    Directory.Delete(root, true);
                    Directory.CreateDirectory(root);
                }
                file.SaveAs(Path.Combine(root, originalName));


                try
                {
                    Gallery_model.Ext = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                }
                catch
                {
                    Gallery_model.Ext = null;
                }
            }
            eventService.InsertEventGallery(Gallery_model);
          
            return RedirectToAction("EventGalleryList");
        }
        public ActionResult DeleteEventGallery(int GalleryID)
        {
            

            eventService.DeleteEventGallery(GalleryID);
            return RedirectToAction("EventGalleryList");
        }

        public ActionResult UpdateEventGallery(int GalleryID)
        {
            IList<SelectListItem> eventList = eventService.GetEvents().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.TitleEn }).ToList();
            eventList.Insert(0, new SelectListItem { Text = "select event", Value = "" });
            ViewBag.EventGallery = eventList;

            var model = eventService.GetEventGallery(GalleryID);
            var EventGallery_model = new EventGalleryModel()
            {
                ID = model.ID,
                EventID=model.EventID??0,
                Ext=model.Ext,
                Name=model.Name,
                DescriptionEn=model.DescriptionEn,
                DescriptionAr=model.DescriptionAr
            };
            return View(EventGallery_model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateEventGallery(EventGalleryModel model, HttpPostedFileBase file)
        {
            var EventGallery_model = new EventGallery()
            {
                ID = model.ID,
                EventID = model.EventID,
                Ext = model.Ext,
                Name = model.Name,
                DescriptionEn=model.DescriptionEn,
                DescriptionAr=model.DescriptionAr
            };

            if (file != null)
            {
                string dir = Guid.NewGuid().ToString();
                var originalName = Path.GetFileName(file.FileName);
                EventGallery_model.Name = originalName;
                var root = Server.MapPath("~/UploadedFiles");
                root += "/" + dir;
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                else
                {
                    Directory.Delete(root, true);
                    Directory.CreateDirectory(root);
                }
                file.SaveAs(Path.Combine(root, originalName));

                try
                {
                    EventGallery_model.Ext = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                }
                catch
                {
                    EventGallery_model.Ext = null;
                }             
             
            }

            eventService.UpdateEventGallery(EventGallery_model);
            return RedirectToAction("EventGalleryList");
        }

    }
}