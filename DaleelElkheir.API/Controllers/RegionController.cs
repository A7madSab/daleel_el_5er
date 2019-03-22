using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Regions;
using DaleelElkheir.BLL.Services.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class RegionController : ApiController
    {

        private readonly IRegionService RegionService;

        public RegionController(IRegionService _RegionService)
        {
            this.RegionService = _RegionService;
        }

        [HttpPost]
        public IHttpActionResult GetGovernorate(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                var regions = RegionService.GetGovernorates().Select(x => new { ID = x.ID, Name = model.Lang == "ar" ? x.NameAr : x.NameEn });
                return Ok(new BaseResponse(regions));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetRegions(GetRegionModel model)
        {
            if(ModelState.IsValid)
            {
                var regions = RegionService.GetRegions(w=>w.GovernorateID==model.GovernorateID).Select(x=>new{ID=x.ID,Name=model.Lang=="ar"?x.NameAr: x.NameEn });
                return Ok(new BaseResponse(regions));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetAreas(GetAreaModel model)
        {
            if (ModelState.IsValid)
            {
                var regions = RegionService.GetAreas(model.CityID).Select(x => new { ID = x.ID, Name = model.Lang == "ar" ? x.NameAr : x.NameEn });
                return Ok(new BaseResponse(regions));
            }
            return BadRequest(ModelState);
        }
    }
}
