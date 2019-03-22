using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Confirmations
{
    public interface IConfirmationService
    {
        CaseConfirmation GetCaseConfirmation(int id);
        List<CaseConfirmation> GetCaseConfirmation(Expression<Func<CaseConfirmation, bool>> Predicate);

        List<CaseConfirmation> GetCaseConfirmations();

    }
}
