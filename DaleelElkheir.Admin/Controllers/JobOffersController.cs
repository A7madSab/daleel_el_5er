using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.BLL.Services;
using DaleelElkheir.BLL.Services.JobOffers;
using DaleelElkheir.Admin.Models.JobOffer;

namespace DaleelElkheir.Admin.Controllers
{
    public class JobOffersController : Controller
    {
        private IJobOfferService JobOfferService;

        public JobOffersController(IJobOfferService JobOfferService)
        {
            this.JobOfferService = JobOfferService;
        }

        public ActionResult Index()
        {
            var jobOffers = JobOfferService.GetJobOffer();
            return View(jobOffers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobOfferModel jobOffer)
        {
            var jobOfferDTO = new JobOffer
            {
                DescritpionAr = jobOffer.DescritpionAr,
                DescritpionEn = jobOffer.DescritpionEn,
                ID = jobOffer.ID,
                ContactInfo = jobOffer.ContactInfo,
                Employer = jobOffer.Employer,
                JobTitle = jobOffer.JobTitle
            };

            JobOfferService.InsertJobOffer(jobOfferDTO);
            return RedirectToAction("Index", "JobOffers");
        }


        public ActionResult Delete(int id)
        {
            JobOffer jobOffer = JobOfferService.GetJobOffer(id);
            return View(jobOffer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(JobOffer jobOffer)
        {
            JobOfferService.DeleteJobOffer(jobOffer.ID);
            return RedirectToAction("Index", "JobOffers");
        }

        public ActionResult Edit(int id)
        {
            JobOffer jobOffer = JobOfferService.GetJobOffer(id);
            return View(jobOffer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobOfferModel jobOfferModel)
        {
            JobOffer jobOffer = new JobOffer
            {
                ID = jobOfferModel.ID,
                Employer = jobOfferModel.Employer,
                JobTitle = jobOfferModel.JobTitle,
                ContactInfo = jobOfferModel.ContactInfo,
                DescritpionAr = jobOfferModel.DescritpionAr,
                DescritpionEn = jobOfferModel.DescritpionEn,
            };
            JobOfferService.UpdateJobOffer(jobOffer);
            return RedirectToAction("Index", "JobOffers");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            JobOffer jobOffer = JobOfferService.GetJobOffer(id);
            return View(jobOffer);
        }
    }
}
