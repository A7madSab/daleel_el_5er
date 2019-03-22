using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("CharityType")]
    public class CharityType
    {
        public int ID { get; set; }
        public string CharityName { get; set; }
    }
}
