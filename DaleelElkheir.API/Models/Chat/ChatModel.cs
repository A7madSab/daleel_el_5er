using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Chat
{
    public class ChatThreadModel
    {
        public int UserID { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

    }
    public class ChatModel
    {
        public bool isAdmin { get; set; }

        public string Message { get; set; }

    }
    public class ChatRequest
    {
        public int UserID { get; set; }
        public int CaseID { get; set; }
    }
    public class ChatThreadRequest
    {
        public int UserID { get; set; }
    }
    
}