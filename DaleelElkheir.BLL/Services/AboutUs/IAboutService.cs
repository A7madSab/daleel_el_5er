using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.AboutUs
{
    public interface IAboutService
    {
        #region AboutServices
        About GetAbout(int id);

        List<About> GetAbout(Expression<Func<About, bool>> Predicate);

        List<About> GetAbout();

        void InsertAbout(About _About);

        void UpdateAbout(About _About);

        void DeleteAbout(int id);
        #endregion

        #region ResponsibilityServices
        List<CompanySocialResponsibility> getCompanyResponsibility();
        void InsertCompanyResponsibility(CompanySocialResponsibility _company);
        void DeleteCompanyResponsibility(int id);
        #endregion
    }
}
