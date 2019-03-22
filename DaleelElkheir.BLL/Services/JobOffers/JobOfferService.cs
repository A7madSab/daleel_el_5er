using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.JobOffers
{
    public class JobOfferService : IJobOfferService
    {
        private readonly IUnitOfWork unitOfWork;

        public JobOfferService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public JobOffer GetJobOffer(int ID)
        {
            return unitOfWork.Repository<JobOffer>().GetById(ID);
        }

        public List<JobOffer> GetJobOffer(Expression<Func<JobOffer, bool>> Predicate)
        {
            return unitOfWork.Repository<JobOffer>().Get(Predicate);
        }

        public List<JobOffer> GetJobOffer()
        {
            return unitOfWork.Repository<JobOffer>().GetAll();
        }

        public void InsertJobOffer(JobOffer jobOffer)
        {
            unitOfWork.Repository<JobOffer>().Insert(jobOffer);
            unitOfWork.Save();
        }

        public void UpdateJobOffer(JobOffer jobOffer)
        {
            unitOfWork.Repository<JobOffer>().Update(jobOffer);
            unitOfWork.Save();
        }

        public void DeleteJobOffer(int ID)
        {
            unitOfWork.Repository<JobOffer>().Delete(ID);
            unitOfWork.Save();
        }
    }
}
