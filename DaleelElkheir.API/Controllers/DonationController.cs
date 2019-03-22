using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Donations;
using DaleelElkheir.BLL.Services.Donations;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    [RoutePrefix("api/donation")]
    public class DonationController : ApiController
    {
        private readonly IDonationService donationService;

        public DonationController()
        {
        }

        public DonationController(IDonationService _donationService)
        {
            this.donationService = _donationService;
        }

        [HttpGet, Route("get")]
        public IHttpActionResult Get()
        {
            var donations = donationService.GetDonations();
            return Ok(new BaseResponse (donations));
        }

        [HttpPost, Route("donate")]
        public IHttpActionResult Donate(DonationDTO donationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var dontion = new Donation
                {
                    Contact = donationDTO.Contact,
                    Name = donationDTO.Name
                };
                donationService.InsertDonation(dontion);
                return Ok(new BaseResponse( dontion));
            }
        }
    }
}