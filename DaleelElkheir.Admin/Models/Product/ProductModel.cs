using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.Product
{
    public class ProductModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? ProductCategory { get; set; }

        public int? Seller { get; set; }

        public string FileName { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string ProgramDescription { get; set; }

        public string Ext { get; set; }
        
    }
}