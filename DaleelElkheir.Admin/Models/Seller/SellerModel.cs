using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Seller
{
    public class SellerModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Contract { get; set; }
        
        public string Link { get; set; }

    }
}