using DaleelElkheir.Admin.Models.Volunteers;
using DaleelElkheir.BLL.Services.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService volunteerService;
         public VolunteerController(IVolunteerService _VolunteerService)
        {
            this.volunteerService = _VolunteerService;
        }
        // GET: Volunteer
        public ActionResult List()
        {
            var volunteers = volunteerService.GetVolunteers();
            var model = volunteers.Select(s => new VolunteerListModel()
            {
                Name=s.Name,
                Contact=s.Contact,
                Email=s.Email,
                Job=s.Job,
                Nationality=s.Nationality,
                DaysAvailable=s.DaysAvailable,
                AboutQuestion=s.AboutQuestion,
                VolunteeringMethod=s.VoulunteeringMethod,
                Categories=s.VolunteerCategories.Any()? s.VolunteerCategories.Select(w=>w.Category.NameEn).Aggregate((a, x) => a + ", " + x):""
            });
            return View(model);
        }
    }
}