using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Events
{

    public class EventDetailsRequest:BaseRequest
    {

        public int? EventID { get; set; }

    }

    public class EventGalleryRequest :BaseRequest
    {

        public int? EventID { get; set; }

    }
    public class EventFilterRequest : BaseRequest
    {

        public int? GovernorateID { get; set; }

        public int? OrganizationID { get; set; }

        public int? CategoryID { get; set; }
    
        public int? RegionID { get; set; }
    }
    public class EventRequest:BaseRequest
    {
        [Required]
        public string EventDate { get; set; }
    }
    public class EventModel
    {

        public int ID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int DaysToEnd { get; set; }

        public string Link { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public string Description { get; set; }


        public string Organization { get; set; }

        public string Category { get; set; }

        public string Governorate { get; set; }

        public string Region { get; set; }

        public string Mobile { get; set; }

        public string Image { get; set; }


    }

    public class EventGalleryModel
    {
        public string ImageURL { get; set; }
        public string Description { get; set; }

    }
    public class EventGallerysModel
    {
        public int EventID { get; set; }
        public string ImageURL { get; set; }

    }
}