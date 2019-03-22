namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatThreadMessage")]
    public partial class ChatThreadMessage
    {
        public int ID { get; set; }

        public int ThreadID { get; set; }

        public int? AdminID { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SendDate { get; set; }

        public int? Seen { get; set; }

        public virtual ChatThread ChatThread { get; set; }

        public virtual User User { get; set; }
    }
}
