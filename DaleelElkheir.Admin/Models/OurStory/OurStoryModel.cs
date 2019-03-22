using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Models.OurStory
{
    public class OurStoryModel
    {
        [AllowHtml]
        public int ID { get; set; }

        [AllowHtml]
        [Url(ErrorMessage = "Please enter a valid url")]
        public string VideoURL { get; set; }

        [AllowHtml]
        public string BriefEnglish { get; set; }

        [AllowHtml]
        public string BriefArabic { get; set; }
    }
}