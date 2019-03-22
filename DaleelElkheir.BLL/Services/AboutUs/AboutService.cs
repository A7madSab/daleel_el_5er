using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.AboutUs
{
    public class AboutService : IAboutService
    {
        #region About       

        private readonly IUnitOfWork unitOfWork;

        public AboutService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public About GetAbout(int id)
        {
            return unitOfWork.Repository<About>().GetById(id);
        }

        public List<About> GetAbout(Expression<Func<About, bool>> Predicate)
        {
            return unitOfWork.Repository<About>().Get(Predicate);
        }

        public List<About> GetAbout()
        {
            return unitOfWork.Repository<About>().GetAll();
        }

        public void InsertAbout(About _About)
        {
            unitOfWork.Repository<About>().Insert(_About);
            unitOfWork.Save();
        }

        public void UpdateAbout(About _About)
        {
            unitOfWork.Repository<About>().Update(_About);
            unitOfWork.Save();
        }

        public void DeleteAbout(int id)
        {
            unitOfWork.Repository<About>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

        #region ResponsibilityServices
        public List<CompanySocialResponsibility> getCompanyResponsibility()
        {
            return unitOfWork.Repository<CompanySocialResponsibility>().GetAll();
        }
        public void InsertCompanyResponsibility(CompanySocialResponsibility _company)
        {
            unitOfWork.Repository<CompanySocialResponsibility>().Insert(_company);
            unitOfWork.Save();
        }
        public void DeleteCompanyResponsibility(int id)
        {
            unitOfWork.Repository<CompanySocialResponsibility>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

    }
}
