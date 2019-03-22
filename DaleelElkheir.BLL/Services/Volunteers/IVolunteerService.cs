using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Volunteers
{
    public interface IVolunteerService
    {
        void InsertVolunteer(volunteer model);

        List<volunteer> GetVolunteers();
    }
}
