using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaleelElkheir.Admin.Models.Guide;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Guides;
using DaleelElkheir.BLL.Services.Keywords;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.Admin.Controllers
{
    public class GuideController : Controller
    {
        readonly IGuideServices GuideServices;
        readonly IKeyworkServices KeyworkServices;
        readonly IFileDataService FileDataService;

        public GuideController(IGuideServices _guideServices, IKeyworkServices _KeyworkServices, IFileDataService _FileDataService)
        {
            this.GuideServices = _guideServices;
            this.KeyworkServices = _KeyworkServices;
            this.FileDataService = _FileDataService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var guides= GuideServices.GetGuide();
            return View(guides);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var AllGuide = GuideServices.GetGuide();
            return View();
        }

        [HttpPost]
        public ActionResult Create(GuideModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var _Guide = new Guide
                {
                    ID = model.ID,
                    Name = model.Name,
                    Description = model.Description,
                };

                if (file != null)
                {
                    string dir = Guid.NewGuid().ToString();
                    var originalName = Path.GetFileName(file.FileName);
                    _Guide.FileName = originalName;
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
                        _Guide.Ext = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                    }
                    catch
                    {
                        _Guide.Ext = null;
                    }
                }
                GuideServices.InsertGuide(_Guide);

                List<KeyWord> keyWords = new List<KeyWord>();
                string[] wordsArray = model.KeyWords.ToString().Split(' ');

                foreach (string word in wordsArray)
                {
                    KeyWord k = new KeyWord
                    {
                        GuideID = _Guide.ID,
                        Word = word,
                    };
                    keyWords.Add(k);
                }
                KeyworkServices.InsertKeyWord(keyWords);
                return RedirectToAction("index");
            }
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var guide = GuideServices.GetGuide(id);
            var keywords = KeyworkServices.GetKeyWord(x => x.GuideID == id);
            string words = "";
            foreach (KeyWord key in keywords)
            {
                words += key.Word + " ";
            }

            GuideModel model = new GuideModel
            {
                ID = guide.ID,
                Description = guide.Description,
                Name = guide.Name,
                KeyWords = words,
                FileName = guide.FileName,
                Ext = guide.Ext,
            };

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(GuideModel model, HttpPostedFileBase file)
        {
            var _Guide = new Guide
            {
                ID = model.ID,
                Name = model.Name,
                Description = model.Description,
                Ext = model.Ext,
                FileName= model.FileName,

            };

            if (file != null)
            {
                string dir = Guid.NewGuid().ToString();
                var originalName = Path.GetFileName(file.FileName);
                _Guide.FileName = originalName;
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
                    _Guide.Ext = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                }
                catch
                {
                    _Guide.Ext = null;
                }
            }

            var keys = KeyworkServices.GetKeyWord(x=>x.GuideID == _Guide.ID);
            KeyworkServices.DeleteKeyWords(keys);

            string[] wordsArray = model.KeyWords.ToString().Split(' ');
            List<KeyWord> keyWords = new List<KeyWord>();
            foreach (string word in wordsArray)
            {
                KeyWord k = new KeyWord
                {
                    GuideID = _Guide.ID,
                    Word = word,
                };
                keyWords.Add(k);
            }

            KeyworkServices.InsertKeyWord(keyWords);
            GuideServices.UpdateGuide(_Guide);

            return RedirectToAction("index");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            IEnumerable<KeyWord> keyWords = KeyworkServices.GetKeyWord(x => x.GuideID == id);
            KeyworkServices.DeleteKeyWords(keyWords);

            GuideServices.DeleteGuide(id);

            return RedirectToAction("index");
        }

    }
}