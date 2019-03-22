using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.BLL.Services.JobOffers;
using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.JobOffer;

namespace DaleelElkheir.API.Controllers
{
    [RoutePrefix("api/job")]
    public class JobOffersController : ApiController
    {
        public IJobOfferService jobOfferService;

        public JobOffersController() { }

        public JobOffersController(IJobOfferService JobOfferService)
        {
            this.jobOfferService = JobOfferService;
        }

        [HttpGet, Route("JobOffers")]
        public IHttpActionResult JobOffers()
        {
            var returnObj = jobOfferService.GetJobOffer().Select(x => new JobOfferModel
            {
                ID = x.ID,
                DescritpionAr = x.DescritpionAr,
                DescritpionEn = x.DescritpionEn,
                ContactInfo = x.ContactInfo,
                Employer = x.ContactInfo,
                JobTitle = x.JobTitle
            });
            return Ok( new BaseResponse( returnObj));
        }

        [HttpPost, Route("JobOffer")]
        public IHttpActionResult JobOffer(JobOfferModelRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var job = jobOfferService.GetJobOffer(request.ID);
            if (job == null)
            {
                return BadRequest("no Job Offer With this ID");
            }
            JobOfferModel returnObject = new JobOfferModel
            {
                ID = job.ID,
                ContactInfo = job.ContactInfo,
                DescritpionEn = job.DescritpionEn,
                DescritpionAr = job.DescritpionAr,
                Employer = job.Employer,
                JobTitle = job.JobTitle
            };

            return Ok(new BaseResponse(returnObject));
        }
    }
}