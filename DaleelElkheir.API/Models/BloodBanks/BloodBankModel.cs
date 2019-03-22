using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.BloodBanks
{
    public class FilterBloodBankRequest:BaseRequest
    {
        public int? RegionID { get; set; }

        public int? GovernorateID { get; set; }
    }
    public class BloodBankModel
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public string City { get; set; }
        public string Governorate { get; set; }

    }
}