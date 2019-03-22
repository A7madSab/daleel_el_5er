using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.CSR
{
    public class CSRCompanyModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; }

        [Required]
        [StringLength(200)]
        public string NameEn { get; set; }

        public string Category { get; set; }

        public int? FileID { get; set; }

        public virtual FileData FileData { get; set; }

    }
}