using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Sellers
{
    public class SellerServices : ISellerServices
    {
        private readonly IUnitOfWork unitOfWork;

        public SellerServices(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public void DeleteSeller(int id)
        {
            unitOfWork.Repository<Seller>().Delete(id);
            unitOfWork.Save();
        }

        public Seller GetSeller(int id)
        {
            return unitOfWork.Repository<Seller>().GetById(id);
        }

        public List<Seller> GetSeller(Expression<Func<Seller, bool>> Predicate)
        {
            return unitOfWork.Repository<Seller>().Get(Predicate);
        }

        public List<Seller> GetSeller()
        {
            return unitOfWork.Repository<Seller>().GetAll();
        }

        public void InsertSeller(Seller _Seller)
        {
            unitOfWork.Repository<Seller>().Insert(_Seller);
            unitOfWork.Save();
        }

        public void UpdateSeller(Seller _Seller)
        {
            unitOfWork.Repository<Seller>().Update(_Seller);
            unitOfWork.Save();
        }
    }
}
