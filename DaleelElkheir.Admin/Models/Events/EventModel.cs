using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.Events
{
    public class EventModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TitleEn { get; set; }

        [Required]
        [StringLength(100)]
        public string TitleAr { get; set; }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }

        public string AddressEn { get; set; }

        public string AddressAr { get; set; }

        public string Link { get; set; }

        [AllowHtml]
        public string HowToJoin { get; set; }

        [AllowHtml]
        public string DescriptionProgram { get; set; }

        [AllowHtml]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        public string DescriptionAr { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        public int? ImageID { get; set; }

        public virtual FileData FileData { get; set; }

        public int UserID { get; set; }

        [Required]
        public int OurProgramID { get; set; }

        [Required]
        public int OrganizationID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int CityID { get; set; }

        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Required]
        public int? GovernorateID { get; set; }

        public int ConfirmationID { get; set; }
    }

    public partial class EventGalleryModel
    {
        public int ID { get; set; }
        
        public int? EventID { get; set; }

        public string Ext { get; set; }

        [AllowHtml]
        [StringLength(200)]
        public string Name { get; set; }

        [AllowHtml]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        public string DescriptionAr { get; set; }


    }
}