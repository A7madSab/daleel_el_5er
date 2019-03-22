using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.TrusteesBoards
{
    public class TrusteesBoardModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        //public int? ImageID { get; set; }

        public string Image { get; set; }
    }
}