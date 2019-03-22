using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using System.Linq.Expressions;

namespace DaleelElkheir.BLL.Services.Sellers
{
    public interface ISellerServices
    {
        #region SellerServices

        Seller GetSeller(int id);

        List<Seller> GetSeller(Expression<Func<Seller, bool>> Predicate);

        List<Seller> GetSeller();

        void InsertSeller(Seller _Seller);

        void UpdateSeller(Seller _Seller);

        void DeleteSeller(int id);

        #endregion
    }
}
