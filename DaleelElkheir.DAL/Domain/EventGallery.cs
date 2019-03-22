namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventGallery")]
    public partial class EventGallery
    {
        public int ID { get; set; }


        public int? EventID { get; set; }

        [StringLength(400)]
        public string Ext { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionAr { get; set; }

        public virtual Event Event { get; set; }
    }
}
