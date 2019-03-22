using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.CSR;
using DaleelElkheir.BLL.Services.CSRs;
using DaleelElkheir.BLL.Services.FilesData;
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
    public class CSRCompanyController : Controller
    {
        private readonly ICSRService CSR_Service;
        private readonly IFileDataService FileDataService;
        public CSRCompanyController(ICSRService _ICSRService, IFileDataService _FileDataService)
        {
            this.CSR_Service = _ICSRService;
            this.FileDataService = _FileDataService;
        }
        public ActionResult CSRList()
        {

            var CSRs = CSR_Service.GetCSRs();
            return View(CSRs);
        }

        [HttpGet]
        public ActionResult CreateCSR()
        {
            IList<SelectListItem> CategoryList = new List<SelectListItem>();
            CategoryList.Add(new SelectListItem { Value = "School", Text = "School" });
            CategoryList.Add(new SelectListItem { Value = "Company", Text = "Company" });
            CategoryList.Add(new SelectListItem { Value = "University", Text = "University" });

            CategoryList.Insert(0, new SelectListItem { Text = "select Category", Value = "" });
            ViewBag.CategoryList = CategoryList;

            return View();
        }

        public ActionResult CreateCSR(CSRCompanyModel model, HttpPostedFileBase file)
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
                    model.FileID = request.ID;
                }

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

                //    FileDataService.InsertFileData(image);
                //}

                var _CSR = new CompanySocialResponsibility();

                _CSR.NameEn = model.NameEn;
                _CSR.NameAr = model.NameAr;
                _CSR.Category = model.Category;
                _CSR.FileID = model.FileID;
                _CSR.FileData = model.FileData;
                //if(image.ID!=0)
                //_CSR.FileID = image.ID;
                

                CSR_Service.InsertCSR(_CSR);
                return RedirectToAction("CSRList");
            }
            return RedirectToAction("CreateCSR");
        }

        [HttpGet]
        public ActionResult UpdateCSR(int CSRID)
        {
            var _CSR = CSR_Service.GetCSR(CSRID);

            IList<SelectListItem> CategoryList = new List<SelectListItem>();
            CategoryList.Add(new SelectListItem { Value = "School", Text = "School" });
            CategoryList.Add(new SelectListItem { Value = "Company", Text = "Company" });
            CategoryList.Add(new SelectListItem { Value = "University", Text = "University" });

            CategoryList.Insert(0, new SelectListItem { Text = "select Category", Value = "" });
            ViewBag.CategoryList = CategoryList;

            var _CSRCompanyModel = new CSRCompanyModel()
            {
                ID = _CSR.ID,
                NameEn = _CSR.NameEn,
                NameAr = _CSR.NameAr,
                Category = _CSR.Category,
                // FileBinary = _CSR.FileData != null ? _CSR.FileData.FileBinary : null,
                FileID =_CSR.FileID,
                FileData=_CSR.FileData

            };
            return View(_CSRCompanyModel);
        }

        public ActionResult UpdateCSR(CSRCompanyModel model, HttpPostedFileBase file)
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
            if (model.FileID != null)
            {
                request.ID = int.Parse(model.FileID.ToString());
                FileDataService.UpdateFileData(request);
            }
            else
            {
                FileDataService.InsertFileData(request);
                model.FileID = request.ID;
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

               
            //    if (model.FileID != null)
            //    {
            //        image = new FileData()
            //        {
            //            ID = int.Parse(model.FileID.ToString()),
            //            Name = objFiles.FileName,
            //            FileBinary = imageData
            //        };

            //        FileDataService.UpdateFileData(image);
            //    }
            //    else 
            //    {
            //        image = new FileData()
            //        {
            //            Name = objFiles.FileName,
            //            FileBinary = imageData
            //        };

            //        FileDataService.InsertFileData(image);
            //    }

            //}
           

            var _CSR = new CompanySocialResponsibility()
            {
                ID = model.ID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                Category = model.Category,
                FileID = model.FileID,
                FileData=model.FileData
            };
            CSR_Service.UpdateCSR(_CSR);
            return RedirectToAction("CSRList");
        }

        public ActionResult DeleteCSR(int CSRID)
        {

            var CSRActivies = CSR_Service.GetCSRActivity(x => x.CSR_ID == CSRID);

            if (CSRActivies.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var csr = CSR_Service.GetCSR(CSRID);
                var fileID = csr.FileID;
                CSR_Service.DeleteCSR(CSRID);

                if (fileID != null)
                {
                    FileDataService.DeleteFileData(int.Parse(fileID.ToString()));
                }

                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("CSRList");
            }


        }
    }
}