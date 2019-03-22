using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Donations
{
    public interface IDonationService
    {
        #region DonationServices

        Donation GetDonation(int id);

        List<Donation> GetDonations(Expression<Func<Donation, bool>> Predicate);

        List<Donation> GetDonations();

        void InsertDonation(Donation _Donation);

        void UpdateDonation(Donation _Donation);

        void DeleteDonation(int id);

        #endregion
    }
}
