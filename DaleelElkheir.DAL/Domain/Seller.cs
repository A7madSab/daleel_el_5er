using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("Seller")]
    public class Seller
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Contract { get; set; }

        public string Link { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
