using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.BLL.Services.CharityTypes
{
    public interface ICharityTypeServices
    {
        #region AboutServices
        CharityType GetCharityType(int id);

        List<CharityType> GetCharityTypes(Expression<Func<CharityType, bool>> Predicate);

        List<CharityType> GetCharityType();

        void InsertCharity(CharityType _CharityType);

        void UpdateCharity(CharityType _CharityType);

        void DeleteCharity(int id);
        #endregion
    }
}
