using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Categories
{
    public class CategoryService: ICategoryService
    {
        #region Category
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public Category GetCategory(int id)
        {
            return unitOfWork.Repository<Category>().GetById(id);
        }

        public List<Category> GetCategories(Expression<Func<Category, bool>> Predicate)
        {
            return unitOfWork.Repository<Category>().Get(Predicate);
        }

        public List<Category> GetCategories()
        {
            return unitOfWork.Repository<Category>().GetAll();
        }

        public void InsertCategory(Category _Category)
        {
            unitOfWork.Repository<Category>().Insert(_Category);
            unitOfWork.Save();
        }

        public void UpdateCategory(Category _Category)
        {
            unitOfWork.Repository<Category>().Update(_Category);
            unitOfWork.Save();
        }
        public void DeleteCategory(int id)
        {
            unitOfWork.Repository<Category>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

    }
}
