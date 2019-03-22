using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Products
{
    public interface IProductServices
    {
        #region ProductServices

        Product GetProduct(int id);

        List<Product> GetProduct(Expression<Func<Product, bool>> Predicate);

        List<Product> GetProduct();

        void InsertProduct(Product _Product);

        void UpdateProduct(Product _Product);

        void DeleteProduct(int id);

        #endregion
    }
}
