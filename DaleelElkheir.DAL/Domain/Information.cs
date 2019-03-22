namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Information
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleEn { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleAr { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NewsDate { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionAr { get; set; }

        public int? ImageID { get; set; }

        public string VideoLink { get; set; }

        public virtual FileData FileData { get; set; }
    }
}
