using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Informations;
using DaleelElkheir.BLL.Services.Informations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class InformationController : ApiController
    {
        private readonly InformationService informationService;
        public InformationController(InformationService _informationService)
        {
            this.informationService = _informationService;
        }

        [HttpPost]
        public IHttpActionResult GetInformations(BaseRequest request)
        {
            if (ModelState.IsValid)
            {

                var informations = informationService.GetInformations();
                List<InformationModel> informationList = new List<InformationModel>();
                foreach (var item in informations)
                {
                    var _informationModel = new InformationModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        NewsDate=Convert.ToDateTime(item.NewsDate),
                        Description= request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Link=item.VideoLink,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                    };
                    informationList.Add(_informationModel);
                }
                return Ok(new BaseResponse(informationList));
            }
            return BadRequest(ModelState);
        }
    }
}
