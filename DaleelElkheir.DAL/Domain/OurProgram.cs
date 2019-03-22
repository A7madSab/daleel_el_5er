namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OurProgram")]
    public partial class OurProgram
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OurProgram()
        {
            Events = new HashSet<Event>();
            HelpCases = new HashSet<HelpCase>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HelpCase> HelpCases { get; set; }
    }
}
