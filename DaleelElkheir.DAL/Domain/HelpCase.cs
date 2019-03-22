namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HelpCase")]
    public partial class HelpCase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HelpCase()
        {
            ChatThreads = new HashSet<ChatThread>();
            UserCases = new HashSet<UserCase>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string NameEn { get; set; }

        [StringLength(100)]
        public string NameAr { get; set; }

        [StringLength(50)]
        public string CaseCode { get; set; }

        [StringLength(100)]
        public string ContactNumber { get; set; }

        public int? ImageFileID { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionAr { get; set; }

        public string DescriptionProgram { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }

        public int? OrgID { get; set; }

        public int? CityID { get; set; }

        public int? CategoryID { get; set; }

        public int? CaseTypeID { get; set; }

        public int? CaseStatusID { get; set; }

        public double? RequiredAmount { get; set; }

        public double? CurrentAmount { get; set; }

        public DateTime? CreateDate { get; set; }

        public int UserID { get; set; }

        public int OurProgramID { get; set; }

        public Guid? SharedID { get; set; }

        public int ConfirmationID { get; set; }

        public virtual CaseConfirmation CaseConfirmation { get; set; }

        public virtual CaseStatu CaseStatu { get; set; }

        public virtual CaseType CaseType { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatThread> ChatThreads { get; set; }

        public virtual City City { get; set; }

        public virtual FileData FileData { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual OurProgram OurProgram { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCase> UserCases { get; set; }
    }
}
