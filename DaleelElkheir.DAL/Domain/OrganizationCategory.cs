namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrganizationCategory")]
    public partial class OrganizationCategory
    {
        public int ID { get; set; }

        public int? OrgID { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
