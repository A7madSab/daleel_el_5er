using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.Organizations
{
    public class OrganizationModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(100)]
        public string NameAr { get; set; }

        public int? LogoFileID { get; set; }

        [AllowHtml]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        public string DescriptionAr { get; set; }

        [AllowHtml]
        public string HowToDonate { get; set; }

        [AllowHtml]
        public string DescriptionProgram { get; set; }

        [StringLength(100)]
        public string AddressEn { get; set; }

        [StringLength(100)]
        public string AddressAr { get; set; }

        public int? CityID { get; set; }
        
        public int? GovernorateID { get; set; }

        public int? AreaID { get; set; }

        public virtual FileData FileData { get; set; }


        public List<int?> CategoryList { get; set; }

        [StringLength(200)]
        public string Latitude { get; set; }

        [StringLength(200)]
        public string Longitude { get; set; }
    }


    public class OrganizationModelReturn
    {
        public int? ID { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public string AddressEn { get; set; }

        public string AddressAr { get; set; }

        public string Governorate { get; set; }

        public string Region { get; set; }

        public string Area { get; set; }

        public string ProgramDescription { get; set; }

        public string Categoy { get; set; }

        public int? Status { get; set; }
    }
}