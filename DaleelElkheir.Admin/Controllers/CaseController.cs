using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Cases;
using DaleelElkheir.Admin.TriggerNotifications;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.Categories;
using DaleelElkheir.BLL.Services.ChatThreads;
using DaleelElkheir.BLL.Services.Confirmations;
using DaleelElkheir.BLL.Services.DeviceTokens;
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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DaleelElkheir.BLL.Services.CharityTypes;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir,Organization")]
    public class CaseController : Controller
    {
        private readonly ICaseService caseService;
        private readonly IRegionService regionService;
        private readonly ICategoryService categoryService;
        private readonly IFileDataService FileData_Service;
        private readonly IUserService user_Service;
        private readonly IOrganizationService organizationService;
        private readonly IOurProgramService OurProgram_Service;
        private readonly IConfirmationService caseConfirmationService;
        private readonly ITriggerNotificationSender triggerNotification;
        private readonly IDeviceTokenService deviceTokenService;
        private readonly IChatThreadService chatService;
        private readonly ICharityTypeServices charityServices;

        public CaseController(ICaseService _caseService, IRegionService _regionService, ICategoryService _categoryService, 
            IOrganizationService _organizationService, IFileDataService _FileDataService, IUserService _userService,
            IOurProgramService _OurProgramService, IConfirmationService _caseConfirmationService,
            ITriggerNotificationSender _triggerNotification, IDeviceTokenService _deviceTokenService, IChatThreadService _chatService,
            ICharityTypeServices _charityServices)
        {
            this.caseService = _caseService;
            this.regionService = _regionService;
            this.categoryService = _categoryService;
            this.organizationService = _organizationService;
            this.FileData_Service = _FileDataService;
            this.user_Service = _userService;
            this.OurProgram_Service = _OurProgramService;
            this.caseConfirmationService = _caseConfirmationService;
            this.triggerNotification = _triggerNotification;
            this.deviceTokenService = _deviceTokenService;
            this.chatService = _chatService;
            this.charityServices = _charityServices;

            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;
        }

        public ActionResult CaseList()
        {
            IList<SelectListItem> OrgList = organizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Value = x.NameEn, Text = x.NameEn }).ToList();
            OrgList.Insert(0, new SelectListItem { Text = "select Organization", Value = "" });
            ViewBag.Organization = OrgList;

            var user= (Session["User"] as User);

            var cases =  caseService.GetCases(x=>x.OrgID==user.OrganizationID||user.UserTypeID==1);

            for (int i = 0; i < cases.Count(); i++)
            {
                cases[i].DescriptionAr = cases[i].DescriptionAr != null ? Regex.Replace(cases[i].DescriptionAr, @"<[^>]*>", "") : "";
                cases[i].DescriptionEn = cases[i].DescriptionEn != null ? Regex.Replace(cases[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(cases);
        }

        [HttpGet]
        public ActionResult CreateCase()
        {
            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;

            IList<SelectListItem> CategoryList = categoryService.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CategoryList.Insert(0, new SelectListItem { Text = "select category", Value = "" });
            ViewBag.category = CategoryList;

            IList<SelectListItem> CaseStatusList = caseService.GetCaseStatus().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            CaseStatusList.Insert(0, new SelectListItem { Text = "select case status", Value = "" });
            ViewBag.CaseStatus = CaseStatusList;


            IList<SelectListItem> CaseTypeList = caseService.GetCaseType().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CaseTypeList.Insert(0, new SelectListItem { Text = "select case type", Value = "" });
            ViewBag.CaseType = CaseTypeList;

            var currentUser = (Session["User"] as User);
            if (currentUser != null && currentUser.UserTypeID == 2)
            {
                IList<SelectListItem> OrganizationList = organizationService.GetOrganizations(y=>y.ID==currentUser.OrganizationID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
                ViewBag.Organizations = OrganizationList;
            }
            else
            {
                IList<SelectListItem> OrganizationList = organizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
                OrganizationList.Insert(0, new SelectListItem { Text = "select Organization ", Value = "" });
                ViewBag.Organizations = OrganizationList;
            }

            // IList<SelectListItem> regionList = regionService.GetRegions().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            // regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = "";

            IList<SelectListItem> OurProgramList = OurProgram_Service.GetOurPrograms().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.TitleEn }).ToList();
            OurProgramList.Insert(0, new SelectListItem { Text = "select Program ", Value = "" });
            ViewBag.OurPrograms = OurProgramList;

            IList<SelectListItem> charityTypes = charityServices.GetCharityType().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.CharityName }).ToList();
            charityTypes.Insert(0, new SelectListItem { Text = "select Charity Type", Value = "" });
            ViewBag.charityTypes = charityTypes;

            IList<SelectListItem> ConfirmationList = caseConfirmationService.GetCaseConfirmation(x=>x.ID!=3).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            ConfirmationList.Insert(0, new SelectListItem { Text = "select Confirmation", Value = "" });
            ViewBag.Confirmation = ConfirmationList;

            return View();
        }

        public ActionResult CreateCase(CaseModel model, HttpPostedFileBase file)
        {
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
                    model.ImageFileID = request.ID;
                }

                var currentUser = (Session["User"] as User);
                var _case = new HelpCase();

                _case.NameEn = model.NameEn;
                _case.NameAr = model.NameAr;
                _case.CaseCode = model.CaseCode;
                _case.ContactNumber = model.ContactNumber;
                _case.DescriptionEn = model.DescriptionEn;
                _case.DescriptionAr = model.DescriptionAr;
                _case.CityID = model.CityID;
                _case.CaseStatusID = model.CaseStatusID;
                _case.CaseTypeID = model.CaseTypeID;
                _case.CategoryID = model.CategoryID;
                _case.CurrentAmount = model.CurrentAmount;
                _case.DueDate = Convert.ToDateTime(model.DueDate);
                _case.RequiredAmount = model.RequiredAmount;
                _case.OrgID = model.OrgID;
                _case.OurProgramID = model.OurProgramID;
                _case.ConfirmationID = model.ConfirmationID;
                _case.UserID = currentUser.ID;
                _case.FileData = model.FileData;
                _case.ImageFileID = model.ImageFileID;
                _case.CreateDate = DateTime.Now;
                _case.DescriptionProgram = model.DescriptionProgram;
               
                if (currentUser!=null&&currentUser.UserTypeID==2)
                {
                    _case.ConfirmationID = 1;
                    _case.OrgID = currentUser.OrganizationID;
                }
                
                //if (image.ID != 0)
                //    _case.ImageFileID = image.ID;

                caseService.InsertCase(_case);

                if (_case.ConfirmationID == 2)
                {
                    InsertCaseNotification();
                }
                return RedirectToAction("CaseList");
            }
            return RedirectToAction("CreateCase");
        }

        [HttpGet]
        public ActionResult UpdateCase(int caseID)
        {
            var _case = caseService.GetCase(caseID);

            IList<SelectListItem> CategoryList = categoryService.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CategoryList.Insert(0, new SelectListItem { Text = "select category", Value = "" });
            ViewBag.category = CategoryList;



            IList<SelectListItem> CaseStatusList = caseService.GetCaseStatus().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            CaseStatusList.Insert(0, new SelectListItem { Text = "select case status", Value = "" });
            ViewBag.CaseStatus = CaseStatusList;


            IList<SelectListItem> CaseTypeList = caseService.GetCaseType().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CaseTypeList.Insert(0, new SelectListItem { Text = "select case type", Value = "" });
            ViewBag.CaseType = CaseTypeList;

          

            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;


            IList<SelectListItem> regionList = regionService.GetRegions(x => x.GovernorateID == _case.City.GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = regionList;

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

            IList<SelectListItem> OurProgramList = OurProgram_Service.GetOurPrograms().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.TitleEn }).ToList();
            OurProgramList.Insert(0, new SelectListItem { Text = "select Program ", Value = "" });
            ViewBag.OurPrograms = OurProgramList;


            IList<SelectListItem> ConfirmationList = caseConfirmationService.GetCaseConfirmations().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            ConfirmationList.Insert(0, new SelectListItem { Text = "select Confirmation", Value = "" });
            ViewBag.Confirmation = ConfirmationList;

            var caseMod = new CaseModel()
            {
                ID = _case.ID,
                NameEn = _case.NameEn,
                NameAr = _case.NameAr,
                CaseCode = _case.CaseCode,
                ContactNumber = _case.ContactNumber,
                DescriptionEn = _case.DescriptionEn,
                DescriptionAr = _case.DescriptionAr,
                CityID = _case.CityID,
                CaseStatusID = _case.CaseStatusID,
                CaseTypeID = _case.CaseTypeID,
                CategoryID = _case.CategoryID,
                CurrentAmount = _case.CurrentAmount,
                DueDate = _case.DueDate!=null?_case.DueDate.Value.ToShortDateString():null,
                RequiredAmount = _case.RequiredAmount,
                GovernorateID=_case.City.GovernorateID,
                OrgID=_case.OrgID,
                //FileBinary = _case.FileData != null ? _case.FileData.FileBinary : null,
                ImageFileID=_case.ImageFileID,
                UserID=_case.UserID,
                OurProgramID=_case.OurProgramID,
                ConfirmationID=_case.ConfirmationID,
                FileData=_case.FileData,
                DescriptionProgram = _case.DescriptionProgram,
            };
            return View(caseMod);
        }

        public ActionResult UpdateCase(CaseModel model, HttpPostedFileBase file)
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
                if (model.ImageFileID != null)
                {
                    request.ID = int.Parse(model.ImageFileID.ToString());
                    FileData_Service.UpdateFileData(request);
                }
                else
                {
                    FileData_Service.InsertFileData(request);
                    model.ImageFileID = request.ID;
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


            //    if (model.ImageFileID != null)
            //    {
            //        image = new FileData()
            //        {
            //            ID = int.Parse(model.ImageFileID.ToString()),
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

            var _case = new HelpCase()
            {
                ID = model.ID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                CaseCode=model.CaseCode,
                ContactNumber = model.ContactNumber,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                CityID = model.CityID,
                CaseStatusID = model.CaseStatusID,
                CaseTypeID = model.CaseTypeID,
                CategoryID = model.CategoryID,
                CurrentAmount = model.CurrentAmount,
                DueDate =Convert.ToDateTime(model.DueDate),
                RequiredAmount = model.RequiredAmount,
                OrgID=model.OrgID,
                ImageFileID = model.ImageFileID,
                UserID=model.UserID,
                OurProgramID=model.OurProgramID,
                ConfirmationID=model.ConfirmationID,
                FileData=model.FileData,
                DescriptionProgram = model.DescriptionProgram,
            };
            var CaseBeforeUpdate = caseService.GetCas().Where(x=>x.ID==model.ID).FirstOrDefault();
            if (CaseBeforeUpdate.ConfirmationID != 2 && _case.ConfirmationID == 2)
            {
                InsertCaseNotification();
            }
            caseService.UpdateCase(_case);
            return RedirectToAction("CaseList");
        }

        [HttpGet]
        public ActionResult GetRegions(int GovernorateID)
        {
            IList<SelectListItem> regionsList = regionService.GetRegions(x => x.GovernorateID == GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionsList.Insert(0, new SelectListItem { Text = "select segions", Value = "" });
            ViewBag.region = regionsList;
            return Json(regionsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCase(int caseID)
        {
            //deleting case related chat threads
            chatService.DeleteChatThreads(caseID);
            var followCases = user_Service.GetUserCases(x=>x.CaseID==caseID);
            foreach(var cases in followCases)
            {
                user_Service.DeleteUserCase(cases.ID);
            }
            var cse = caseService.GetCase(caseID);
            var fileID = cse.ImageFileID;
            caseService.DeleteCase(caseID);
            if (fileID != null)
            {
                FileData_Service.DeleteFileData(int.Parse(fileID.ToString()));
            }
            return RedirectToAction("CaseList");
        }

        public void InsertCaseNotification()
        {
            var usersList = user_Service.GetUsers();

            var currentUser = (Session["User"] as User).ID;

            //var UserDevices = user_Service.GetUsersDevices().Where(x => usersList.Contains(x.User)).AsQueryable();

            var UserDevices = deviceTokenService.GetDeviceTokens().AsQueryable();

            var isSent = triggerNotification.SendAllUsers(currentUser, UserDevices, "New Case", "New Case Added", "تم اضافة حالة جديد",1);
        }
    }
}