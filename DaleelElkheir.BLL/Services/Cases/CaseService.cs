using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Cases
{
    public class CaseService : ICaseService
    {
        #region CaseServices
        private readonly IUnitOfWork unitOfWork;

        public CaseService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public HelpCase GetCase(int id)
        {
            return unitOfWork.Repository<HelpCase>().GetById(id);
        }

        public List<HelpCase> GetCases(Expression<Func<HelpCase, bool>> Predicate)
        {
            return unitOfWork.Repository<HelpCase>().Get(Predicate);
        }

        public List<HelpCase> GetCases()
        {
            return unitOfWork.Repository<HelpCase>().GetAll();
        }
        public List<HelpCase> GetCas()
        {
            return unitOfWork.Repository<HelpCase>().GetAll(false);
        }
        public void InsertCase(HelpCase _Case)
        {
            unitOfWork.Repository<HelpCase>().Insert(_Case);
            unitOfWork.Save();
        }

        public void UpdateCase(HelpCase _Case)
        {
            unitOfWork.Repository<HelpCase>().Update(_Case);
            unitOfWork.Save();
        }
        public void DeleteCase(int id)
        {
            unitOfWork.Repository<HelpCase>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

        #region CaseStatusServices
        public CaseStatu GetCaseStatus(int id)
        {
            return unitOfWork.Repository<CaseStatu>().GetById(id);
        }

        public List<CaseStatu> GetCaseStatus(Expression<Func<CaseStatu, bool>> Predicate)
        {
            return unitOfWork.Repository<CaseStatu>().Get(Predicate);
        }

        public List<CaseStatu> GetCaseStatus()
        {
            return unitOfWork.Repository<CaseStatu>().GetAll();
        }

        public void InsertCaseStatus(CaseStatu _Case)
        {
            unitOfWork.Repository<CaseStatu>().Insert(_Case);
            unitOfWork.Save();
        }

        public void UpdateCaseStatus(CaseStatu _Case)
        {
            unitOfWork.Repository<CaseStatu>().Update(_Case);
            unitOfWork.Save();
        }
        public void DeleteCaseStatus(int id)
        {
            unitOfWork.Repository<CaseStatu>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

        #region CaseTypeServices
        public CaseType GetCaseType(int id)
        {
            return unitOfWork.Repository<CaseType>().GetById(id);
        }

        public List<CaseType> GetCaseType(Expression<Func<CaseType, bool>> Predicate)
        {
            return unitOfWork.Repository<CaseType>().Get(Predicate);
        }

        public List<CaseType> GetCaseType()
        {
            return unitOfWork.Repository<CaseType>().GetAll();
        }

        public void InsertCaseType(CaseType _Casetype)
        {
            unitOfWork.Repository<CaseType>().Insert(_Casetype);
            unitOfWork.Save();
        }

        public void UpdateCaseType(CaseType _Casetype)
        {
            unitOfWork.Repository<CaseType>().Update(_Casetype);
            unitOfWork.Save();
        }
        public void DeleteCaseType(int id)
        {
            unitOfWork.Repository<CaseType>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

    }
}
