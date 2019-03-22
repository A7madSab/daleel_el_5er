using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.Cases
{
    public class CaseModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(100)]
        public string NameAr { get; set; }

        [StringLength(50)]
        public string CaseCode { get; set; }

        [StringLength(100)]
        public string ContactNumber { get; set; }

        public int? ImageFileID { get; set; }

        [AllowHtml]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        public string DescriptionAr { get; set; }

        public string DescriptionProgram { get; set; }

        public string DueDate { get; set; }

        [Required]
        public int? OrgID { get; set; }

        [Required]
        public int? CityID { get; set; }

        [Required]
        public int? GovernorateID { get; set; }

        [Required]
        public int? CategoryID { get; set; }

        [Required]
        public int? CaseTypeID { get; set; }

        [Required]
        public int? CaseStatusID { get; set; }

        [Required]
        public int? CharityTypeID { get; set; }

        public double? RequiredAmount { get; set; }

        public double? CurrentAmount { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual FileData FileData { get; set; }


        public int UserID { get; set; }

        [Required]
        public int OurProgramID { get; set; }

        public int ConfirmationID { get; set; }
    }
}