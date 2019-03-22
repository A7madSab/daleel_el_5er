using DaleelElkheir.API.Infrastructure;
using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Volunteers;
using DaleelElkheir.BLL.Services.Volunteers;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class VolunteerController : ApiController
    {
        private readonly IVolunteerService volunteerService;
        public VolunteerController(IVolunteerService _VolunteerService)
        {
            this.volunteerService = _VolunteerService;
        }

        public IHttpActionResult AddVolunteer(VolunteerSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                volunteer entity = new volunteer()
                {
                    Name = model.Name,
                    Contact = model.Contact,
                    Email = model.Email,
                    Job=model.Job,
                    Nationality=model.Nationality,
                    DaysAvailable=model.DaysAvailable,
                    AboutQuestion=model.AboutQuestion,
                    VoulunteeringMethod=model.VoulunteeringMethod
                };
                if (model.Categories.Count > 1)
                {
                    foreach (var category in model.Categories)
                    {
                        entity.VolunteerCategories.Add(new VolunteerCategory()
                        {
                            CategoryID = category
                        });
                    }
                }
                volunteerService.InsertVolunteer(entity);
                string mailBody = $"Name: {entity.Name} <br/> Contact: {entity.Contact} <br/> Email: {entity.Email} <br/> Job: {entity.Job} <br/> Nationality: {entity.Nationality} <br/> DaysAvailable: {entity.DaysAvailable} <br/> How did you know about us? : {entity.AboutQuestion} <br/> How would you prefer to volunteer? {entity.VoulunteeringMethod}";
                new MailSender().SendMail(ConfigurationManager.AppSettings["VolunteersReceiver"].ToString(), "New Volunteer Added", mailBody);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }
    }
}
