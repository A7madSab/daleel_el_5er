using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.ProductCategories
{
    public class ProductCategoryServices : IProductCategoryServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductCategoryServices(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public void DeleteProductCategory(int id)
        {
            unitOfWork.Repository<ProductCategory>().Delete(id);
            unitOfWork.Save();
        }

        public ProductCategory GetProductCategory(int id)
        {
            return unitOfWork.Repository<ProductCategory>().GetById(id);
        }

        public List<ProductCategory> GetProductCategory(Expression<Func<ProductCategory, bool>> Predicate)
        {
            return unitOfWork.Repository<ProductCategory>().Get(Predicate);
        }

        public List<ProductCategory> GetProductCategory()
        {
            return unitOfWork.Repository<ProductCategory>().GetAll();
        }

        public void InsertProductCategory(ProductCategory _ProductCategory)
        {
            unitOfWork.Repository<ProductCategory>().Insert(_ProductCategory);
            unitOfWork.Save();
        }

        public void UpdateProductCategory(ProductCategory _ProductCategory)
        {
            unitOfWork.Repository<ProductCategory>().Update(_ProductCategory);
            unitOfWork.Save();
        }
    }
}
