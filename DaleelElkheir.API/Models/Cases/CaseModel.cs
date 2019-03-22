using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Cases
{
    public class CaseFilterRequest:BaseRequest
    {
        public int? OrganizationID { get; set; }

        public int? UserID { get; set; }

        public int? GovernorateID { get; set; }

        public int? CategoryID { get; set; }

        public int? RegionID { get; set; }
    }
    public class CaseModel
    {

        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string CaseCode { get; set; }

        public string DueDate { get; set; }

        public string Organization { get; set; }

        public string City { get; set; }

        public string Governorate { get; set; }

        public string Category { get; set; }

        public string CaseType { get; set; }

        public string CaseStatus { get; set; }

        public string ProgramDescription { get; set; }


        public double? RequiredAmount { get; set; }

        public double? CurrentAmount { get; set; }
        public string Description { get; set; }
        public string SharedURL { get; set; }
        public string Image { get; set; }

        public bool Joined { get; set; }


    }

    public class CaseModelDetail
    {

     
        [StringLength(100)]
        public string Name { get; set; }

        public string CaseCode { get; set; }

        public string ContactNumber { get; set; }

        public string Description { get; set; }

        public string DueDate { get; set; }

        public string Organization { get; set; }

        public string City { get; set; }

        public string Governorate { get; set; }

        public string Category { get; set; }

        public int? CategoryID { get; set; }

        public string CaseType { get; set; }

        public string CaseStatus { get; set; }

        public double? RequiredAmount { get; set; }

        public double? CurrentAmount { get; set; }

        public string SharedURL { get; set; }
        public string Image { get; set; }

        public string ProgramDescription { get; set; }


    }

    public class CaseProgramRequest:BaseRequest
    {
        public int ProgramID { get; set; }
    }
    
}