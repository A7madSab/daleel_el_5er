using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.CSRs;
using DaleelElkheir.BLL.Services.CSRs;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class CSRController : ApiController
    {
        private readonly ICSRService CSRService;
        public CSRController(ICSRService _CSRService)
        {
            this.CSRService = _CSRService;
        }

        [HttpPost]
        public IHttpActionResult GetCSRs(BaseRequest request)
        {
            if (ModelState.IsValid)
            {

                var _CSRs =  CSRService.GetCSRs();
                var _CSRActivity = CSRService.GetCSRActivitys();

                List<CSRActivityModel> CSRList = new List<CSRActivityModel>();
                foreach (var item in _CSRActivity)
                {
                    var csrModel = new CSRActivityModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Description= request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        companyImage = item.CompanySocialResponsibility.FileData!=null? item.CompanySocialResponsibility.FileData.Extenstion:null,
                        companyName= request.Lang == "ar" ? item.CompanySocialResponsibility.NameAr:item.CompanySocialResponsibility.NameEn,
                        ActivityDate= String.Format("{0:dd/MM/yyyy HH:mm tt}", item.ActivityDate.Value.ToString())
                    };
                    CSRList.Add(csrModel);
                }
                return Ok(new BaseResponse(CSRList));
            }
            return BadRequest(ModelState);
        }
    }
}
