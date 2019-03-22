using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
namespace DaleelElkheir.BLL.Services.OurStories
{
    public class OurStoryService : IOurStoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public OurStoryService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public OurStory GetOurStory(int Id)
        {
            return unitOfWork.Repository<OurStory>().GetById(Id);
        }

        public List<OurStory> GetOurStory(Expression<Func<OurStory, bool>> Predicate)
        {
            return unitOfWork.Repository<OurStory>().Get(Predicate);
        }

        public List<OurStory> GetOurStory()
        {
            return unitOfWork.Repository<OurStory>().GetAll();
        }

        public void InsertOurStory(OurStory Story)
        {
            unitOfWork.Repository<OurStory>().Insert(Story);
            unitOfWork.Save();
        }

        public void UpdateOurStory(OurStory Story)
        {
            unitOfWork.Repository<OurStory>().Update(Story);
            unitOfWork.Save();
        }

        public void DeleteOurStory(int Id)
        {
            unitOfWork.Repository<OurStory>().Delete(Id);
            unitOfWork.Save();
        }
    }
}
