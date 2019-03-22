using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Organizations;
using DaleelElkheir.BLL.Services.Categories;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Organizations;
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
using System.Data.Entity;
namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService organizationService;
        private readonly IRegionService regionService;
        private readonly IFileDataService FileDataService;
        private readonly ICategoryService CategoryService;
        private readonly IUserService userService;

        public OrganizationController(IOrganizationService _organizationService, IRegionService _regionService, 
            IFileDataService _FileDataService, ICategoryService _CategoryService, IUserService _userService)
        {
            this.organizationService = _organizationService;
            this.regionService = _regionService;
            this.FileDataService = _FileDataService;
            this.CategoryService = _CategoryService;
            this.userService = _userService;
        }

        public ActionResult OrganizationList()
        {
            IList<SelectListItem> OrganizationList = organizationService.GetOrganizations().Select(x => new SelectListItem { Value = x.NameEn, Text = x.NameEn }).ToList();
            OrganizationList.Insert(0, new SelectListItem { Text = "select Organization ", Value = "" });
            ViewBag.Organization = OrganizationList;

            var organizations = organizationService.GetOrganizations();

            for (int i = 0; i < organizations.Count(); i++)
            {
                organizations[i].DescriptionAr = organizations[i].DescriptionAr != null ? Regex.Replace(organizations[i].DescriptionAr, @"<[^>]*>", "") : "";
                organizations[i].DescriptionEn = organizations[i].DescriptionEn != null ? Regex.Replace(organizations[i].DescriptionEn, @"<[^>]*>", "") : "";
            }

            IEnumerable<OrganizationModelReturn> ReturnOrganizations = organizations.Select(x => new OrganizationModelReturn
            {
                ID = x.ID,
                NameEn = x.NameEn,
                NameAr = x.NameAr,
                AddressAr = x.NameEn,
                AddressEn = x.AddressEn,
                Governorate = x.City == null ? null : x.City.Governorate.NameEn,
                Region = x.City == null? null: x.City.NameEn,
                Area= x.Area == null ? null : x.Area.NameEn,
                Categoy = x.OrganizationCategories.FirstOrDefault() == null ? null : x.OrganizationCategories.FirstOrDefault().Category.NameEn,
                ProgramDescription = x.DescriptionProgram,
                Status = x.Status
            });

            return View(ReturnOrganizations);

        }

        [HttpGet]
        public ActionResult CreateOrganization()
        {
            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;

            // IList<SelectListItem> regionList = regionService.GetRegions().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            // regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = "";
            ViewBag.area = "";

            IList<SelectListItem> CatList = CategoryService.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            ViewBag.Category = CatList;

            return View();
        }

        public ActionResult CreateOrganization(OrganizationModel model, HttpPostedFileBase file)
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
                    FileDataService.InsertFileData(request);
                    model.LogoFileID = request.ID;
                }

                var organization = new Organization();

                organization.NameEn = model.NameEn.Trim();
                organization.NameAr = model.NameAr.Trim();
                organization.AddressAr = model.AddressAr;
                organization.AddressEn = model.AddressEn;
                organization.DescriptionEn = model.DescriptionEn;
                organization.DescriptionAr = model.DescriptionAr;
                organization.CityID = model.CityID;
                organization.AreaID = model.AreaID;
                organization.Latitude = model.Latitude;
                organization.Longitude = model.Longitude;
                organization.FileData = model.FileData;
                organization.LogoFileID = model.LogoFileID;
                organization.DescriptionProgram = model.DescriptionProgram;
                organization.HowToDonate = model.HowToDonate;
                organization.Status = 1;

                //if (image.ID != 0)
                //    organization.LogoFileID = image.ID;

                organizationService.InsertOrganization(organization);

                if (model.CategoryList != null)
                    foreach (var item in model.CategoryList)
                    {
                        var _CategoryOrg = new OrganizationCategory() {  OrgID = organization.ID, CategoryID = item };
                        organizationService.InsertOrganizationCategory(_CategoryOrg);
                    }

                return RedirectToAction("OrganizationList");
            }
            return RedirectToAction("CreateOrganization");
        }

        [HttpGet]
        public ActionResult UpdateOrganization(int organizationID)
        {
            var _organization = organizationService.GetOrganization(organizationID);

            IList<SelectListItem> GovernorateList = regionService.GetGovernorates().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            GovernorateList.Insert(0, new SelectListItem { Text = "select Governorate", Value = "" });
            ViewBag.Governorate = GovernorateList;


            IList<SelectListItem> regionList = regionService.GetRegions(x => x.GovernorateID == _organization.City.GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.region = regionList;

            IList<SelectListItem> areaList = regionService.GetAreas((Int32)_organization.CityID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            areaList.Insert(0, new SelectListItem { Text = "select region", Value = "" });
            ViewBag.area = areaList;

            IList<SelectListItem> CatList = CategoryService.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            ViewBag.Category = CatList;


            var _organizationModel = new OrganizationModel()
            {
                ID = _organization.ID,
                NameEn = _organization.NameEn,
                NameAr = _organization.NameAr,
                AddressEn = _organization.AddressEn,
                AddressAr = _organization.AddressAr,
                DescriptionEn = _organization.DescriptionEn,
                DescriptionAr = _organization.DescriptionAr,
                CityID = _organization.CityID,
                AreaID=_organization.AreaID,
                GovernorateID = _organization.City.GovernorateID,
                DescriptionProgram =_organization.DescriptionProgram,
                HowToDonate = _organization.HowToDonate,
              //  FileBinary = _organization.FileData != null ? _organization.FileData.FileBinary : null,
                LogoFileID = _organization.LogoFileID,
                CategoryList= organizationService.GetOrganizationCategorys(x => x.OrgID == _organization.ID).Select(x => x.CategoryID).ToList(),
                Latitude=_organization.Latitude,
                Longitude=_organization.Longitude,
                FileData = _organization.FileData,
            };
            return View(_organizationModel);
        }

        public ActionResult UpdateOrganization(OrganizationModel model, HttpPostedFileBase file)
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
                if (model.LogoFileID != null)
                {
                    request.ID = int.Parse(model.LogoFileID.ToString());
                    FileDataService.UpdateFileData(request);
                }
                else
                {
                    FileDataService.InsertFileData(request);
                    model.LogoFileID = request.ID;
                }
            }
            else
            {

            }
            
            var _organization = new Organization()
            {
                ID = model.ID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                AddressEn = model.AddressEn,
                AddressAr = model.AddressAr,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                CityID=model.CityID,
                AreaID = model.AreaID,
                LogoFileID = model.LogoFileID,
                HowToDonate = model.HowToDonate,
                DescriptionProgram=model.DescriptionProgram,
                Latitude =model.Latitude,
                Longitude=model.Longitude,
                FileData = model.FileData
            };

            organizationService.UpdateOrganization(_organization);

            var categoryList = organizationService.GetOrganizationCategorys(x => x.OrgID == _organization.ID);
            foreach (var categ in categoryList)
            {
                organizationService.DeleteOrganizationCategory(categ.ID);
            }

            if (model.CategoryList != null)
            foreach (var item in model.CategoryList)
            {
                var _OrgCateg = new OrganizationCategory() { OrgID = _organization.ID, CategoryID = item };
                organizationService.InsertOrganizationCategory(_OrgCateg);
            }

            return RedirectToAction("OrganizationList");
        }

        [HttpGet]
        public ActionResult GetRegions(int GovernorateID)
        {
            IList<SelectListItem> regionsList = regionService.GetRegions(x => x.GovernorateID == GovernorateID).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            regionsList.Insert(0, new SelectListItem { Text = "select segions", Value = "" });
            ViewBag.region = regionsList;
            return Json(regionsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteOrganization(int organizationID)
        {
            var followOrg = userService.GetUserOrganizations(x => x.OrgID == organizationID);
            foreach (var org in followOrg)
            {
                userService.DeleteUserOrganization(org.ID);
            }

            var user = userService.GetUsers(x => x.OrganizationID == organizationID);

            if (user.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var categoryList = organizationService.GetOrganizationCategorys(x => x.OrgID == organizationID);
                foreach (var categ in categoryList)
                {
                    organizationService.DeleteOrganizationCategory(categ.ID);
                }

                var organization = organizationService.GetOrganization(organizationID);
                var fileID = organization.LogoFileID;
                organizationService.DeleteOrganization(organizationID);

                if (fileID != null)
                {
                    FileDataService.DeleteFileData(int.Parse(fileID.ToString()));
                }


                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                //  return RedirectToAction("OrganizationList");
            }


        }

        public ActionResult ApproveOrganization(int organizationID)
        {
            var org = organizationService.GetOrganization(organizationID);
            org.Status = (Int32)OrgStatus.Approved;
            organizationService.UpdateOrganization(org);
            return RedirectToAction("OrganizationList");
        }

        //public ActionResult RejectOrganization(int organizationID)
        //{
        //    var org = organizationService.GetOrganization(organizationID);
        //    org.Status = (Int32)OrgStatus.Rejected;
        //    organizationService.UpdateOrganization(org);
        //    return RedirectToAction("OrganizationList");
        //}
    }
}