using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.CSR
{
    public class CSRActivityModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleEn { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleAr { get; set; }

        [AllowHtml]
        [StringLength(400)]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        [StringLength(400)]
        public string DescriptionAr { get; set; }

        public int CSR_ID { get; set; }

        public string ActivityDate { get; set; }
    }
}