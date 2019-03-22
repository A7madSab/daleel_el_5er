using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public string ProgramDescription { get; set; }

        public string Ext { get; set; }

        public int? SellerID{ get; set; }

        public int? CategoryID { get; set; }

        public virtual Seller Seller { get; set; }

        public virtual ProductCategory Category { get; set; }

    }
}
