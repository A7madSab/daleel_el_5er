using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Hospitals
{
    public interface IHospitalService
    {
        #region HospitalServices
        Hospital GetHospital(int id);

        List<Hospital> GetHospitals(Expression<Func<Hospital, bool>> Predicate);

        List<Hospital> GetHospitals();

        void InsertHospital(Hospital _hospital);

        void UpdateHospital(Hospital _hospital);

        void DeleteHospital(int id);
        #endregion

        #region HospitalContactServices
        HospitalContact GetHospitalContact(int id);

        List<HospitalContact> GetHospitalContacts(Expression<Func<HospitalContact, bool>> Predicate);
        List<HospitalContact> GetHospitalContacts();

        void InsertHospitalContact(HospitalContact _HospitalContact);

        void UpdateHospitalContact(HospitalContact _HospitalContact);
        void DeleteHospitalContact(int id);

        #endregion
    }
}
