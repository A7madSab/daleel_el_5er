using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Keywords
{
    public class KeyWordServices : IKeyworkServices
    {
        private readonly IUnitOfWork unitOfWork;

        public KeyWordServices(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public void DeleteKeyWord(int id)
        {
            unitOfWork.Repository<KeyWord>().Delete(id);
            unitOfWork.Save();
        }

        public KeyWord GetKeyWord(int id)
        {
            return unitOfWork.Repository<KeyWord>().GetById(id);
        }

        public List<KeyWord> GetKeyWord(Expression<Func<KeyWord, bool>> Predicate)
        {
            return unitOfWork.Repository<KeyWord>().Get(Predicate);
        }

        public List<KeyWord> GetKeyWord()
        {
            return unitOfWork.Repository<KeyWord>().GetAll();
        }

        public void InsertKeyWord(KeyWord _Guide)
        {
            unitOfWork.Repository<KeyWord>().Insert(_Guide);
            unitOfWork.Save();
        }

        public void UpdateKeyWord(KeyWord _Guide)
        {
            unitOfWork.Repository<KeyWord>().Update(_Guide);
            unitOfWork.Save();
        }

        public void InsertKeyWord(List<KeyWord>_Guide)
        {
            unitOfWork.Repository<KeyWord>().Insert(_Guide);
            unitOfWork.Save();
        }

        public void DeleteKeyWords(IEnumerable<KeyWord> KeyWords)
        {
            unitOfWork.Repository<KeyWord>().Delete(KeyWords);
        }
    }
}
