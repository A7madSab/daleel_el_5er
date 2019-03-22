using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Informations
{
    public class InformationService : IInformationService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public InformationService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public Information GetInformation(int id)
        {
            return unitOfWork.Repository<Information>().GetById(id);
        }

        public List<Information> GetInformation(Expression<Func<Information, bool>> Predicate)
        {
            return unitOfWork.Repository<Information>().Get(Predicate);
        }

        public List<Information> GetInformations()
        {
            return unitOfWork.Repository<Information>().GetAll();
        }

        public void InsertInformation(Information _Information)
        {
            unitOfWork.Repository<Information>().Insert(_Information);
            unitOfWork.Save();
        }

        public void UpdateInformation(Information _Information)
        {
            unitOfWork.Repository<Information>().Update(_Information);
            unitOfWork.Save();
        }
        public void DeleteInformation(int id)
        {
            unitOfWork.Repository<Information>().Delete(id);
            unitOfWork.Save();
        }

    }
}
