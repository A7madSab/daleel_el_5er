using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.OurPrograms
{
    public class OurProgramModel
    {

        public int ID { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

       
        [StringLength(300)]
        public string Description { get; set; }

    }
}