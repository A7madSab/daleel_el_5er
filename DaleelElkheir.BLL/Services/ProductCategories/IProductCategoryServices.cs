using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.BLL.Services.ProductCategories
{
    public interface IProductCategoryServices
    {
        #region ProductCategoryServices
        ProductCategory GetProductCategory(int id);

        List<ProductCategory> GetProductCategory(Expression<Func<ProductCategory, bool>> Predicate);

        List<ProductCategory> GetProductCategory();

        void InsertProductCategory(ProductCategory _ProductCategory);

        void UpdateProductCategory(ProductCategory _ProductCategory);

        void DeleteProductCategory(int id);
        #endregion
    }
}
