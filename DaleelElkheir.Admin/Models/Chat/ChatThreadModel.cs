using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Chat
{
    public class ChatThreadModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public DateTime CreationDate { get; set; }

        public List<string> Message { get; set; }

        public int? SeenCount { get; set; }
        public int CaseID { get; set; }
        public virtual User User { get; set; }
        public virtual HelpCase HelpCase { get; set; }

    }

}