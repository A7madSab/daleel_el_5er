using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Sponsors
{
    public class SponsorModel
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Link { get; set; }

        public string Image { get; set; }
    }
}