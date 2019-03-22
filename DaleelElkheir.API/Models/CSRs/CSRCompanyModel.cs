using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.CSRs
{
    public class CSRModel
    {
        public int ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}