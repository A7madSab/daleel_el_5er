namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatThread")]
    public partial class ChatThread
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChatThread()
        {
            ChatThreadMessages = new HashSet<ChatThreadMessage>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int CaseID { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual HelpCase HelpCase { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatThreadMessage> ChatThreadMessages { get; set; }
    }
}
