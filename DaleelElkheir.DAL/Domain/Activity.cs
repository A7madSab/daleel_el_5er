namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Activity")]
    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            EventForActivities = new HashSet<EventForActivity>();
        }

        public int ID { get; set; }

        [StringLength(700)]
        public string Target { get; set; }

        public decimal? Price { get; set; }

        public int SeasonalProjectID { get; set; }

        [StringLength(400)]
        public string Region { get; set; }

        public int OrganizationID { get; set; }

        public int JoinStatus { get; set; }

        public int Approval { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual SeasonalProject SeasonalProject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventForActivity> EventForActivities { get; set; }
    }
}
