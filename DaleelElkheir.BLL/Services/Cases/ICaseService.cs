using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Cases
{
    public interface ICaseService
    {
        #region CaseServices
        HelpCase GetCase(int id);

        List<HelpCase> GetCases(Expression<Func<HelpCase, bool>> Predicate);

        List<HelpCase> GetCases();
        List<HelpCase> GetCas();
        void InsertCase(HelpCase _Case);

        void UpdateCase(HelpCase _Case);

        void DeleteCase(int id);
        #endregion

        #region CaseStatusServices
        CaseStatu GetCaseStatus(int id);

        List<CaseStatu> GetCaseStatus(Expression<Func<CaseStatu, bool>> Predicate);

        List<CaseStatu> GetCaseStatus();
        void InsertCaseStatus(CaseStatu _Case);

        void UpdateCaseStatus(CaseStatu _Case);
        void DeleteCaseStatus(int id);

        #endregion

        #region CaseTypeServices
        CaseType GetCaseType(int id);

        List<CaseType> GetCaseType(Expression<Func<CaseType, bool>> Predicate);

        List<CaseType> GetCaseType();

        void InsertCaseType(CaseType _Casetype);

        void UpdateCaseType(CaseType _Casetype);
        void DeleteCaseType(int id);

        #endregion
    }
}
