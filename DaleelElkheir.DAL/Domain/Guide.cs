using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("Guide")]
    public class Guide
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

        public string FileName { get; set; }

        public string Ext { get; set; }

        public virtual ICollection<KeyWord> KeyWords { get; set; }
    }
}
