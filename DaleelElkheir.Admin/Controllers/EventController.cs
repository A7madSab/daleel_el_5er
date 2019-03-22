using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Events;
using DaleelElkheir.Admin.TriggerNotifications;
using DaleelElkheir.BLL.Services.Categories;
using DaleelElkheir.BLL.Services.Confirmations;
using DaleelElkheir.BLL.Services.DeviceTokens;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Services.OurPrograms;
using DaleelElkheir.BLL.Services.Regions;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.BLL.Type;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir,Organization")]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IFileDataService FileData_Service;
        private readonly IUserService user_Service;
        private readonly IOurProgramService OurProgram_Service;
        private readonly IOrganizationService organizationService;
        private readonly ICategoryService categoryService;
        private readonly IRegionService regionService;
        private readonly IConfirmationService caseConfirmationService;
        private readonly ITriggerNotificationSender triggerNotification;
        private readonly IDeviceTokenService deviceTokenService;
        public EventController(IEventService _eventService, IFileDataService _FileDataService,IUserService _userService, IOurProgramService _OurProgramService, IRegionService _regionService, ICategoryService _categoryService, IOrganizationService _organizationService, IConfirmationService _caseConfirmationService, ITriggerNotificationSender _triggerNotification, IDeviceTokenService _deviceTokenService)
        {
            this.eventService = _eventService;
            this.FileData_Service = _FileDataService;
            this.user_Service = _userService;
            this.OurProgram_Service = _OurProgramService;
            this.regionService = _regionService;
            this.categoryService = _categoryService;
            this.organizationService = _organizationService;
            this.caseConfirmationService = _caseConfirmationService;
            this.triggerNotification = _triggerNotification;
            this.deviceTokenService = _deviceTokenService;
        }
        public ActionResult EventList()
        {
            IList<SelectListItem> OrgList = organizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Value = x.NameEn, Text = x.NameEn }).ToList();
            OrgList.Insert(0, new SelectListItem { Text = "select Organization", Value = "" });
            ViewBag.Organization = OrgList;

            CultureInfo customCulture = new CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;

            var user = (Session["User"] as User);

            var events = eventService.GetEvent(x => x.OrganizationID == user.OrganizationID || user.UserTypeID == 1);

            for (int i = 0; i < events.Count(); i++)
            {
                events[i].DescriptionAr = events[i].DescriptionAr != null ? Regex.Replace(events[i].DescriptionAr, @"<[^>]*>", "") : "";
                events[i].DescriptionEn = events[i].DescriptionEn != null ? Regex.Replace(events[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(events);
        }

        [HttpGet]
        public ActionResult CreateEvent()
        {
            IList<SelectListItem> OurProgramList = OurProgram_Service.GetOurPrograms().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.TitleEn }).ToList();
            OurProgramList.Insert(0, new SelectListItem { Text = "select Program ", Value = "" });
            ViewBag.OurPrograms = OurProgramList;

            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;

            IList<SelectListItem> CategoryList = categoryService.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CategoryList.Insert(0, new SelectListItem { Text = "select category", Value = "" });
            ViewBag.category = CategoryList;

            var currentUser = (Session["User"] as User);
            if (currentUser != null && currentUser.UserTypeID == 2)
            {
                IList<SelectListItem> OrganizationList = organizationService.GetOrganizations(y => y.ID == currentUser.OrganizationID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
                ViewBag.Organizations = OrganizationList;
            }
            else
            {
                IList<SelectListItem> OrganizationList = organizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
                OrganizationList.Insert(0, new SelectListItem { Text = "select Organization ", Value = "" });
                ViewBag.Organizations = OrganizationList;
            }
            

            IList<SelectListItem> ConfirmationList = caseConfirmationService.GetCaseConfirmation(x => x.ID != 3).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            ConfirmationList.Insert(0, new SelectListItem { Text = "select Confirmation", Value = "" });
            ViewBag.Confirmation = ConfirmationList;

            ViewBag.region = "";

            return View();
        }

        public ActionResult CreateEvent(EventModel model,HttpPostedFileBase file)
        {
            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy h:mm tt";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            if (ModelState.IsValid)
            {
              
                if (file != null)
                {
                    string dir = Guid.NewGuid().ToString();
                    FileData request = new FileData();
                    var originalName = Path.GetFileName(file.FileName);
                    request.Name = originalName;
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
                        request.Extenstion = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                    }
                    catch
                    {
                        request.Extenstion = null;
                    }
                    FileData_Service.InsertFileData(request);
                    model.ImageID = request.ID;
                }
                //////////////////////////////////////////////
                //byte[] imageData = null;
                //var image = new FileData();
                //if (Request.Files.Count > 0)
                //{
                //    HttpPostedFileBase objFiles = Request.Files["FileBinary"];
                //    using (var binaryReader = new BinaryReader(objFiles.InputStream))
                //    {
                //        imageData = binaryReader.ReadBytes(objFiles.ContentLength);
                //    }
                //    image = new FileData()
                //    {
                //        Name = objFiles.FileName,
                //        FileBinary = imageData
                //    };

                //    FileData_Service.InsertFileData(image);
                //}

                var user = (Session["User"] as User);
                var _event = new Event();

                _event.TitleEn = model.TitleEn;
                _event.TitleAr = model.TitleAr;
                _event.Link = model.Link;
                _event.AddressEn = model.AddressEn;
                _event.AddressAr = model.AddressAr;
                _event.DescriptionEn = model.DescriptionEn;
                _event.DescriptionAr = model.DescriptionAr;
                _event.StartDate = Convert.ToDateTime(model.StartDate);
                _event.EndDate = Convert.ToDateTime(model.EndDate);
                _event.UserID = user.ID;
                _event.OurProgramID = model.OurProgramID;
                _event.CategoryID = model.CategoryID;
                _event.OrganizationID = model.OrganizationID;
                _event.CityID = model.CityID;
                _event.Mobile = model.Mobile;
                _event.ConfirmationID = model.ConfirmationID;
                _event.FileData = model.FileData;
                _event.ImageID = model.ImageID;
                _event.HowToJoin = model.HowToJoin;
                _event.DescriptionProgram = model.DescriptionProgram;
                _event.CreationDate = DateTime.Today;


                if (user != null && user.UserTypeID == 2)
                {
                    _event.ConfirmationID = 1;
                }

                //if (image.ID != 0)
                //    _event.ImageID = image.ID;

                eventService.InsertEvent(_event);

                if (_event.ConfirmationID == 2)
                {
                    InsertEventNotification();
                }

                return RedirectToAction("EventList");
            }
            return RedirectToAction("CreateEvent");
        }

        [HttpGet]
        public ActionResult UpdateEvent(int eventID)
        {
            var _event = eventService.GetEvent(eventID);

            IList<SelectListItem> OurProgramList = OurProgram_Service.GetOurPrograms().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.TitleEn }).ToList();
            OurProgramList.Insert(0, new SelectListItem { Text = "select Program ", Value = "" });
            ViewBag.OurPrograms = OurProgramList;

            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;

            IList<SelectListItem> CategoryList = categoryService.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CategoryList.Insert(0, new SelectListItem { Text = "select category", Value = "" });
            ViewBag.category = CategoryList;

            var currentUser = (Session["User"] as User);
            if (currentUser != null && currentUser.UserTypeID == 2)
            {
                IList<SelectListItem> OrganizationList = organizationService.GetOrganizations(y => y.ID == currentUser.OrganizationID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
                ViewBag.Organizations = OrganizationList;
            }
            else
            {

                IList<SelectListItem> OrganizationList = organizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
                OrganizationList.Insert(0, new SelectListItem { Text = "select Organization ", Value = "" });
                ViewBag.Organizations = OrganizationList;
            }

            IList<SelectListItem> regionList = regionService.GetRegions(x => x.GovernorateID == _event.City.GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = regionList;

            IList<SelectListItem> ConfirmationList = caseConfirmationService.GetCaseConfirmations().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            ConfirmationList.Insert(0, new SelectListItem { Text = "select Confirmation", Value = "" });
            ViewBag.Confirmation = ConfirmationList;

            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            var eventModel = new EventModel()
            {
                ID = _event.ID,
                TitleEn = _event.TitleEn,
                TitleAr = _event.TitleAr,
                DescriptionEn = _event.DescriptionEn,
                DescriptionAr = _event.DescriptionAr,
                Link = _event.Link,
                AddressEn = _event.AddressEn,
                AddressAr = _event.AddressAr,
                StartDate = _event.StartDate.ToString(),
                EndDate = _event.EndDate.ToString(),
                FileData = _event.FileData,
                ImageID = _event.ImageID,
                UserID = _event.UserID,
                OurProgramID = _event.OurProgramID,
                CategoryID = _event.CategoryID,
                OrganizationID = _event.OrganizationID,
                CityID = _event.CityID,
                GovernorateID = _event.City.GovernorateID,
                Mobile = _event.Mobile,
                ConfirmationID = _event.ConfirmationID,
                CreationDate = _event.CreationDate,
                DescriptionProgram = _event.DescriptionProgram,
                HowToJoin = _event.HowToJoin,
            };
            return View(eventModel);
        }

        public ActionResult UpdateEvent(EventModel model , HttpPostedFileBase file)
        {
            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy h:mm tt";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;


            if (file != null)
            {
                string dir = Guid.NewGuid().ToString();
                FileData request = new FileData();
                var originalName = Path.GetFileName(file.FileName);
                request.Name = originalName;
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
                    request.Extenstion = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                }
                catch
                {
                    request.Extenstion = null;
                }
                if (model.ImageID != null)
                {
                    request.ID = int.Parse(model.ImageID.ToString());
                    FileData_Service.UpdateFileData(request);
                }
                else
                {
                    FileData_Service.InsertFileData(request);
                    model.ImageID = request.ID;
                }
            }
            else
            {

            }


            //byte[] imageData = null;
            //FileData image = new FileData();
            //HttpPostedFileBase objFiles = Request.Files["FileBinary"];
            //if (objFiles.ContentLength > 0)
            //{
            //    using (var binaryReader = new BinaryReader(objFiles.InputStream))
            //    {
            //        imageData = binaryReader.ReadBytes(objFiles.ContentLength);
            //    }


            //    if (model.ImageID != null)
            //    {
            //        image = new FileData()
            //        {
            //            ID = int.Parse(model.ImageID.ToString()),
            //            Name = objFiles.FileName,
            //            FileBinary = imageData
            //        };

            //        FileData_Service.UpdateFileData(image);
            //    }
            //    else
            //    {
            //        image = new FileData()
            //        {
            //            Name = objFiles.FileName,
            //            FileBinary = imageData
            //        };

            //        FileData_Service.InsertFileData(image);
            //    }

            //}

            var _event = new Event()
            {
                ID = model.ID,
                TitleEn = model.TitleEn,
                TitleAr = model.TitleAr,
                Link=model.Link,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                AddressEn=model.AddressEn,
                AddressAr=model.AddressAr,
                StartDate = Convert.ToDateTime(model.StartDate),
                EndDate = Convert.ToDateTime(model.EndDate),
                ImageID = model.ImageID,
                UserID=model.UserID,
                OurProgramID=model.OurProgramID,
                CategoryID = model.CategoryID,
                OrganizationID = model.OrganizationID,
                CityID = model.CityID,
                Mobile = model.Mobile,
                ConfirmationID = model.ConfirmationID,
                FileData=model.FileData
            };
            var eventBeforeUpdate = eventService.GetEvnt().Where(x=>x.ID==model.ID).FirstOrDefault();
            if (eventBeforeUpdate.ConfirmationID!=2 && _event.ConfirmationID == 2)
            {
                InsertEventNotification();
            }
            eventService.UpdateEvent(_event);
                    
            return RedirectToAction("EventList");
            
        }

        public ActionResult DeleteEvent(int eventID)
        {
           
            var _event = eventService.GetEvent(eventID);
            var fileID = _event.ImageID;

            eventService.DeleteEvent(eventID);

            if (fileID != null)
            {
                FileData_Service.DeleteFileData(int.Parse(fileID.ToString()));
            }

            return RedirectToAction("EventList");
        }

        [HttpGet]
        public ActionResult GetRegions(int GovernorateID)
        {
            IList<SelectListItem> regionsList = regionService.GetRegions(x => x.GovernorateID == GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionsList.Insert(0, new SelectListItem { Text = "select segions", Value = "" });
            ViewBag.region = regionsList;
            return Json(regionsList, JsonRequestBehavior.AllowGet);
        }

        public void InsertEventNotification()
        {

            var currentUser = (Session["User"] as User).ID;
            //var UserDevices = user_Service.GetUsersDevices().Where(x => usersList.Contains(x.User)).AsQueryable();

            var UserDevices = deviceTokenService.GetDeviceTokens().AsQueryable();

            var isSent = triggerNotification.SendAllUsers(currentUser, UserDevices, "New Event", "New Event Added", "تم اضافة حدث جديد",2);
        }

    }
}