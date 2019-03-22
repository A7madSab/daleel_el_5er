using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.Admin.Models.ProductCategories;
using DaleelElkheir.BLL.Services.ProductCategories;
using DaleelElkheir.BLL.Services.Products;

namespace DaleelElkheir.Admin.Controllers
{
    public class ProductCategoriesController : Controller
    {
        IProductCategoryServices productCategoryServices;
        IProductServices productServices;

        public ProductCategoriesController(IProductCategoryServices _productCategoryServices, IProductServices _productServices)
        {
            productCategoryServices = _productCategoryServices;
            productServices = _productServices;
        }
        
        public ActionResult Index()
        {
            return View(productCategoryServices.GetProductCategory());
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoriesModel productCategoryModel)
        {
            if(!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var productCategory = new ProductCategory
            {
                Description = productCategoryModel.Description,
                ID = productCategoryModel.ID,
                Name = productCategoryModel.Name
            };

            productCategoryServices.InsertProductCategory(productCategory);
            return RedirectToAction("index");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = productCategoryServices.GetProductCategory(id);

            var returnObj = new ProductCategoriesModel
            {
                ID = model.ID,
                Description = model.Description,
                Name = model.Name,
            };
            return View(returnObj);
        }
        

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategoriesModel productCategoryMdoel)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var productCategory = new ProductCategory
            {
                ID = productCategoryMdoel.ID,
                Description = productCategoryMdoel.Description,
                Name = productCategoryMdoel.Name,
            };
            productCategoryServices.UpdateProductCategory(productCategory);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = productServices.GetProduct(x => x.CategoryID == id);

            if (product.Count == 0)
            {
                productCategoryServices.DeleteProductCategory(id);
            }
            return RedirectToAction("index");
        }
    }
}
