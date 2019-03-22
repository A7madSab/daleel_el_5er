using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.SeasonalProjects
{
    public interface ISeasonalProjectService
    {
        SeasonalProject GetSeasonalProject(int id);
        List<SeasonalProject> GetSeasonalProject(Expression<Func<SeasonalProject, bool>> Predicate);

        List<SeasonalProject> GetSeasonalProjects();
        void InsertSeasonalProject(SeasonalProject _SeasonalProject);
        void UpdateSeasonalProject(SeasonalProject _SeasonalProject);
        void DeleteSeasonalProject(int id);

        #region Activities
        Activity GetSeasonalProjectActivity(int id);
        List<Activity> GetSeasonalProjectActivity(Expression<Func<Activity, bool>> Predicate);

        List<Activity> GetSeasonalProjectActivities();
        void InsertSeasonalProjectActivity(Activity _Activity);
        void UpdateSeasonalProjectActivity(Activity _Activity);
        void DeleteSeasonalProjectActivity(int id);
        #endregion

        #region EventActivities
        EventForActivity GetEventActivity(int id);
        List<EventForActivity> GetEventActivity(Expression<Func<EventForActivity, bool>> Predicate);

        List<EventForActivity> GetEventActivities();
        void InsertEventActivity(EventForActivity _Activity);
        void UpdateEventActivity(EventForActivity _Activity);
        void DeleteEventActivity(int id);
        #endregion

    }
}
