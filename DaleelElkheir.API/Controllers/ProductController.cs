using DaleelElkheir.API.Models.ProductCategories;
using DaleelElkheir.BLL.Services.ProductCategories;
using DaleelElkheir.BLL.Services.Products;
using DaleelElkheir.BLL.Services.Sellers;
using DaleelElkheir.DAL.Domain;
using System.Linq;
using System.Web.Http;

namespace DaleelElkheir.API.Models
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        ProductServices productServices;
        ProductCategoryServices productCategoryServices;
        SellerServices sellerServices;

        public ProductController(ProductServices _productServices, 
            ProductCategoryServices _productCategoryServices, SellerServices _sellerServices)
        {
            productServices = _productServices;
            productCategoryServices = _productCategoryServices;
            sellerServices = _sellerServices;
        }

        [HttpGet,Route("getProducts")]
        public IHttpActionResult getProducts()
        {
            var productModel = productServices.GetProduct().Select(x => new ProductModel
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description,
                ProgramDescription = x.ProgramDescription,
                SellerID = x.Seller.ID,
                SellerName = x.Seller.Name,
                CategoryID =x.Category.ID,
                CategoryName = x.Category.Name,
            });
            return Ok(new BaseResponse(productModel));
        }

        [HttpPost, Route("Products")]
        public IHttpActionResult AddProducts(ProductModel model)
        {
            if (productCategoryServices.GetProductCategory(model.CategoryID) == null)
            {
                return BadRequest("Category Not Found");
            }
            else if (sellerServices.GetSeller(model.SellerID) == null)
            {
                return BadRequest("Seller Not Found");
            }

            var product = new Product
            {
                ID = model.ID,
                Description = model.Description,
                Name = model.Name,
                ProgramDescription = model.ProgramDescription,
                SellerID = model.SellerID,
                CategoryID = model.CategoryID,
                FileName = model.FileName,
                Ext = model.Ext,
            };

            productServices.InsertProduct(product);

            return Ok(new BaseResponse(product));
        }
    }
}
