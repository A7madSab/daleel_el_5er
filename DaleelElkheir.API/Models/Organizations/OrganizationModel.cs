using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Organizations
{
    public class OrganizationRegionRequest : BaseRequest
    {
        public int? RegionID { get; set; }
    }

    public class OrganizationFilterRequest : BaseRequest
    {
        public int? GovernorateID { get; set; }

        public int? CategoryID { get; set; }
        
        public int? RegionID { get; set; }
    }

    public class OrganizationModel
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public string HowToDonate { get; set; }

        public string Governorate { get; set; }

        public string DescriptionProgram { get; set; }

        [StringLength(100)]
        public string Area { get; set; }

        public string Logo { get; set; }
        public List<CategModel> Categories { get; set; }

        [StringLength(200)]
        public string Latitude { get; set; }

        [StringLength(200)]
        public string Longitude { get; set; }


    }

    public class CategModel
    {
        public string Name { get; set; }
    }

    public class OrganizationsByRegionModel
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public string Area { get; set; }
    
        [StringLength(200)]
        public string Latitude { get; set; }

        [StringLength(200)]
        public string Longitude { get; set; }
    }

    public class OrganizationSimpleModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(100)]
        public string NameAr { get; set; }

        public int? LogoFileID { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionAr { get; set; }

        [StringLength(100)]
        public string AddressEn { get; set; }

        [StringLength(100)]
        public string AddressAr { get; set; }

        public int? CityID { get; set; }

        public int? AreaID { get; set; }

        public List<int?> CategoryList { get; set; }

        [StringLength(200)]
        public string Latitude { get; set; }

        [StringLength(200)]
        public string Longitude { get; set; }
    }

    
}