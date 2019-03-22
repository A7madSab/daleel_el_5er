using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DaleelElkheir.Admin.Models.Product;
using DaleelElkheir.BLL.Services.Products;
using DaleelElkheir.BLL.Services.Sellers;
using DaleelElkheir.BLL.Services.ProductCategories;
using DaleelElkheir.DAL.Domain;
using System.IO;
using System.Configuration;

namespace DaleelElkheir.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IProductServices productServices;
        public ISellerServices sellerService;
        public IProductCategoryServices productCategoryServices;

        public ProductController(IProductServices _productServices, ISellerServices _sellerService, IProductCategoryServices _productCategoryServices)
        {
            productServices = _productServices;
            sellerService = _sellerService;
            productCategoryServices = _productCategoryServices;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var ProductDisplay = productServices.GetProduct().Select( x => new ProductDisplay
            {
                ID = x.ID,
                Description = x.Description,
                FileName = x.FileName,
                Name = x.Name,
                SellerName = x.Seller.Name,
                ProgramDescription = x.ProgramDescription,
                ProductCategoryName = x.Category.Name,
                ProductCategorydesc= x.Category.Description,
            });
            return View(ProductDisplay);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> categoryList = productCategoryServices.GetProductCategory().Select(x=> new SelectListItem{ Value = x.ID.ToString() , Text = x.Name }).ToList();
            categoryList.Insert(0, new SelectListItem { Text = "select Product Cateogry", Value = "" });
            ViewBag.categoryList = categoryList;

            List<SelectListItem> SellerList = sellerService.GetSeller().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            SellerList.Insert(0, new SelectListItem { Text = "select Seller", Value = "" });
            ViewBag.SellerList = SellerList;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel productModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ID = productModel.ID,
                    Name = productModel.Name,
                    Description = productModel.Description,
                    ProgramDescription = productModel.ProgramDescription,
                    CategoryID = productModel.ProductCategory ?? null,
                    SellerID = productModel.Seller??null,
                };

                if (file != null)
                {
                    string dir = Guid.NewGuid().ToString();
                    var originalName = Path.GetFileName(file.FileName);
                    product.FileName = originalName;
                    var root = Server.MapPath("~/UploadedFiles");
                    root += "/" + dir;

                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    else
                    {
                        Directory.Delete(root, true);
                        Directory.CreateDirectory(root);
                    }
                    file.SaveAs(Path.Combine(root, originalName));

                    try
                    {
                        product.Ext = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                    }
                    catch
                    {
                        product.Ext = null;
                    }
                }

                productServices.InsertProduct(product);
                return RedirectToAction("index");
            }
            return View(ModelState);
        }

        public ActionResult Edit(int id)
        {
            List<SelectListItem> categoryList = productCategoryServices.GetProductCategory().Select(n => new SelectListItem { Value = n.ID.ToString(), Text = n.Name }).ToList();
            categoryList.Insert(0, new SelectListItem { Text = "select Product Cateogry", Value = "" });
            ViewBag.categoryList = categoryList;

            List<SelectListItem> SellerList = sellerService.GetSeller().Select(n => new SelectListItem { Value = n.ID.ToString(), Text = n.Name }).ToList();
            SellerList.Insert(0, new SelectListItem { Text = "select Seller", Value = "" });
            ViewBag.SellerList = SellerList;

            var x = productServices.GetProduct(id);

            var productModel = new ProductModel
            {
                ID = x.ID,
                Description = x.Description,
                Ext = x.Ext,
                FileName = x.FileName,
                Name = x.Name,
                ProductCategory = x.Category.ID,
                ProgramDescription = x.ProgramDescription,
                Seller = x.Seller.ID,
            };

            return View(productModel);
        }
        
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel productModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ID = productModel.ID,
                    Name = productModel.Name,
                    Description = productModel.Description,
                    ProgramDescription = productModel.ProgramDescription,
                    CategoryID = productModel.ProductCategory.Value,
                    SellerID = productModel.Seller.Value,
                    Ext = productModel.Ext,
                    FileName = productModel.FileName,
                };

                if (file != null)
                {
                    string dir = Guid.NewGuid().ToString();
                    var originalName = Path.GetFileName(file.FileName);
                    product.FileName = originalName;
                    var root = Server.MapPath("~/UploadedFiles");
                    root += "/" + dir;

                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    else
                    {
                        Directory.Delete(root, true);
                        Directory.CreateDirectory(root);
                    }
                    file.SaveAs(Path.Combine(root, originalName));

                    try
                    {
                        product.Ext = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
                    }
                    catch
                    {
                        product.Ext = null;
                    }
                }
                productServices.UpdateProduct(product);
                return RedirectToAction("index");
            }
            return RedirectToAction("Edit");
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            productServices.DeleteProduct(id);
            return RedirectToAction("index");
        }
    }
}
