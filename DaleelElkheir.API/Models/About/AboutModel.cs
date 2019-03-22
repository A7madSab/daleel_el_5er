using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.About
{
    public class AboutModel
    {

        public string Beif { get; set; }

        public string Vision { get; set; }

        public string Message { get; set; }

        public int CasesAcount { get; set; }

        public int EventAcount { get; set; }

        public int OrganizationAcount { get; set; }

        public int? BloodBankHelpCount { get; set; }
        public List<SocailResponsibilityModel> SocailResponsibilities { get; set; }

    }

    public class CharityInfoModel
    {
        public string Mobile { get; set; }

        public string FacebookCount { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string WebSite { get; set; }
        public string Address { get; set; }
    }
    public class SocailResponsibilityModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}