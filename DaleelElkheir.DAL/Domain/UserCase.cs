namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCase")]
    public partial class UserCase
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? CaseID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        public virtual HelpCase HelpCase { get; set; }

        public virtual User User { get; set; }
    }
}
