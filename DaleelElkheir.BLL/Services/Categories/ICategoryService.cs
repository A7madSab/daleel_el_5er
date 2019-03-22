using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Categories
{
    public interface ICategoryService
    {
        #region CategoryServices
        Category GetCategory(int id);

        List<Category> GetCategories(Expression<Func<Category, bool>> Predicate);

        List<Category> GetCategories();

        void InsertCategory(Category _Category);

        void UpdateCategory(Category _Category);

        void DeleteCategory(int id);
        #endregion

    }
}
