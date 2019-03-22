namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserDevice")]
    public partial class UserDevice
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string DeviceToken { get; set; }

        public Guid SecurityToken { get; set; }

        public virtual User User { get; set; }
    }
}
