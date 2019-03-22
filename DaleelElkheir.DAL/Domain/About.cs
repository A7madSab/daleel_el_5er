namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About
    {
        public int ID { get; set; }

        public string BriefEn { get; set; }

        public string BriefAr { get; set; }

        public string VisionEn { get; set; }

        public string VisionAr { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        [StringLength(20)]
        public string EmergencyNumber { get; set; }

        [StringLength(50)]
        public string FacebookCount { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string WebSite { get; set; }

        public int? BloodBankHelpsAcount { get; set; }

        public string Message { get; set; }

        public string Address { get; set; }
    }
}
