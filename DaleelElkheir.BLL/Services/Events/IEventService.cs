using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Events
{
    public interface IEventService
    {
        Event GetEvent(int id);
        List<Event> GetEvent(Expression<Func<Event,bool>> Predicate);

        List<Event> GetEvents();

        List<int> GetEventDays(int month);
        List<Event> GetEvnt();
        void InsertEvent(Event _event);
        void UpdateEvent(Event _event);
        void DeleteEvent(int id);


        EventGallery GetEventGallery(int id);

        List<EventGallery> GetEventGallery(Expression<Func<EventGallery, bool>> Predicate);

        List<EventGallery> GetEventGallerys();

        void InsertEventGallery(EventGallery _Gallery);

        void UpdateEventGallery(EventGallery _Gallery);
    
        void DeleteEventGallery(int id);
      

    }
}
