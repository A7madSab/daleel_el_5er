namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BloodBank")]
    public partial class BloodBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BloodBank()
        {
            BloodBankContacts = new HashSet<BloodBankContact>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string NameEn { get; set; }

        [StringLength(100)]
        public string NameAr { get; set; }

        [StringLength(100)]
        public string TitleEn { get; set; }

        [StringLength(100)]
        public string TitleAr { get; set; }

        public int? CityID { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionAr { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BloodBankContact> BloodBankContacts { get; set; }
    }
}
