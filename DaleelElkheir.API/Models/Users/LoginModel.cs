using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Users
{
    public class LoginModel
    {
       // [Required]
       // public string UserName { get; set; }

       // [Required]
        public string Email { get; set; }


       // [Required]
        public string Password { get; set; }

        public string DeviceToken { get; set; }

        public string Facebook_ID { get; set; }

        public string google_ID { get; set; }
    }
}