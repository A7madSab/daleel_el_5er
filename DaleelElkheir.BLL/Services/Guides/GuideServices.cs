using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Guides
{
    public class GuideServices : IGuideServices
    {
        private readonly IUnitOfWork unitOfWork;

        public GuideServices(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public void DeleteGuide(int id)
        {
            unitOfWork.Repository<Guide>().Delete(id);
            unitOfWork.Save();
        }


        public Guide GetGuide(int id)
        {
            return unitOfWork.Repository<Guide>().GetById(id);
        }

        public List<Guide> GetGuide(Expression<Func<Guide, bool>> Predicate)
        {
            return unitOfWork.Repository<Guide>().Get(Predicate);
        }

        public List<Guide> GetGuide()
        {
            return unitOfWork.Repository<Guide>().GetAll();
        }

        public void InsertGuide(Guide _Guide)
        {
            unitOfWork.Repository<Guide>().Insert(_Guide);
            unitOfWork.Save();
        }

        public void UpdateGuide(Guide _Guide)
        {
            unitOfWork.Repository<Guide>().Update(_Guide);
            unitOfWork.Save();
        }
    }
}
