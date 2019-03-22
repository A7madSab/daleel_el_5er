using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.API.Models.Users
{
    public class LogoutModel
    {
        [Required]
        public Guid SecurityToken { get; set; }
       // [Required]
       // public string DeviceToken { get; set; }
    }
}
