namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeasonalProject")]
    public partial class SeasonalProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SeasonalProject()
        {
            Activities = new HashSet<Activity>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
