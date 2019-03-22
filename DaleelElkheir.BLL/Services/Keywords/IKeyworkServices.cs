using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using System.Linq.Expressions;

namespace DaleelElkheir.BLL.Services.Keywords
{
    public interface IKeyworkServices
    {
        #region GuideServices

        KeyWord GetKeyWord(int id);

        List<KeyWord> GetKeyWord(Expression<Func<KeyWord, bool>> Predicate);

        List<KeyWord> GetKeyWord();

        void InsertKeyWord(KeyWord _Guide);

        void UpdateKeyWord(KeyWord _Guide);

        void DeleteKeyWord(int id);

        void DeleteKeyWords(IEnumerable<KeyWord> keyWords);

        void InsertKeyWord(List<KeyWord> _Guide);


        #endregion
    }
}
