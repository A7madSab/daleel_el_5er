using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Informations;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Informations;
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
    public class InformationController : Controller
    {
        private readonly IInformationService informationService;
        private readonly IFileDataService FileDataService;
        public InformationController(IInformationService _informationService,IFileDataService _FileDataService)
        {
            this.informationService = _informationService;
            this.FileDataService = _FileDataService;
        }
        public ActionResult InformationList()
        {

            var informations = informationService.GetInformations();

            for (int i = 0; i < informations.Count(); i++)
            {
                informations[i].DescriptionAr = informations[i].DescriptionAr != null ? Regex.Replace(informations[i].DescriptionAr, @"<[^>]*>", "") : "";
                informations[i].DescriptionEn = informations[i].DescriptionEn != null ? Regex.Replace(informations[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(informations);
        }

        [HttpGet]
        public ActionResult CreateInformation()
        {
            return View();
        }

        public ActionResult CreateInformation(InformationModel model, HttpPostedFileBase file)
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

                var _Information = new Information();
                _Information.TitleEn = model.TitleEn;
                _Information.TitleAr = model.TitleAr;
                _Information.NewsDate = model.NewsDate;
                _Information.DescriptionEn = model.DescriptionEn;
                _Information.DescriptionAr = model.DescriptionAr;
                _Information.VideoLink = model.VideoLink;
                _Information.FileData = model.FileData;

                _Information.ImageID = model.ImageID;

                //if (image.ID != 0)
                //    _Information.ImageID = image.ID;

                informationService.InsertInformation(_Information);
                return RedirectToAction("InformationList");
            }
            return RedirectToAction("CreateInformation");
        }

        [HttpGet]
        public ActionResult UpdateInformation(int InformationID)
        {
            var _Information = informationService.GetInformation(InformationID);


            var informationModel = new InformationModel()
            {
                ID = _Information.ID,
                TitleEn = _Information.TitleEn,
                TitleAr = _Information.TitleAr,
                NewsDate=Convert.ToDateTime(_Information.NewsDate),
                DescriptionEn = _Information.DescriptionEn,
                DescriptionAr = _Information.DescriptionAr,
                VideoLink=_Information.VideoLink,
               // FileBinary = _Information.FileData != null ? _Information.FileData.FileBinary : null,
                ImageID = _Information.ImageID,
                FileData=_Information.FileData
            };
            return View(informationModel);
        }

        public ActionResult UpdateInformation(InformationModel model, HttpPostedFileBase file)
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
            else
            {

            }
            
            var _Information = new Information()
            {
                ID = model.ID,
                TitleEn = model.TitleEn,
                TitleAr = model.TitleAr,
                NewsDate=model.NewsDate,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                VideoLink=model.VideoLink,
                ImageID = model.ImageID,
                FileData=model.FileData
            };
            informationService.UpdateInformation(_Information);
            return RedirectToAction("InformationList");
        }

        public ActionResult DeleteInformation(int InformationID)
        {
            var information = informationService.GetInformation(InformationID);
            var fileID = information.ImageID;
            informationService.DeleteInformation(InformationID);

            if (fileID != null)
            {
                FileDataService.DeleteFileData(int.Parse(fileID.ToString()));
            }
            return RedirectToAction("InformationList");
        }
    }
}