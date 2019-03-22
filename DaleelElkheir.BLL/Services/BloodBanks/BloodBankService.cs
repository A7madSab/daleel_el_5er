using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.BloodBanks
{
    public class BloodBankService:IBloodBankService
    {
        #region BloodBank
        private readonly IUnitOfWork unitOfWork;

        public BloodBankService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public BloodBank GetBloodBank(int id)
        {
            return unitOfWork.Repository<BloodBank>().GetById(id);
        }

        public List<BloodBank> GetBloodBanks(Expression<Func<BloodBank, bool>> Predicate)
        {
            return unitOfWork.Repository<BloodBank>().Get(Predicate);
        }

        public List<BloodBank> GetBloodBanks()
        {
            return unitOfWork.Repository<BloodBank>().GetAll();
        }

        public void InsertBloodBank(BloodBank _bloodBank)
        {
            unitOfWork.Repository<BloodBank>().Insert(_bloodBank);
            unitOfWork.Save();
        }

        public void UpdateBloodBank(BloodBank _bloodBank)
        {
            unitOfWork.Repository<BloodBank>().Update(_bloodBank);
            unitOfWork.Save();
        }
        public void DeleteBloodBank(int id)
        {
            unitOfWork.Repository<BloodBank>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

        #region BloodBankContacts
        public BloodBankContact GetBloodBankContact(int id)
        {
            return unitOfWork.Repository<BloodBankContact>().GetById(id);
        }

        public List<BloodBankContact> GetBloodBankContacts(Expression<Func<BloodBankContact, bool>> Predicate)
        {
            return unitOfWork.Repository<BloodBankContact>().Get(Predicate);
        }

        public List<BloodBankContact> GetBloodBankContacts()
        {
            return unitOfWork.Repository<BloodBankContact>().GetAll();
        }

        public void InsertBloodBankContact(BloodBankContact _BloodBankContact)
        {
            unitOfWork.Repository<BloodBankContact>().Insert(_BloodBankContact);
            unitOfWork.Save();
        }

        public void UpdateBloodBankContact(BloodBankContact _BloodBankContact)
        {
            unitOfWork.Repository<BloodBankContact>().Update(_BloodBankContact);
            unitOfWork.Save();
        }
        public void DeleteBloodBankContact(int id)
        {
            unitOfWork.Repository<BloodBankContact>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

    }
}
