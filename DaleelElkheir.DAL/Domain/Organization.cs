namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization")]
    public partial class Organization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organization()
        {
            Activities = new HashSet<Activity>();
            Events = new HashSet<Event>();
            HelpCases = new HashSet<HelpCase>();
            OrganizationCategories = new HashSet<OrganizationCategory>();
            Users = new HashSet<User>();
            UserOrgs = new HashSet<UserOrg>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string NameEn { get; set; }

        [StringLength(100)]
        public string NameAr { get; set; }

        public int? LogoFileID { get; set; }

        public string HowToDonate{ get; set; }

        public string DescriptionProgram { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionAr { get; set; }

        [StringLength(100)]
        public string AddressEn { get; set; }

        [StringLength(100)]
        public string AddressAr { get; set; }

        public int? CityID { get; set; }

        public int? AreaID { get; set; }

        [StringLength(200)]
        public string Latitude { get; set; }

        [StringLength(200)]
        public string Longitude { get; set; }

        public int? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        public virtual Area Area { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        public virtual FileData FileData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HelpCase> HelpCases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationCategory> OrganizationCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserOrg> UserOrgs { get; set; }
    }
}
