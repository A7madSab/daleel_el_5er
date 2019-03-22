using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Events;
using DaleelElkheir.BLL.Services.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class EventController : ApiController
    {
        private readonly IEventService EventService;

        public EventController(IEventService _EventService)
        {
            this.EventService = _EventService;

            CultureInfo customCulture = new CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;
        }

        [HttpPost]
        public IHttpActionResult GetEvents(BaseRequest request)
        {
            if(ModelState.IsValid)
            {
                var events = EventService.GetEvent(x => x.ConfirmationID == 2);
                List<EventModel> eventList = new List<EventModel>();
                foreach(var item in events)
                {
                    int buffer = 0;
                    if (item.EndDate>DateTime.Now)
                    {
                        buffer = Convert.ToInt32((item.EndDate.Value - DateTime.Now).TotalDays);
                    }
                    var eventModel = new EventModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Address = request.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        StartDate = item.StartDate.HasValue ? item.StartDate.Value.ToString() : "",
                        EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToString() : "",
                        DaysToEnd= buffer,
                        Link =item.Link,
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        Mobile=item.Mobile,
                        Category= request.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        Organization= request.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Governorate= request.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Region = request.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                    };
                    eventList.Add(eventModel);
                    
                }
                return Ok(new BaseResponse(eventList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetEventByDate(EventRequest request)
        {
            if (ModelState.IsValid)
            {
                var eventDate = Convert.ToDateTime(request.EventDate);
                var events = EventService.GetEvent(x=>DbFunctions.TruncateTime(x.StartDate)<=DbFunctions.TruncateTime(eventDate) && DbFunctions.TruncateTime(x.EndDate)>= DbFunctions.TruncateTime(eventDate) && x.ConfirmationID == 2);
                List<EventModel> eventList = new List<EventModel>();
                foreach (var item in events)
                {
                    var eventModel = new EventModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Address = request.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        StartDate = item.StartDate.HasValue ? item.StartDate.Value.ToString() : "",
                        EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToString() : "",
                        Link=item.Link,
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        Mobile = item.Mobile,
                        Category = request.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        Organization = request.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Governorate = request.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Region = request.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                    };
                    eventList.Add(eventModel);

                }
                return Ok(new BaseResponse(eventList));
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetEventsAcount()
        {
            if (ModelState.IsValid)
            {
                var eventAcount = EventService.GetEvents().Count();
                return Ok(new BaseResponse(eventAcount));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetFilteredEvents(EventFilterRequest request)
        {
            if (ModelState.IsValid)
            {
                var events = EventService.GetEvent(x => x.ConfirmationID == 2);
                if (request.CategoryID != null)
                {
                    events = events.Where(x => x.CategoryID == request.CategoryID).ToList();
                }
                if (request.GovernorateID != null)
                {
                    events = events.Where(x => x.City.GovernorateID == request.GovernorateID).ToList();
                }
                if (request.OrganizationID !=null)
                {
                    events = events.Where(x => x.OrganizationID == request.OrganizationID).ToList();
                }
                if (request.RegionID != null)
                {
                    events = events.Where(x => x.CityID == request.RegionID).ToList();
                }

                List<EventModel> eventList = new List<EventModel>();
                foreach (var item in events)
                {
                    var eventModel = new EventModel()
                    {
                        ID = item.ID,
                        Title = request.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Address = request.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        StartDate = item.StartDate.HasValue ? item.StartDate.Value.ToString() : "",
                        EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToString() : "",
                        Link=item.Link,
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        Mobile = item.Mobile,
                        Category = request.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        Organization = request.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Governorate = request.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Region = request.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                    };
                    eventList.Add(eventModel);

                }
                return Ok(new BaseResponse(eventList));
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        public IHttpActionResult GetEventGallery(EventGalleryRequest request)
        {
            if (ModelState.IsValid)
            {
                var eventGallery = EventService.GetEventGallerys();
                if (request.EventID!=null)
                  eventGallery = EventService.GetEventGallery(x => x.EventID==request.EventID);

                List<EventGalleryModel> eventGalleryList = new List<EventGalleryModel>();
                foreach (var item in eventGallery)
                {
                    var eventModel = new EventGalleryModel()
                    {
                         ImageURL=item.Ext,
                         Description=request.Lang.ToLower()=="ar"?item.DescriptionAr:item.DescriptionEn
                    };
                    eventGalleryList.Add(eventModel);

                }
                return Ok(new BaseResponse(eventGalleryList));
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public IHttpActionResult GetEventGallerys()
        {
            if (ModelState.IsValid)
            {
                var eventGallery = EventService.GetEventGallerys();
                List<EventGallerysModel> eventGalleryList = new List<EventGallerysModel>();
                foreach (var item in eventGallery)
                {
                    var eventModel = new EventGallerysModel()
                    {
                        EventID=item.EventID??0,
                        ImageURL = item.Ext
                         
                    };
                    eventGalleryList.Add(eventModel);

                }
                return Ok(new BaseResponse(eventGalleryList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetEventDetailsByID(EventDetailsRequest request)
        {
            if (ModelState.IsValid)
            {
                var eventObj = EventService.GetEvent(x=>x.ID ==request.EventID).FirstOrDefault();
                        
                    
                    var eventModel = new EventModel()
                    {
                        ID = eventObj.ID,
                        Title = request.Lang == "ar" ? eventObj.TitleAr : eventObj.TitleEn,
                        Address = request.Lang == "ar" ? eventObj.AddressAr : eventObj.AddressEn,
                        StartDate = eventObj.StartDate.HasValue ? eventObj.StartDate.Value.ToString() : "",
                        EndDate = eventObj.EndDate.HasValue ? eventObj.EndDate.Value.ToString() : "",
                        Link=eventObj.Link,
                        Description = request.Lang == "ar" ? eventObj.DescriptionAr : eventObj.DescriptionEn,
                        Image = eventObj.FileData != null ? eventObj.FileData.Extenstion : null,
                        Mobile = eventObj.Mobile,
                        Category = request.Lang == "ar" ? eventObj.Category.NameAr : eventObj.Category.NameEn,
                        Organization = request.Lang == "ar" ? eventObj.Organization.NameAr : eventObj.Organization.NameEn,
                        Governorate = request.Lang == "ar" ? eventObj.City.Governorate.NameAr : eventObj.City.Governorate.NameEn,
                        Region = request.Lang == "ar" ? eventObj.City.NameAr : eventObj.City.NameEn,
                    };
                
                return Ok(new BaseResponse(eventModel));
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        public IHttpActionResult GetEventDays(MonthModel model)
        {
            var days = EventService.GetEventDays(model.Month);
            return Ok(new BaseResponse(new { Days=days}));

        }

    }
}
