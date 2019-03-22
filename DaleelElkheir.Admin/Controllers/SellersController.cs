using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.BLL.Services.Sellers;
using DaleelElkheir.BLL.Services.Products;

namespace DaleelElkheir.Admin.Controllers
{
    public class SellersController : Controller
    {
        readonly ISellerServices SellerServices;
        readonly IProductServices productServices;

        public SellersController(ISellerServices _SellerServices, IProductServices _productServices)
        {
            SellerServices = _SellerServices;
            productServices = _productServices;
        }

        public ActionResult Index()
        {
            return View(SellerServices.GetSeller());
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(SellerServices.GetSeller(id));
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Seller seller)
        {
            SellerServices.InsertSeller(seller);
            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Seller seller)
        {
            SellerServices.UpdateSeller(seller);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = productServices.GetProduct(x => x.SellerID == id);

            if(product.Count == 0)
            {
                SellerServices.DeleteSeller(id);
            }
            return RedirectToAction("index");
        }
    }
}
