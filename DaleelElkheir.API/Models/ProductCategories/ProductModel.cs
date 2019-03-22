using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.ProductCategories
{
    public class ProductModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProgramDescription { get; set; }

        public string FileName { get; set; }

        public string Ext { get; set; }

        public int SellerID { get; set; }

        public string SellerName { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
    }
}