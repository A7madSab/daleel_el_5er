namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrusteesBoard")]
    public partial class TrusteesBoard
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; }

        [Required]
        [StringLength(200)]
        public string NameEn { get; set; }

        [StringLength(300)]
        public string TitleAr { get; set; }

        [StringLength(300)]
        public string TitleEn { get; set; }

        public int? ImageID { get; set; }

        public virtual FileData FileData { get; set; }
    }
}
