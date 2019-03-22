using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models
{
    public class MonthModel
    {
        [Required]
        public int Month { get; set; }
    }
}