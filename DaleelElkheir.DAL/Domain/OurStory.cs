using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("OurStory")]
    public partial class OurStory
    {
        public int ID { get; set; }

        public string VideoURL { get; set; }

        public string BriefEnglish { get; set; }

        public string BriefArabic { get; set; }
    }
}
