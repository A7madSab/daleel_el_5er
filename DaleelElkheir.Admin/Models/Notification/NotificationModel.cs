using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.Admin.Models.Notification
{
    public class NotificationModel
    {
        public string Body { get; set; }
        public int? UserID { get; set; }

        public int TransID { get; set; }

        public int TransType { get; set; }
    }
}
