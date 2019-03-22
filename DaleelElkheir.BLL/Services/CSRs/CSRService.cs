using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.CSRs
{
    public class CSRService : ICSRService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public CSRService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        #region CSRServices
        public CompanySocialResponsibility GetCSR(int id)
        {
            return unitOfWork.Repository<CompanySocialResponsibility>().GetById(id);
        }

        public List<CompanySocialResponsibility> GetCSR(Expression<Func<CompanySocialResponsibility, bool>> Predicate)
        {
            return unitOfWork.Repository<CompanySocialResponsibility>().Get(Predicate);
        }

        public List<CompanySocialResponsibility> GetCSRs()
        {
            return unitOfWork.Repository<CompanySocialResponsibility>().GetAll();
        }

        public void InsertCSR(CompanySocialResponsibility _csr)
        {
            unitOfWork.Repository<CompanySocialResponsibility>().Insert(_csr);
            unitOfWork.Save();
        }

        public void UpdateCSR(CompanySocialResponsibility _csr)
        {
            unitOfWork.Repository<CompanySocialResponsibility>().Update(_csr);
            unitOfWork.Save();
        }
        public void DeleteCSR(int id)
        {
            unitOfWork.Repository<CompanySocialResponsibility>().Delete(id);
            unitOfWork.Save();
        }

        #endregion


        #region CSRActivityServices
        public CSRActivity GetCSRActivity(int id)
        {
            return unitOfWork.Repository<CSRActivity>().GetById(id);
        }

        public List<CSRActivity> GetCSRActivity(Expression<Func<CSRActivity, bool>> Predicate)
        {
            return unitOfWork.Repository<CSRActivity>().Get(Predicate);
        }

        public List<CSRActivity> GetCSRActivitys()
        {
            return unitOfWork.Repository<CSRActivity>().GetAll();
        }

        public void InsertCSRActivity(CSRActivity _csrActivity)
        {
            unitOfWork.Repository<CSRActivity>().Insert(_csrActivity);
            unitOfWork.Save();
        }

        public void UpdateCSRActivity(CSRActivity _csrActivity)
        {
            unitOfWork.Repository<CSRActivity>().Update(_csrActivity);
            unitOfWork.Save();
        }
        public void DeleteCSRActivity(int id)
        {
            unitOfWork.Repository<CSRActivity>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

    }
}
