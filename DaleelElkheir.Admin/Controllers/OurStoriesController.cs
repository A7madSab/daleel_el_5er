    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.OurStory;
using DaleelElkheir.BLL.Services.AboutUs;
using DaleelElkheir.BLL.Services.OurStories;
using DaleelElkheir.DAL.Domain;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class OurStoriesController : Controller
    {
        private readonly IOurStoryService ourStoryService;

        public OurStoriesController(IOurStoryService ourStoryService)
        {

            this.ourStoryService = ourStoryService;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var story = ourStoryService.GetOurStory().FirstOrDefault();
            OurStoryModel ReturnObj = new OurStoryModel
            {
                ID = story.ID,
                BriefArabic= story.BriefArabic,
                BriefEnglish = story.BriefEnglish,
                VideoURL= story.VideoURL,
            };
            return View(ReturnObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OurStoryModel ourStory)
        {
            OurStory ourStoryobj = new OurStory
            {
                ID = ourStory.ID,
                BriefArabic = ourStory.BriefArabic,
                BriefEnglish = ourStory.BriefEnglish,
                VideoURL = ourStory.VideoURL
            };
            ourStoryService.UpdateOurStory(ourStoryobj);
            return RedirectToAction("Edit", "OurStories");
        }

    }
}
