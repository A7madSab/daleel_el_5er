using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Sponsors;
using DaleelElkheir.BLL.Services.Sponsors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class SponsorController : ApiController
    {
        private readonly ISponsorService sponsorService;
        public SponsorController(ISponsorService _sponsorService)
        {
            this.sponsorService = _sponsorService;
        }

        [HttpPost]
        public IHttpActionResult GetSponsor(BaseRequest request)
        {
            if (ModelState.IsValid)
            {

                var Sponsors = sponsorService.GetSponsors();
                List<SponsorModel> SponsorList = new List<SponsorModel>();
                foreach (var item in Sponsors)
                {
                    var _sponsorModel = new SponsorModel()
                    {
                        ID=item.ID,
                        Name = request.Lang == "ar" ? item.NameAr : item.NameEn,
                        Link=item.Link,
                        Image = item.FileData!=null? item.FileData.Extenstion:null,
                    };
                    SponsorList.Add(_sponsorModel);
                }
                return Ok(new BaseResponse(SponsorList));
            }
            return BadRequest(ModelState);
        }
    }
}
