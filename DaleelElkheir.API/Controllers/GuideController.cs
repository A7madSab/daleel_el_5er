using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Guide;
using DaleelElkheir.API.Models.Keyword;
using DaleelElkheir.BLL.Services.Guides;
using DaleelElkheir.BLL.Services.Keywords;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    [RoutePrefix("api/guide")]
    public class GuideController : ApiController
    {
        private readonly IGuideServices guideServices;
        private readonly IKeyworkServices keyworkServices;

        public GuideController(IGuideServices _IGuideServices, IKeyworkServices keyworkServices)
        {
            this.guideServices = _IGuideServices;
            this.keyworkServices = keyworkServices;
        }

        [HttpGet, Route("get")]
        public IHttpActionResult get()
        {
            var guides = guideServices.GetGuide();
            List<GuideModel> guideModels = new List<GuideModel>();
            string words = "";

            foreach (Guide g in guides)
            {
                var keywords = keyworkServices.GetKeyWord(x => x.GuideID == g.ID);

                foreach (KeyWord k in keywords)
                {
                    words += k.Word + ' ';
                }
                var model = new GuideModel
                {
                    ID = g.ID,
                    Description = g.Description,
                    Ext = g.Ext,
                    FileName = g.FileName,
                    Name = g.Name,
                    KeyWords = words,
                };
                guideModels.Add(model);
            }
            return Ok(new BaseResponse(guideModels));
        }

        [HttpGet,Route("get/{id}")]
        public IHttpActionResult get(int id )
        {

            var guide = guideServices.GetGuide(id);
            var keyword = keyworkServices.GetKeyWord(x => x.GuideID == id);
            string words = "";
            foreach(KeyWord k in keyword)
            {
                words += k.Word + ' ' ;
            }

            GuideModel model = new GuideModel
            {
                ID = guide.ID,
                Description = guide.Description,
                Ext = guide.Ext,
                FileName = guide.FileName,
                Name = guide.Name,
                KeyWords = words
            };


            return Ok(new BaseResponse(model));
        }

        [HttpPost, Route("get")]
        public IHttpActionResult getByWord(KeywordModel keyWord)
        {
            string[] words = keyWord.Words.Split(' ');
            List<Guide> keywords = new List<Guide>();

            foreach (string word in words)
            {
                //keywords.AddRange( keyworkServices.GetKeyWord(x => x.Guide.KeyWords.Contains(new KeyWord { Word = word })));
                //keywords.AddRange(guideServices.GetGuide(x => x.KeyWords.Contains(new KeyWord { Word = word })));
            }

            var ReturnObj = keywords.Select(x => new GuideModel
            {
                Description = x.Description,
                Ext = x.Ext,
                FileName = x.FileName,
                ID = x.ID,
                Name = x.Name,
            });

            return Ok(new BaseResponse(ReturnObj));
        }
    }
}
