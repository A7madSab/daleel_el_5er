using DaleelElkheir.Admin.Models.Seller;
using DaleelElkheir.API.Models;
using DaleelElkheir.BLL.Services.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    [RoutePrefix("api/seller")]
    public class SellerController : ApiController
    {
        SellerServices sellerServices;

        public SellerController(SellerServices _sellerServices)
        {
            sellerServices = _sellerServices;
        }


        [HttpGet, Route("getSellers")]
        public IHttpActionResult getProducts()
        {
            var seller = sellerServices.GetSeller();

            var sellerObj = seller.Select(x => new SellerModel
            {
                ID = x.ID,
                Contract = x.Contract,
                Link = x.Link,
                Name = x.Name,
            }).ToList();

            return Ok(new BaseResponse(sellerObj));
        }
    }
}
