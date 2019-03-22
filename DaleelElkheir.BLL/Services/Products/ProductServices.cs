using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductServices(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }


        public void DeleteProduct(int id)
        {
            unitOfWork.Repository<Product>().Delete(id);
            unitOfWork.Save();
        }

        public Product GetProduct(int id)
        {
            return unitOfWork.Repository<Product>().GetById(id);
        }

        public List<Product> GetProduct(Expression<Func<Product, bool>> Predicate)
        {
            return unitOfWork.Repository<Product>().Get(Predicate);
        }

        public List<Product> GetProduct()
        {
            return unitOfWork.Repository<Product>().GetAll();
        }

        public void InsertProduct(Product _Product)
        {
            unitOfWork.Repository<Product>().Insert(_Product);
            unitOfWork.Save();
        }

        public void UpdateProduct(Product _Product)
        {
            unitOfWork.Repository<Product>().Update(_Product);
            unitOfWork.Save();
        }
    }
}
