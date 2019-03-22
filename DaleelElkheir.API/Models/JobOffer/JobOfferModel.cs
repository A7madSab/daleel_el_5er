using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.JobOffer
{
    public class JobOfferModel
    {
        public int ID { get; set; }

        public string JobTitle { get; set; }

        public string Employer { get; set; }

        public string DescritpionAr { get; set; }

        public string DescritpionEn { get; set; }

        public string ContactInfo { get; set; }
    }

    public class JobOfferModelRequest
    {
        [Required]
        public int ID { get; set; }
    }
}