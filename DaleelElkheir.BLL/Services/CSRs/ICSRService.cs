using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.CSRs
{
    public interface ICSRService
    {
        #region
        CompanySocialResponsibility GetCSR(int id);
        List<CompanySocialResponsibility> GetCSR(Expression<Func<CompanySocialResponsibility, bool>> Predicate);

        List<CompanySocialResponsibility> GetCSRs();
        void InsertCSR(CompanySocialResponsibility _csr);
        void UpdateCSR(CompanySocialResponsibility _csr);
        void DeleteCSR(int id);
        #endregion


        #region
        CSRActivity GetCSRActivity(int id);
        List<CSRActivity> GetCSRActivity(Expression<Func<CSRActivity, bool>> Predicate);

        List<CSRActivity> GetCSRActivitys();
        void InsertCSRActivity(CSRActivity _csrActivity);
        void UpdateCSRActivity(CSRActivity _csrActivity);
        void DeleteCSRActivity(int id);
        #endregion

    }
}
