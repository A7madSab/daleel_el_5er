using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Hospitals
{
    public class HospitalDetailModel
    {

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
         public string Title { get; set; }

        public string City { get; set; }
        public string Governorate { get; set; }
        public string Description { get; set; }
        public List<HospitalContactModel> HospitalContacts { get; set; }

    }

    public class HospitalContactModel
    {

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

    }
}