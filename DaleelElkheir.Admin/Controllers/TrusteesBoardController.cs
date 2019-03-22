using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.TrusteesBoards;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.TrusteesBoards;
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
    public class TrusteesBoardController : Controller
    {
        private readonly ITrusteesBoardService trusteesBoardService;
        private readonly IFileDataService FileDataService;
        public TrusteesBoardController(ITrusteesBoardService _TrusteesBoardService, IFileDataService _FileDataService)
        {
            this.trusteesBoardService = _TrusteesBoardService;
            this.FileDataService = _FileDataService;
        }
        public ActionResult TrusteesBoardList()
        {

            var TrusteesBoards = trusteesBoardService.GetTrusteesBoards();

            return View(TrusteesBoards);
        }

        [HttpGet]
        public ActionResult CreateTrusteesBoard()
        {
            return View();
        }

        public ActionResult CreateTrusteesBoard(TrusteesBoardModel model, HttpPostedFileBase file)
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

                    var _Trustees = new TrusteesBoard();
                _Trustees.NameEn = model.NameEn;
                _Trustees.NameAr = model.NameAr;
                _Trustees.TitleEn = model.TitleEn;
                _Trustees.TitleAr = model.TitleAr;
                _Trustees.FileData = model.FileData;
                _Trustees.ImageID = model.ImageID;
                //if (image.ID != 0)
                //    _Trustees.ImageID = image.ID;

                trusteesBoardService.InsertTrusteesBoard(_Trustees);
                return RedirectToAction("TrusteesBoardList");
            }
            return RedirectToAction("CreateTrusteesBoard");
        }

        [HttpGet]
        public ActionResult UpdateTrusteesBoard(int BoardID)
        {
            var _TrusteesBoard = trusteesBoardService.GetTrusteesBoard(BoardID);


            var _TrusteesBoardModel = new TrusteesBoardModel()
            {
                ID = _TrusteesBoard.ID,
                NameEn = _TrusteesBoard.NameEn,
                NameAr = _TrusteesBoard.NameAr,
                TitleEn=_TrusteesBoard.TitleEn,
                TitleAr=_TrusteesBoard.TitleAr,
               // FileBinary = _TrusteesBoard.FileData != null ? _TrusteesBoard.FileData.FileBinary : null,
                ImageID = _TrusteesBoard.ImageID,
                FileData=_TrusteesBoard.FileData
            };
            return View(_TrusteesBoardModel);
        }

        public ActionResult UpdateTrusteesBoard(TrusteesBoardModel model, HttpPostedFileBase file)
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
            var _TrusteesBoard = new TrusteesBoard()
            {
                ID = model.ID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                TitleEn=model.TitleEn,
                TitleAr=model.TitleAr,
                ImageID = model.ImageID,
                FileData=model.FileData

            };
            trusteesBoardService.UpdateTrusteesBoard(_TrusteesBoard);
            return RedirectToAction("TrusteesBoardList");
        }

        public ActionResult DeleteTrusteesBoard(int BoardID)
        {
            var trusteesBoard = trusteesBoardService.GetTrusteesBoard(BoardID);
            var fileID = trusteesBoard.ImageID;
            trusteesBoardService.DeleteTrusteesBoard(BoardID);

            if (fileID != null)
            {
                FileDataService.DeleteFileData(int.Parse(fileID.ToString()));
            }
            return RedirectToAction("TrusteesBoardList");
        }
    }
}