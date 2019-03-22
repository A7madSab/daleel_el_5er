namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public string Title { get; set; }

        public string TitleAr { get; set; }

        public string Body { get; set; }

        public string BodyAr { get; set; }

        public int? FromUserID { get; set; }

        public DateTime? Date { get; set; }

        public int? Type { get; set; }

        public bool? Seen { get; set; }

        public int? TransID { get; set; }

        public int? TransType { get; set; }
    }
}
