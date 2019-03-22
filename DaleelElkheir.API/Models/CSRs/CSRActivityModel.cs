using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.CSRs
{
    public class CSRActivityModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(400)]
        public string Description { get; set; }

        public string companyName { get; set; }

        public string companyImage { get; set; }

        public string ActivityDate { get; set; }
    }
}