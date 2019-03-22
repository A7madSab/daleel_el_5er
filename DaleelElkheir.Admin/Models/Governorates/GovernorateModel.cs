using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Governorates
{
    public class GovernorateModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(100)]
        public string NameAr { get; set; }
    }
}