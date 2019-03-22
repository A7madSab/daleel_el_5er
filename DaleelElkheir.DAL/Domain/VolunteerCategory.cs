namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VolunteerCategory")]
    public partial class VolunteerCategory
    {
        public int ID { get; set; }

        public int? VolunteerID { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual volunteer volunteer { get; set; }
    }
}
