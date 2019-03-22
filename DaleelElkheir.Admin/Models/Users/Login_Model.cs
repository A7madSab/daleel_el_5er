using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Users
{
    public class Login_Model
    {
        
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
    }
}