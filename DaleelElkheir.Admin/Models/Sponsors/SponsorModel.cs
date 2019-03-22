using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Sponsors
{
    public class SponsorModel
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string NameEn { get; set; }

        [StringLength(200)]
        public string NameAr { get; set; }

        [StringLength(50)]
        public string Link { get; set; }

        public int? ImageID { get; set; }

        public virtual FileData FileData { get; set; }
    }
}