using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Volunteers
{
    public class VolunteerSimpleModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Contact { get; set; }

        public string Job { get; set; }

        public string Nationality { get; set; }

        public string DaysAvailable { get; set; }

        public string AboutQuestion { get; set; }

        public string VoulunteeringMethod { get; set; }

        public List<int> Categories { get; set; }
    }
}