using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Informations
{
    public class InformationModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public DateTime NewsDate { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public string Image { get; set; }
        public string Link { get; set; }
    }
}