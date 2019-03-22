using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.TrusteesBoards
{
    public class TrusteesBoardService : ITrusteesBoardService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public TrusteesBoardService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public TrusteesBoard GetTrusteesBoard(int id)
        {
            return unitOfWork.Repository<TrusteesBoard>().GetById(id);
        }

        public List<TrusteesBoard> GetTrusteesBoard(Expression<Func<TrusteesBoard, bool>> Predicate)
        {
            return unitOfWork.Repository<TrusteesBoard>().Get(Predicate);
        }

        public List<TrusteesBoard> GetTrusteesBoards()
        {
            return unitOfWork.Repository<TrusteesBoard>().GetAll();
        }

        public void InsertTrusteesBoard(TrusteesBoard _TrusteesBoard)
        {
            unitOfWork.Repository<TrusteesBoard>().Insert(_TrusteesBoard);
            unitOfWork.Save();
        }

        public void UpdateTrusteesBoard(TrusteesBoard _TrusteesBoard)
        {
            unitOfWork.Repository<TrusteesBoard>().Update(_TrusteesBoard);
            unitOfWork.Save();
        }
        public void DeleteTrusteesBoard(int id)
        {
            unitOfWork.Repository<TrusteesBoard>().Delete(id);
            unitOfWork.Save();
        }

    }
}
