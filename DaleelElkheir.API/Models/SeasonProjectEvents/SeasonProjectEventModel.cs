using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.SeasonProjectEvents
{
    public class SeasonProjectEventModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Activity { get; set; }

        public string Project { get; set; }

    }
}