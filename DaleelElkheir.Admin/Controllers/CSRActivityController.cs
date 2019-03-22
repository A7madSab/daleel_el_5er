using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.CSR;
using DaleelElkheir.BLL.Services.CSRs;
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
    public class CSRActivityController : Controller
    {
        private readonly ICSRService CSR_Service;
        public CSRActivityController(ICSRService _CSRService)
        {
            this.CSR_Service = _CSRService;

            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

        }
        public ActionResult CSRActivityList()
        {

            var CSRActivities = CSR_Service.GetCSRActivitys();

            for (int i = 0; i < CSRActivities.Count(); i++)
            {
                CSRActivities[i].DescriptionAr = CSRActivities[i].DescriptionAr != null ? Regex.Replace(CSRActivities[i].DescriptionAr, @"<[^>]*>", "") : "";
                CSRActivities[i].DescriptionEn = CSRActivities[i].DescriptionEn != null ? Regex.Replace(CSRActivities[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(CSRActivities);
        }

        [HttpGet]
        public ActionResult CreateCSRActivity()
        {
            IList<SelectListItem> CSR_List = CSR_Service.GetCSRs().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CSR_List.Insert(0, new SelectListItem { Text = "select CSRCompany", Value = "" });
            ViewBag.CSRList = CSR_List;

            return View();
        }

        public ActionResult CreateCSRActivity(CSRActivityModel model)
        {
            if (ModelState.IsValid)
            {
                var _CSRActivity = new CSRActivity()
                {
                    TitleEn = model.TitleEn,
                    TitleAr = model.TitleAr,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionAr = model.DescriptionAr,
                    CSR_ID=model.CSR_ID,
                    ActivityDate=Convert.ToDateTime(model.ActivityDate)

                };

                CSR_Service.InsertCSRActivity(_CSRActivity);
                return RedirectToAction("CSRActivityList");
            }
            return RedirectToAction("CreateCSRActivity");
        }

        [HttpGet]
        public ActionResult UpdateCSRActivity(int CSRActivityID)
        {

            IList<SelectListItem> CSR_List = CSR_Service.GetCSRs().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NameEn }).ToList();
            CSR_List.Insert(0, new SelectListItem { Text = "select CSRCompany", Value = "" });
            ViewBag.CSRList = CSR_List;

            var _CSRActivity = CSR_Service.GetCSRActivity(CSRActivityID);


            var _CSRActivityModel = new CSRActivityModel()
            {
                ID = _CSRActivity.ID,
                TitleEn = _CSRActivity.TitleEn,
                TitleAr = _CSRActivity.TitleAr,
                DescriptionEn = _CSRActivity.DescriptionEn,
                DescriptionAr = _CSRActivity.DescriptionAr,
                ActivityDate= _CSRActivity.ActivityDate!=null?_CSRActivity.ActivityDate.Value.ToShortDateString():null,
                CSR_ID=_CSRActivity.CSR_ID
            };
            return View(_CSRActivityModel);
        }

        public ActionResult UpdateCSRActivity(CSRActivityModel model)
        {
            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;


            var _CSRActivity = new CSRActivity()
            {
                ID = model.ID,
                TitleEn = model.TitleEn,
                TitleAr = model.TitleAr,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
                ActivityDate =Convert.ToDateTime(model.ActivityDate),
                CSR_ID = model.CSR_ID
            };
            CSR_Service.UpdateCSRActivity(_CSRActivity);
            return RedirectToAction("CSRActivityList");
        }

        public ActionResult DeleteCSRActivity(int CSRActivityID)
        {
            CSR_Service.DeleteCSRActivity(CSRActivityID);
            return RedirectToAction("CSRActivityList");
        }

    }
}