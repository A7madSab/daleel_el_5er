namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaseType")]
    public partial class CaseType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaseType()
        {
            HelpCases = new HashSet<HelpCase>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string NameAr { get; set; }

        [StringLength(50)]
        public string NameEn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HelpCase> HelpCases { get; set; }
    }
}
