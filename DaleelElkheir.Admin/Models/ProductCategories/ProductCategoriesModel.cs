using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.ProductCategories
{
    public class ProductCategoriesModel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }
    }
}