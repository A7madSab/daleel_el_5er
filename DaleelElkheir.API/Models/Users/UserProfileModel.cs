using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Users
{
    public class UserProfileModel
    {
       
        
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

       // public int? UserTypeID { get; set; }

        public byte[] Image { get; set; }
     
    }
}