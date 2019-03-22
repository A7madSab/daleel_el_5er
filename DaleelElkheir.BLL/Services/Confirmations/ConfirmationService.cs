using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Confirmations
{
    public class ConfirmationService : IConfirmationService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public ConfirmationService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public CaseConfirmation GetCaseConfirmation(int id)
        {
            return unitOfWork.Repository<CaseConfirmation>().GetById(id);
        }

        public List<CaseConfirmation> GetCaseConfirmation(Expression<Func<CaseConfirmation, bool>> Predicate)
        {
            return unitOfWork.Repository<CaseConfirmation>().Get(Predicate);
        }

        public List<CaseConfirmation> GetCaseConfirmations()
        {
            return unitOfWork.Repository<CaseConfirmation>().GetAll();
        }

     
    }
}
