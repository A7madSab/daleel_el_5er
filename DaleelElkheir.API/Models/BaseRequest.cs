using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.API.Models
{
    public class BaseRequest
    {
        [Required]
        public string Lang { get; set; }
    }
}
