using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.TrusteesBoards;
using DaleelElkheir.BLL.Services.TrusteesBoards;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class TrusteesBoardController : ApiController
    {
        private readonly ITrusteesBoardService trusteesBoardService;
        public TrusteesBoardController(ITrusteesBoardService _TrusteesBoardService)
        {
            this.trusteesBoardService = _TrusteesBoardService;
        }

        [HttpPost]
        public IHttpActionResult GetTrusteesBoard(BaseRequest request)
        {
            if (ModelState.IsValid)
            {

                var TrusteesBoards = trusteesBoardService.GetTrusteesBoards();
                List<TrusteesBoardModel> TrusteesBoardList = new List<TrusteesBoardModel>();
                foreach (var item in TrusteesBoards)
                {
                    var BoardModel = new TrusteesBoardModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Name = request.Lang == "ar" ? item.NameAr : item.NameEn,
                        Image= item.FileData!=null? item.FileData.Extenstion:null,
                    };
                    TrusteesBoardList.Add(BoardModel);
                }
                return Ok(new BaseResponse(TrusteesBoardList));
            }
            return BadRequest(ModelState);
        }
    }
}
