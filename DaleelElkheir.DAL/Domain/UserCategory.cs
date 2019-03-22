namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserCategory
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
    }
}
