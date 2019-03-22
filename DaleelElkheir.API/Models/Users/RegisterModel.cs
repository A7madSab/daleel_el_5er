using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Users
{
    public class RegisterModel:BaseRequest
    {
        
        
       // [StringLength(100)]
       // public string UserName { get; set; }

        //[Required]
        [StringLength(100)]
        public string Email { get; set; }

        
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string Facebook_ID { get; set; }

        public string Google_ID { get; set; }

        public string Image { get; set; }
        public byte[] Image2 { get; set; }
    }
}