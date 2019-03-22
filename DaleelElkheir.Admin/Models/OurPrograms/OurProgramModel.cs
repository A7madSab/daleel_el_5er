using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.OurPrograms
{
    public class OurProgramModel
    {

        public int ID { get; set; }

        [StringLength(200)]
        public string TitleEn { get; set; }

        [StringLength(200)]
        public string TitleAr { get; set; }

        [AllowHtml]
        [StringLength(300)]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        [StringLength(300)]
        public string DescriptionAr { get; set; }
    }
}