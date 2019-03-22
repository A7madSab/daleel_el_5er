using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.SeasonalProjects
{
    public class SeasonalProjectListModel
    {
        public SeasonalProjectListModel()
        {
            this.activities = new List<SeasonalProjectActivityModel>();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public List<SeasonalProjectActivityModel> activities { get; set; }
    }

    public class SeasonalProjectActivityModel
    {
        public string Region { get; set; }

        public int OrgID { get; set; }

        public string OrgName { get; set; }

        public string Target { get; set; }

        public string Price { get; set; }


    }
}