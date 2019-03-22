using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Product
{
    public class ProductDisplay
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string ProductCategoryName { get; set; }

        public string ProductCategorydesc{ get; set; }

        public string SellerName { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public string ProgramDescription { get; set; }
    }
}