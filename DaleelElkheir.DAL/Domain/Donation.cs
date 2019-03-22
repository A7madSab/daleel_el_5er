using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("Donation")]
    public partial class Donation
    {   
        public int ID { set; get; }
               
        public string Name { set; get; }
               
        public string Contact { set; get; }
    }
}
