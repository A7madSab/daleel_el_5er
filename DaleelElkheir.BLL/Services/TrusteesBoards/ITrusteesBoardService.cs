using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.TrusteesBoards
{
    public interface ITrusteesBoardService
    {
        TrusteesBoard GetTrusteesBoard(int id);
        List<TrusteesBoard> GetTrusteesBoard(Expression<Func<TrusteesBoard, bool>> Predicate);

        List<TrusteesBoard> GetTrusteesBoards();
        void InsertTrusteesBoard(TrusteesBoard _TrusteesBoard);
        void UpdateTrusteesBoard(TrusteesBoard _TrusteesBoard);
        void DeleteTrusteesBoard(int id);

    }
}
