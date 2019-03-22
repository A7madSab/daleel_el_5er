using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Regions
{
    public class GetRegionModel:BaseRequest
    {
        [Required]
        public int GovernorateID { get; set; }
    }
}