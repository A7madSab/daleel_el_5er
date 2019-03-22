using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Volunteers
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork unitOfWork;

        public VolunteerService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public List<volunteer> GetVolunteers()
        {
            return unitOfWork.Repository<volunteer>().GetAll();
        }

        public void InsertVolunteer(volunteer model)
        {
            unitOfWork.Repository<volunteer>().Insert(model);
            unitOfWork.Save();
        }
        
    }
}
