namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            EventGalleries = new HashSet<EventGallery>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string TitleEn { get; set; }

        [StringLength(100)]
        public string TitleAr { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(100)]
        public string AddressEn { get; set; }

        [StringLength(100)]
        public string AddressAr { get; set; }

        public string HowToJoin { get; set; }

        public string DescriptionProgram { get; set; }

        public string Link { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionAr { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        public int? ImageID { get; set; }

        public int UserID { get; set; }

        public int OurProgramID { get; set; }

        public int OrganizationID { get; set; }

        public int CategoryID { get; set; }

        public int CityID { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public int ConfirmationID { get; set; }

        public virtual CaseConfirmation CaseConfirmation { get; set; }

        public virtual Category Category { get; set; }

        public virtual City City { get; set; }

        public virtual FileData FileData { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual OurProgram OurProgram { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventGallery> EventGalleries { get; set; }
    }
}
