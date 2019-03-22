using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.About
{
    public class AboutModel
    {
        public int ID { get; set; }

        [AllowHtml]
        public string BriefEn { get; set; }
        [AllowHtml]
        public string BriefAr { get; set; }
        [AllowHtml]
        public string VisionEn { get; set; }
        [AllowHtml]
        public string VisionAr { get; set; }

        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        [StringLength(20)]
        public string Mobile { get; set; }

        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Contact Number.")]
        [StringLength(20)]
        public string ContactNumber { get; set; }

        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Emergency Number.")]
        [StringLength(20)]
        public string EmergencyNumber { get; set; }

        [StringLength(50)]
        public string FacebookCount { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [StringLength(100)]
        public string WebSite { get; set; }

        public int? BloodBankHelpsAcount { get; set; }

        [AllowHtml]
        public string Message { get; set; }

        public string Address { get; set; }
    }
}