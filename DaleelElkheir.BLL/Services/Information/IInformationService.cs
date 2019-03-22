using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Informations
{
    public interface IInformationService
    {
        Information GetInformation(int id);
        List<Information> GetInformation(Expression<Func<Information, bool>> Predicate);

        List<Information> GetInformations();
        void InsertInformation(Information _Information);
        void UpdateInformation(Information _Information);
        void DeleteInformation(int id);

    }
}
