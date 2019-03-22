namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySocialResponsibility")]
    public partial class CompanySocialResponsibility
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompanySocialResponsibility()
        {
            CSRActivities = new HashSet<CSRActivity>();
        }

        public int ID { get; set; }

        [StringLength(200)]
        public string NameAr { get; set; }

        [StringLength(200)]
        public string NameEn { get; set; }

        public string Category { get; set; }

        public int? FileID { get; set; }

        public virtual FileData FileData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CSRActivity> CSRActivities { get; set; }
    }
}
