namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserOrg")]
    public partial class UserOrg
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? OrgID { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual User User { get; set; }
    }
}
