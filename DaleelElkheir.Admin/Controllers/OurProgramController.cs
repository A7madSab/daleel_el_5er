using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.OurPrograms;
using DaleelElkheir.BLL.Services.OurPrograms;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class OurProgramController : Controller
    {
        private readonly IOurProgramService OurProgramService;
        public OurProgramController(IOurProgramService _OurProgramService)
        {
            this.OurProgramService = _OurProgramService;
        }
        public ActionResult OurProgramList()
        {

            var OurPrograms = OurProgramService.GetOurPrograms();

            for (int i = 0; i < OurPrograms.Count(); i++)
            {
                OurPrograms[i].DescriptionAr = OurPrograms[i].DescriptionAr != null ? Regex.Replace(OurPrograms[i].DescriptionAr, @"<[^>]*>", "") : "";
                OurPrograms[i].DescriptionEn = OurPrograms[i].DescriptionEn != null ? Regex.Replace(OurPrograms[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(OurPrograms);
        }

        [HttpGet]
        public ActionResult CreateOurProgram()
        {

            return View();
        }

        public ActionResult CreateOurProgram(OurProgramModel model)
        {
            if (ModelState.IsValid)
            {
                var _OurProgram = new OurProgram()
                {
                    TitleEn = model.TitleEn,
                    TitleAr = model.TitleAr,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionAr = model.DescriptionAr,
                    
                };

                OurProgramService.InsertOurProgram(_OurProgram);
                return RedirectToAction("OurProgramList");
            }
            return RedirectToAction("CreateOurProgram");
        }

        [HttpGet]
        public ActionResult UpdateOurProgram(int OurProgramID)
        {
            var _OurProgram = OurProgramService.GetOurProgram(OurProgramID);


            var ourProgramModel = new OurProgramModel()
            {
                ID = _OurProgram.ID,
                TitleEn = _OurProgram.TitleEn,
                TitleAr = _OurProgram.TitleAr,
                DescriptionEn = _OurProgram.DescriptionEn,
                DescriptionAr = _OurProgram.DescriptionAr,
            };
            return View(ourProgramModel);
        }

        public ActionResult UpdateOurProgram(OurProgramModel model)
        {
            var _OurProgram = new OurProgram()
            {
                ID = model.ID,
                TitleEn = model.TitleEn,
                TitleAr = model.TitleAr,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,

            };
            OurProgramService.UpdateOurProgram(_OurProgram);
            return RedirectToAction("OurProgramList");
        }

        public ActionResult DeleteOurProgram(int OurProgramID)
        {
            OurProgramService.DeleteOurProgram(OurProgramID);
            return RedirectToAction("OurProgramList");
        }
    }
}