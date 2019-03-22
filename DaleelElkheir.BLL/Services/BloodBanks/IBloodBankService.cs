using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.BloodBanks
{
    public interface IBloodBankService
    {
        #region BloodBankServices
        BloodBank GetBloodBank(int id);

        List<BloodBank> GetBloodBanks(Expression<Func<BloodBank, bool>> Predicate);

        List<BloodBank> GetBloodBanks();

        void InsertBloodBank(BloodBank _bloodBank);

        void UpdateBloodBank(BloodBank _bloodBank);

        void DeleteBloodBank(int id);
        #endregion

        #region BloodBankContactServices
        BloodBankContact GetBloodBankContact(int id);

        List<BloodBankContact> GetBloodBankContacts(Expression<Func<BloodBankContact, bool>> Predicate);
        List<BloodBankContact> GetBloodBankContacts();

        void InsertBloodBankContact(BloodBankContact _BloodBankContact);

        void UpdateBloodBankContact(BloodBankContact _BloodBankContact);
        void DeleteBloodBankContact(int id);

        #endregion
    }
}
