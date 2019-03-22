using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("CompanyCategory")]
    public class CompanyCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
