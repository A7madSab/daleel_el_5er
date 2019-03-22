using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.BLL.Services.Guides
{
    public interface IGuideServices
    {
        #region GuideServices

        Guide GetGuide(int id);

        List<Guide> GetGuide(Expression<Func<Guide, bool>> Predicate);

        List<Guide> GetGuide();

        void InsertGuide(Guide _Guide);

        void UpdateGuide(Guide _Guide);

        void DeleteGuide(int id);
        


        #endregion

    }
}
