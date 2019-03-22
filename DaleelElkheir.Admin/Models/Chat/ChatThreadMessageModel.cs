using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Chat
{
    public class ChatThreadMessageModel
    {
        public int ID { get; set; }

        public int ThreadID { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SendDate { get; set; }
    }
}