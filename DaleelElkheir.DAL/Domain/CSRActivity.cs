namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSRActivity")]
    public partial class CSRActivity
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleEn { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleAr { get; set; }

        [StringLength(400)]
        public string DescriptionEn { get; set; }

        [StringLength(400)]
        public string DescriptionAr { get; set; }

        public int CSR_ID { get; set; }

        public DateTime? ActivityDate { get; set; }

        public virtual CompanySocialResponsibility CompanySocialResponsibility { get; set; }
    }
}
