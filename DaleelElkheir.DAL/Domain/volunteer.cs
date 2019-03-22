namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("volunteer")]
    public partial class volunteer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public volunteer()
        {
            VolunteerCategories = new HashSet<VolunteerCategory>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(500)]
        public string Job { get; set; }

        [StringLength(500)]
        public string Nationality { get; set; }

        [StringLength(500)]
        public string DaysAvailable { get; set; }

        [StringLength(500)]
        public string AboutQuestion { get; set; }

        [StringLength(500)]
        public string VoulunteeringMethod { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VolunteerCategory> VolunteerCategories { get; set; }
    }
}
