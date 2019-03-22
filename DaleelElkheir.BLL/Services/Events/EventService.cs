using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using DaleelElkheir.BLL.Extensions;

namespace DaleelElkheir.BLL.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork unitOfWork;

        public EventService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public Event GetEvent(int id)
        {
            return unitOfWork.Repository<Event>().GetById(id);
        }

        public List<Event> GetEvent(Expression<Func<Event, bool>> Predicate)
        {
            return unitOfWork.Repository<Event>().Get(Predicate);
        }

        public List<Event> GetEvents()
        {
            return unitOfWork.Repository<Event>().GetAll();
        }

        public List<int> GetEventDays(int month)
        {
            return unitOfWork.Repository<Event>().Get(w => w.StartDate.Value.Month == month || w.EndDate.Value.Month == month).ToList()
                .Select(s => s.StartDate.Value.Range(s.EndDate.Value)).Where(iw=>iw.Any(a=>a.Month==month)).Select(inner=>inner.Select(s=>s.Day)).SelectMany(m=>m).ToList();
        }

        public List<Event> GetEvnt()
        {
            return unitOfWork.Repository<Event>().GetAll(false);
        }
        public void InsertEvent(Event _event)
        {
            unitOfWork.Repository<Event>().Insert(_event);
            unitOfWork.Save();
        }

        public void UpdateEvent(Event _event)
        {
            unitOfWork.Repository<Event>().Update(_event);
            unitOfWork.Save();
        }
        public void DeleteEvent(int id)
        {
            unitOfWork.Repository<Event>().Delete(id);
            unitOfWork.Save();
        }

   
        public EventGallery GetEventGallery(int id)
        {
            return unitOfWork.Repository<EventGallery>().GetById(id);
        }

        public List<EventGallery> GetEventGallery(Expression<Func<EventGallery, bool>> Predicate)
        {
            return unitOfWork.Repository<EventGallery>().Get(Predicate);
        }

        public List<EventGallery> GetEventGallerys()
        {
            return unitOfWork.Repository<EventGallery>().GetAll();
        }

        public void InsertEventGallery(EventGallery _Gallery)
        {
            unitOfWork.Repository<EventGallery>().Insert(_Gallery);
            unitOfWork.Save();
        }

        public void UpdateEventGallery(EventGallery _Gallery)
        {
            unitOfWork.Repository<EventGallery>().Update(_Gallery);
            unitOfWork.Save();
        }
        public void DeleteEventGallery(int id)
        {
            unitOfWork.Repository<EventGallery>().Delete(id);
            unitOfWork.Save();
        }
        

    }

}
