namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Program")]
    public partial class Program
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Descr { get; set; }

        public int? ImageFileID { get; set; }
    }
}
