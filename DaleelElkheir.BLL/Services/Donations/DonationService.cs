using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Donations
{
    public class DonationService : IDonationService
    {
        private readonly IUnitOfWork unitOfWork;

        public DonationService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public void DeleteDonation(int id)
        {
            unitOfWork.Repository<Donation>().Delete(id);
            unitOfWork.Save();
        }

        public Donation GetDonation(int id)
        {
            return unitOfWork.Repository<Donation>().GetById(id);
        }

        public List<Donation> GetDonations(Expression<Func<Donation, bool>> Predicate)
        {
            return unitOfWork.Repository<Donation>().Get(Predicate);
        }

        public List<Donation> GetDonations()
        {
            return unitOfWork.Repository<Donation>().GetAll();
        }

        public void InsertDonation(Donation _Donation)
        {
            unitOfWork.Repository<Donation>().Insert(_Donation);
            unitOfWork.Save();
        }

        public void UpdateDonation(Donation _Donation)
        {
            unitOfWork.Repository<Donation>().Update(_Donation);
            unitOfWork.Save();
        }
    }
}
