using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Sponsors;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Sponsors;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class SponsorController : Controller
    {
        private readonly ISponsorService sponsorService;
        private readonly IFileDataService FileDataService;
        public SponsorController(ISponsorService _SponsorService, IFileDataService _FileDataService)
        {
            this.sponsorService = _SponsorService;
            this.FileDataService = _FileDataService;
        }
        public ActionResult SponsorList()
        {

            var Sponsors = sponsorService.GetSponsors();

            return View(Sponsors);
        }

        [HttpGet]
        public ActionResult CreateSponsor()
        {
            return View();
        }

        public ActionResult CreateSponsor(SponsorModel model, HttpPostedFileBase file)
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
                        model.ImageID = request.ID;
                    }

                    var _Sponsor = new Sponsor();
                _Sponsor.NameEn = model.NameEn;
                _Sponsor.NameAr = model.NameAr;
                _Sponsor.Link = model.Link;
                _Sponsor.ImageID = model.ImageID;
                _Sponsor.FileData = model.FileData;

                sponsorService.InsertSponsor(_Sponsor);
                return RedirectToAction("SponsorList");
            }
            return RedirectToAction("CreateSponsor");
        }

        [HttpGet]
        public ActionResult UpdateSponsor(int SponsorID)
        {
            var _Sponsor = sponsorService.GetSponsor(SponsorID);


            var _SponsorModel = new SponsorModel()
            {
                ID = _Sponsor.ID,
                NameEn = _Sponsor.NameEn,
                NameAr = _Sponsor.NameAr,
                Link = _Sponsor.Link,
                ImageID = _Sponsor.ImageID,
                FileData=_Sponsor.FileData
            };
            return View(_SponsorModel);
        }

        public ActionResult UpdateSponsor(SponsorModel model, HttpPostedFileBase file)
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
                if (model.ImageID != null)
                {
                    request.ID = int.Parse(model.ImageID.ToString());
                    FileDataService.UpdateFileData(request);
                }
                else
                {
                    FileDataService.InsertFileData(request);
                    model.ImageID = request.ID;
                }
            }
            var _Sponsor = new Sponsor()
            {
                ID = model.ID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                Link=model.Link,
                ImageID = model.ImageID,
                FileData=model.FileData

            };
            sponsorService.UpdateSponsor(_Sponsor);
            return RedirectToAction("SponsorList");
        }

        public ActionResult DeleteSponsor(int SponsorID)
        {
            var sponsor = sponsorService.GetSponsor(SponsorID);
            var fileID = sponsor.ImageID;
            sponsorService.DeleteSponsor(SponsorID);

            if (fileID != null)
            {
                FileDataService.DeleteFileData(int.Parse(fileID.ToString()));
            }
            return RedirectToAction("SponsorList");
        }
    }
}