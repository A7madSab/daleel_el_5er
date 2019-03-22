namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventForActivity")]
    public partial class EventForActivity
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleEn { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleAr { get; set; }

        [StringLength(300)]
        public string DescriptionEn { get; set; }

        [StringLength(300)]
        public string DescriptionAr { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int ActivityID { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
