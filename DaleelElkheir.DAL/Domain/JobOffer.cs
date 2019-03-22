using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Domain
{
    [Table("JobOffer")]
    public class JobOffer
    {
        public int ID { get; set; }

        public string JobTitle { get; set; }

        public string Employer { get; set; }

        public string DescritpionAr { get; set; }

        public string DescritpionEn { get; set; }

        public string ContactInfo    { get; set; }
    }
}
