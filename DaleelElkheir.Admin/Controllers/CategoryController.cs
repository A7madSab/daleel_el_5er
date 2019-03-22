using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Categories;
using DaleelElkheir.BLL.Services.Categories;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IOrganizationService organizationService;
        private readonly IUserService userService;

        public CategoryController(ICategoryService _categoryService, IOrganizationService _organizationService, IUserService _userService)
        {
            this.categoryService = _categoryService;
            this.organizationService = _organizationService;
            this.userService = _userService;
           
        }
        public ActionResult CategoryList()
        {

            var categories = categoryService.GetCategories();

            for (int i = 0; i < categories.Count(); i++)
            {
                categories[i].DescriptionAr = categories[i].DescriptionAr != null ? Regex.Replace(categories[i].DescriptionAr, @"<[^>]*>", "") : "";
                categories[i].DescriptionEn = categories[i].DescriptionEn != null ? Regex.Replace(categories[i].DescriptionEn, @"<[^>]*>", "") : "";
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {

            return View();
        }

        public ActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var _category = new Category()
                {
                    NameEn = model.NameEn,
                    NameAr = model.NameAr,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionAr = model.DescriptionAr,
                };

                categoryService.InsertCategory(_category);
                return RedirectToAction("CategoryList");
            }
            return RedirectToAction("CreateCategory");
        }

        [HttpGet]
        public ActionResult UpdateCategory(int categoryID)
        {
            var _category = categoryService.GetCategory(categoryID);


            var categoryModel = new CategoryModel()
            {
                ID = _category.ID,
                NameEn = _category.NameEn,
                NameAr = _category.NameAr,
                DescriptionEn = _category.DescriptionEn,
                DescriptionAr = _category.DescriptionAr,
            };
            return View(categoryModel);
        }

        public ActionResult UpdateCategory(CategoryModel model)
        {
            var _category = new Category()
            {
                ID = model.ID,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                DescriptionEn = model.DescriptionEn,
                DescriptionAr = model.DescriptionAr,
              
            };
            categoryService.UpdateCategory(_category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int categoryID)
        {

            var followCategory = userService.GetUserCategories(x => x.CategoryID == categoryID);
            foreach (var cat in followCategory)
            {
                userService.DeleteUserCategory(cat.ID);
            }

            var Categories = organizationService.GetOrganizationCategorys(x => x.CategoryID == categoryID);


            if (Categories.Count > 0)
            {
                return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                categoryService.DeleteCategory(categoryID);

                return Json(new { result = true, message = "Successful delete" }, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("CategoryList");
            }
        }
    }
}