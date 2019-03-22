using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.Hospitals
{
    public class HospitalModel
    {

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(100)]
        public string NameAr { get; set; }

        [StringLength(100)]
        public string TitleEn { get; set; }

        [StringLength(100)]
        public string TitleAr { get; set; }

        [Required]
        public int? CityID { get; set; }

        [Required]
        public int? GovernorateID { get; set; }

        [AllowHtml]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        public string DescriptionAr { get; set; }

    }

    public class HospitalContactModel
    {
        public int ID { get; set; }

        [Required]
        public int? HospitalID { get; set; }
        [Required]
        [StringLength(50)]
        public string ContactName { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        [StringLength(20)]
        public string ContactNumber { get; set; }

    }


}