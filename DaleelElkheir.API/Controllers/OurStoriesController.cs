using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DaleelElkheir.API.Models;
using DaleelElkheir.BLL.Services.OurStories;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.API.Controllers
{
    public class OurStoriesController : ApiController
    {
        private readonly IOurStoryService ourStoryService;

        public OurStoriesController(IOurStoryService _ourStoryService)
        {
            this.ourStoryService = _ourStoryService;
        }

        [HttpGet,AllowAnonymous]
        public IHttpActionResult GetOurStories()
        {
            var c = ourStoryService.GetOurStory().FirstOrDefault();
            return Ok(new BaseResponse(c));
        }
    }
}