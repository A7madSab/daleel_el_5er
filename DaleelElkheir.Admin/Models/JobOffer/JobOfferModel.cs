using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.JobOffer
{
    public class JobOfferModel
    {
        public int ID { get; set; }

        public string JobTitle { get; set; }

        public string Employer { get; set; }

        [AllowHtml]
        public string DescritpionAr { get; set; }

        [AllowHtml]
        public string DescritpionEn { get; set; }

        public string ContactInfo { get; set; }
    }
}