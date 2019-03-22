using DaleelElkheir.DAL.Repository;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DaleelElkheir.BLL.Services.JobOffers
{
    public interface IJobOfferService
    {
        #region JobOffers

        JobOffer GetJobOffer(int ID);

        List<JobOffer> GetJobOffer(Expression<Func<JobOffer, bool>> Predicate);

        List<JobOffer> GetJobOffer();

        void InsertJobOffer(JobOffer _JobOffer);

        void UpdateJobOffer(JobOffer _JobOffer);

        void DeleteJobOffer(int ID);
        #endregion
    }
}
