using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.OurPrograms
{
    public class OurProgramService : IOurProgramService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public OurProgramService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public OurProgram GetOurProgram(int id)
        {
            return unitOfWork.Repository<OurProgram>().GetById(id);
        }

        public List<OurProgram> GetOurProgram(Expression<Func<OurProgram, bool>> Predicate)
        {
            return unitOfWork.Repository<OurProgram>().Get(Predicate);
        }

        public List<OurProgram> GetOurPrograms()
        {
            return unitOfWork.Repository<OurProgram>().GetAll();
        }

        public void InsertOurProgram(OurProgram _OurProgram)
        {
            unitOfWork.Repository<OurProgram>().Insert(_OurProgram);
            unitOfWork.Save();
        }

        public void UpdateOurProgram(OurProgram _OurProgram)
        {
            unitOfWork.Repository<OurProgram>().Update(_OurProgram);
            unitOfWork.Save();
        }
        public void DeleteOurProgram(int id)
        {
            unitOfWork.Repository<OurProgram>().Delete(id);
            unitOfWork.Save();
        }

    }
}
