using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Chat;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.ChatThreads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class ChatController : ApiController
    {
        readonly private IChatThreadService chatService;
        readonly private ICaseService caseService;

        public ChatController(IChatThreadService _chatService, ICaseService _caseService)
        {
            this.chatService = _chatService;
            this.caseService = _caseService;
        }

        [HttpPost]
        public IHttpActionResult GetThreadsByUser(ChatThreadRequest request)
        {
            if (ModelState.IsValid)
            {
                var chatTread = chatService.GetChatThread(x => x.UserID == request.UserID)
                    //.OrderByDescending(od=>od.ChatThreadMessages==null ? null :od.ChatThreadMessages.Select(s=>s.SendDate))
                    .Select(y => new ChatThreadModel { UserID = y.UserID, ID = y.CaseID, Name = caseService.GetCase(y.CaseID).NameEn });
                return Ok(new BaseResponse(chatTread));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetOldMessges(ChatRequest request)
        {
            if (ModelState.IsValid)
            {
                var chatTread = chatService.GetChatThread(x => x.UserID == request.UserID && x.CaseID == request.CaseID).FirstOrDefault();
                if (chatTread != null)
                {
                    var chatThreadMessages = chatService.GetChatThreadMessage(x => x.ThreadID == chatTread.ID).OrderByDescending(y=>y.SendDate).Select(x=> new ChatModel {isAdmin=x.AdminID==null?false:true,Message=x.Message});

                    return Ok(new BaseResponse(chatThreadMessages));
                }
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }
    }
}
