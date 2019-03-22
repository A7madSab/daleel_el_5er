namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BloodBankContact")]
    public partial class BloodBankContact
    {
        public int ID { get; set; }

        public int? BloodBankID { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        public virtual BloodBank BloodBank { get; set; }
    }
}
