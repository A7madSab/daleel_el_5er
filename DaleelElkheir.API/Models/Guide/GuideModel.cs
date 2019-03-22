using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Guide
{
    public class GuideModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string FileName { get; set; }
        
        public string Ext { get; set; }
        
        public string KeyWords { get; set; }
    }
}