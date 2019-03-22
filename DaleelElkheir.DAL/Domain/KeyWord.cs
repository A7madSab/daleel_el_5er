using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("KeyWord")]
    public class KeyWord
    {
        public int ID { get; set; }
        public string Word { get; set; }
        public int GuideID { get; set; }

        public virtual Guide Guide { get; set; }
    }
}
