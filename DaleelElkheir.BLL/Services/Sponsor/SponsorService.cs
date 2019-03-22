using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Sponsors
{
    public class SponsorService : ISponsorService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public SponsorService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public Sponsor GetSponsor(int id)
        {
            return unitOfWork.Repository<Sponsor>().GetById(id);
        }

        public List<Sponsor> GetSponsor(Expression<Func<Sponsor, bool>> Predicate)
        {
            return unitOfWork.Repository<Sponsor>().Get(Predicate);
        }

        public List<Sponsor> GetSponsors()
        {
            return unitOfWork.Repository<Sponsor>().GetAll();
        }

        public void InsertSponsor(Sponsor _Sponsor)
        {
            unitOfWork.Repository<Sponsor>().Insert(_Sponsor);
            unitOfWork.Save();
        }

        public void UpdateSponsor(Sponsor _Sponsor)
        {
            unitOfWork.Repository<Sponsor>().Update(_Sponsor);
            unitOfWork.Save();
        }
        public void DeleteSponsor(int id)
        {
            unitOfWork.Repository<Sponsor>().Delete(id);
            unitOfWork.Save();
        }

    }
}
