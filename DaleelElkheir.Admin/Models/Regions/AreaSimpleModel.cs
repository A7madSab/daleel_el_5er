using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Regions
{
    public class AreaSimpleModel
    {
        public int ID { get; set; }

        [Required]
        public string NameEn { get; set; }

        [Required]
        public string NameAr { get; set; }

        public int CityID { get; set; }


    }
}