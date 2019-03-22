using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.ProductCategories;
using DaleelElkheir.BLL.Services.ProductCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    [RoutePrefix("api/ProductCategory")]
    public class ProductCategoryController : ApiController
    {
        ProductCategoryServices productCategoryServices;

        public ProductCategoryController(ProductCategoryServices _productCategoryServices)
        {
            productCategoryServices = _productCategoryServices;
        }

        
        [HttpGet, Route("getProductCategory")]
        public IHttpActionResult getProductCategory()
        {
            var productCategories = productCategoryServices.GetProductCategory().Select(x => new ProductCategoryModel
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description
            });

            return Ok(new BaseResponse(productCategories));
        }

    }
}
