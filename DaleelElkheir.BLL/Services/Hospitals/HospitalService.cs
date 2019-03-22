using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Hospitals
{
    public class HospitalService : IHospitalService
    {
        #region Hospital
        private readonly IUnitOfWork unitOfWork;

        public HospitalService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public Hospital GetHospital(int id)
        {
            return unitOfWork.Repository<Hospital>().GetById(id);
        }

        public List<Hospital> GetHospitals(Expression<Func<Hospital, bool>> Predicate)
        {
            return unitOfWork.Repository<Hospital>().Get(Predicate);
        }

        public List<Hospital> GetHospitals()
        {
            return unitOfWork.Repository<Hospital>().GetAll();
        }

        public void InsertHospital(Hospital _hospital)
        {
            unitOfWork.Repository<Hospital>().Insert(_hospital);
            unitOfWork.Save();
        }

        public void UpdateHospital(Hospital _hospital)
        {
            unitOfWork.Repository<Hospital>().Update(_hospital);
            unitOfWork.Save();
        }
        public void DeleteHospital(int id)
        {
            unitOfWork.Repository<Hospital>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

        #region HospitalContacts
        public HospitalContact GetHospitalContact(int id)
        {
            return unitOfWork.Repository<HospitalContact>().GetById(id);
        }

        public List<HospitalContact> GetHospitalContacts(Expression<Func<HospitalContact, bool>> Predicate)
        {
            return unitOfWork.Repository<HospitalContact>().Get(Predicate);
        }

        public List<HospitalContact> GetHospitalContacts()
        {
            return unitOfWork.Repository<HospitalContact>().GetAll();
        }

        public void InsertHospitalContact(HospitalContact _HospitalContact)
        {
            unitOfWork.Repository<HospitalContact>().Insert(_HospitalContact);
            unitOfWork.Save();
        }

        public void UpdateHospitalContact(HospitalContact _HospitalContact)
        {
            unitOfWork.Repository<HospitalContact>().Update(_HospitalContact);
            unitOfWork.Save();
        }
        public void DeleteHospitalContact(int id)
        {
            unitOfWork.Repository<HospitalContact>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

    }
}
