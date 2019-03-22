using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.TrusteesBoards
{
    public class TrusteesBoardModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; }

        [Required]
        [StringLength(200)]
        public string NameEn { get; set; }

        [Required]
        [StringLength(300)]
        public string TitleAr { get; set; }

        [Required]
        [StringLength(300)]
        public string TitleEn { get; set; }

        public int? ImageID { get; set; }

        public virtual FileData FileData { get; set; }
    }
}