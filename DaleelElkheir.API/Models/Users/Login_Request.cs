using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Users
{
    public class Login_Request
    {
        public int UserID { get; set; }
        public Guid SecurityToken { get; set; }
        public int userType { get; set; }
    }
}