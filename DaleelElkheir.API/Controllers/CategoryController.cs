using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Categories;
using DaleelElkheir.BLL.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DaleelElkheir.API.Controllers
{
    
    public class CategoryController : ApiController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }

        [HttpPost]
        public IHttpActionResult GetCategory(BaseRequest request)
        {
            if(ModelState.IsValid)
            {

                var categories=categoryService.GetCategories();
                List<CategoryModel> categoryList = new List<CategoryModel>();
                foreach(var item in categories)
                {
                    var CateModel = new CategoryModel() {
                         ID=item.ID,
                         Name=request.Lang=="ar"?item.NameAr:item.NameEn
                    };
                    categoryList.Add(CateModel);
                }
                return Ok(new BaseResponse(categoryList));
            }
            return BadRequest(ModelState);
        }
    }
}
