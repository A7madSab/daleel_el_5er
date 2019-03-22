namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeviceToken")]
    public partial class DeviceToken
    {
        public int ID { get; set; }

        [Required]
        [StringLength(500)]
        public string DeviceTokenKey { get; set; }

        public Guid UserKey { get; set; }
    }
}
