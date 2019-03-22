using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.BLL.Services.OurStories
{
    public interface IOurStoryService
    {
        #region OurStoryServices
        
        OurStory GetOurStory(int Id);

        List<OurStory> GetOurStory(Expression<Func<OurStory, bool>> Predicate);

        List<OurStory> GetOurStory();

        void InsertOurStory(OurStory Story);

        void UpdateOurStory(OurStory Story);

        void DeleteOurStory(int Id);
        #endregion
    }
}
