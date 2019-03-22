using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.CSRs;
using DaleelElkheir.BLL.Services.CSRs;

namespace DaleelElkheir.API.Controllers
{
    [RoutePrefix("api/CSR")]
    public class CSRModelsController : ApiController
    {
        private readonly ICSRService cSRService;

        public CSRModelsController(ICSRService _ICSRService)
        {
            this.cSRService = _ICSRService;
        }

        // GET: api/CSRCompanyModels
        [Route("get")]
        public IHttpActionResult GetCSRCompanyModels()
        {
            var returnObj = cSRService.GetCSRs().Select(x => new CSRModel
            {
                ID = x.ID,
                Category = x.Category,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                Image = x.FileData != null ? x.FileData.Extenstion.ToString() : null,
            });

            return Ok(new BaseResponse(returnObj));
        }

        [Route("get/{id}")]
        public IHttpActionResult GetCSRCompanyModel(int id)
        {
            var csrModel = cSRService.GetCSR(id);

            CSRModel ReturnObj = new CSRModel
            {
                ID = csrModel.ID,
                Category = csrModel.Category,
                Image = csrModel.FileData != null ? csrModel.FileData.Extenstion.ToString() : null,
                NameAr = csrModel.NameAr,
                NameEn = csrModel.NameEn
            };

            return Ok(new BaseResponse(ReturnObj));
        }

        [HttpGet, Route("getSchool")]
        public IHttpActionResult getSchool()
        {
            var school = cSRService.GetCSRs().Where(x => x.Category == "School").Select(x => new CSRModel
            {
                ID = x.ID,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                Category = x.Category,
                Image = x.FileData != null ? x.FileData.Extenstion.ToString() : null
            });
            return Ok(new BaseResponse(school));
        }

        [HttpGet, Route("getCompany")]
        public IHttpActionResult getCompany()
        {
            var Company = cSRService.GetCSRs().Where(x => x.Category == "Company").Select(x => new CSRModel
            {
                ID = x.ID,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                Category = x.Category,
                Image = x.FileData != null ? x.FileData.Extenstion.ToString() : null
            });

            return Ok(new BaseResponse(Company));
        }

        [HttpGet, Route("getUniversity")]
        public IHttpActionResult getUniversity()
        {
            var University = cSRService.GetCSRs().Where(x => x.Category == "University").Select(x => new CSRModel
            {
                ID = x.ID,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                Category = x.Category,
                Image = x.FileData != null ? x.FileData.Extenstion.ToString() : null
            });

            return Ok(new BaseResponse(University));
        }

        [HttpPost, Route("GetCSRsByCategory")]
        public IHttpActionResult GetCSRsByCategory(BaseRequestString category)
        {
            if (category.body == "Company" || category.body == "University" || category.body == "School")
            {
                var ReturnObj =cSRService.GetCSR(x => x.Category == category.body).Select(y => new CSRModel
                {
                    ID = y.ID,
                    NameAr = y.NameAr,
                    NameEn = y.NameEn,
                    Image = y.FileData == null ? null : y.FileData.Extenstion,
                    Category = y.Category
                });
                return Ok(new BaseResponse(ReturnObj));

            }
            else
            {
                return BadRequest("no category match");
            }
        }
    }
}