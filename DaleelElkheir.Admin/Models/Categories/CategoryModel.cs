using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.Categories
{
    public class CategoryModel
    {

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(100)]
        public string NameAr { get; set; }

        [AllowHtml]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        public string DescriptionAr { get; set; }
    }
}