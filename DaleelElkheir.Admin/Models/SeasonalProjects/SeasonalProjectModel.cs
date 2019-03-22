using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.SeasonalProjects
{
    public class SeasonalProjectModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; }
    }
    public class SeasonalProjectActivityModel
    {

        public int ID { get; set; }

        [StringLength(700)]
        public string Target { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }

        [Required]
        public int SeasonalProjectID { get; set; }

        [StringLength(400)]
        public string Region { get; set; }

        public int OrganizationID { get; set; }

        public int JoinStatus { get; set; }

        public int Approval { get; set; }
    }
    public class EventActivityModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleEn { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleAr { get; set; }

        [AllowHtml]
        [StringLength(300)]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        [StringLength(300)]
        public string DescriptionAr { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int ActivityID { get; set; }
        public int SeasonalProjectID { get; set; }
        
    }
}