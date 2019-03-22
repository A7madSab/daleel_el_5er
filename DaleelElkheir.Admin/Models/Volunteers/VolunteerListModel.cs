using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Volunteers
{
    public class VolunteerListModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public string Job { get; set; }

        public string Nationality { get; set; }

        public string DaysAvailable { get; set; }

        public string AboutQuestion { get; set; }

        public string VolunteeringMethod { get; set; }

        public string Categories { get; set; }
    }
}