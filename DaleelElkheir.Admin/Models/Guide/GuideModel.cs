using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.Guide
{
    public class GuideModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [StringLength(200)]
        public string FileName { get; set; }

        [StringLength(400)]
        public string Ext { get; set; }

        [AllowHtml]
        public string KeyWords { get; set; }

        public virtual FileData FileData { get; set; }
    }
}