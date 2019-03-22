using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.OurPrograms
{
    public interface IOurProgramService
    {
        OurProgram GetOurProgram(int id);
        List<OurProgram> GetOurProgram(Expression<Func<OurProgram, bool>> Predicate);

        List<OurProgram> GetOurPrograms();
        void InsertOurProgram(OurProgram _OurProgram);
        void UpdateOurProgram(OurProgram _OurProgram);
        void DeleteOurProgram(int id);

    }
}
