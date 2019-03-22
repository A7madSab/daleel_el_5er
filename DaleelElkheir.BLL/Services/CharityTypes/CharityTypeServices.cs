using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.BLL.Services.CharityTypes
{
    public class CharityTypeServices : ICharityTypeServices
    {
        private readonly IUnitOfWork unitOfWork;

        public CharityTypeServices(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public void DeleteCharity(int id)
        {
            unitOfWork.Repository<CharityType>().Delete(id);
            unitOfWork.Save();
        }

        public CharityType GetCharityType(int id)
        {
            return unitOfWork.Repository<CharityType>().GetById(id);
        }

        public List<CharityType> GetCharityType()
        {
            return unitOfWork.Repository<CharityType>().GetAll();
        }

        public List<CharityType> GetCharityTypes(Expression<Func<CharityType, bool>> Predicate)
        {
            return unitOfWork.Repository<CharityType>().Get(Predicate);
        }

        public void InsertCharity(CharityType _CharityType)
        {
            unitOfWork.Repository<CharityType>().Insert(_CharityType);
            unitOfWork.Save();
        }

        public void UpdateCharity(CharityType _CharityType)
        {
            unitOfWork.Repository<CharityType>().Update(_CharityType);
            unitOfWork.Save();
        }
    }
}
