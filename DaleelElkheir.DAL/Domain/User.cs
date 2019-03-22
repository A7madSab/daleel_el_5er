namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ChatThreads = new HashSet<ChatThread>();
            ChatThreadMessages = new HashSet<ChatThreadMessage>();
            Events = new HashSet<Event>();
            HelpCases = new HashSet<HelpCase>();
            UserCases = new HashSet<UserCase>();
            UserCategories = new HashSet<UserCategory>();
            UserDevices = new HashSet<UserDevice>();
            UserOrgs = new HashSet<UserOrg>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public int? UserTypeID { get; set; }

        public int? ImageID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Facebook_ID { get; set; }

        [StringLength(200)]
        public string Google_ID { get; set; }

        [StringLength(50)]
        public string VerifyCode { get; set; }

        public int? OrganizationID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatThread> ChatThreads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatThreadMessage> ChatThreadMessages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        public virtual FileData FileData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HelpCase> HelpCases { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual UserType UserType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCase> UserCases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCategory> UserCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDevice> UserDevices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserOrg> UserOrgs { get; set; }
    }
}
