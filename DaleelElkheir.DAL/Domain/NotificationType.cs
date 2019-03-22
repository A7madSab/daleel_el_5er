namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotificationType")]
    public partial class NotificationType
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
