using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.SeasonalProjects
{
    public class SeasonalProjectService : ISeasonalProjectService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public SeasonalProjectService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public SeasonalProject GetSeasonalProject(int id)
        {
            return unitOfWork.Repository<SeasonalProject>().GetById(id);
        }

        public List<SeasonalProject> GetSeasonalProject(Expression<Func<SeasonalProject, bool>> Predicate)
        {
            return unitOfWork.Repository<SeasonalProject>().Get(Predicate);
        }

        public List<SeasonalProject> GetSeasonalProjects()
        {
            return unitOfWork.Repository<SeasonalProject>().GetAll();
        }

        public void InsertSeasonalProject(SeasonalProject _SeasonalProject)
        {
            unitOfWork.Repository<SeasonalProject>().Insert(_SeasonalProject);
            unitOfWork.Save();
        }

        public void UpdateSeasonalProject(SeasonalProject _SeasonalProject)
        {
            unitOfWork.Repository<SeasonalProject>().Update(_SeasonalProject);
            unitOfWork.Save();
        }
        public void DeleteSeasonalProject(int id)
        {
            unitOfWork.Repository<SeasonalProject>().Delete(id);
            unitOfWork.Save();
        }

        #region Seasonal Project Activity
        public Activity GetSeasonalProjectActivity(int id)
        {
            return unitOfWork.Repository<Activity>().GetById(id);
        }

        public List<Activity> GetSeasonalProjectActivity(Expression<Func<Activity, bool>> Predicate)
        {
            return unitOfWork.Repository<Activity>().Get(Predicate);
        }

        public List<Activity> GetSeasonalProjectActivities()
        {
            return unitOfWork.Repository<Activity>().GetAll();
        }

        public void InsertSeasonalProjectActivity(Activity _Activity)
        {
            unitOfWork.Repository<Activity>().Insert(_Activity);
            unitOfWork.Save();
        }

        public void UpdateSeasonalProjectActivity(Activity _Activity)
        {
            unitOfWork.Repository<Activity>().Update(_Activity);
            unitOfWork.Save();
        }
        public void DeleteSeasonalProjectActivity(int id)
        {
            unitOfWork.Repository<Activity>().Delete(id);
            unitOfWork.Save();
        }
        #endregion

        #region Event Activity
        public EventForActivity GetEventActivity(int id)
        {
            return unitOfWork.Repository<EventForActivity>().GetById(id);
        }

        public List<EventForActivity> GetEventActivity(Expression<Func<EventForActivity, bool>> Predicate)
        {
            return unitOfWork.Repository<EventForActivity>().Get(Predicate);
        }

        public List<EventForActivity> GetEventActivities()
        {
            return unitOfWork.Repository<EventForActivity>().GetAll();
        }

        public void InsertEventActivity(EventForActivity _EventActivity)
        {
            unitOfWork.Repository<EventForActivity>().Insert(_EventActivity);
            unitOfWork.Save();
        }

        public void UpdateEventActivity(EventForActivity _EventActivity)
        {
            unitOfWork.Repository<EventForActivity>().Update(_EventActivity);
            unitOfWork.Save();
        }
        public void DeleteEventActivity(int id)
        {
            unitOfWork.Repository<EventForActivity>().Delete(id);
            unitOfWork.Save();
        }
        #endregion

    }
}
