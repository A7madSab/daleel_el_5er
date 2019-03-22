using DaleelElkheir.Admin.Chating;
using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Chat;
using DaleelElkheir.Admin.TriggerNotifications;
using DaleelElkheir.BLL.Services.ChatThreads;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class ChatThreadController : Controller
    {
        private readonly IChatThreadService chatThreadService;

        public ChatThreadController(IChatThreadService _chatThreadService)
        {
            this.chatThreadService = _chatThreadService;
        }
        public ActionResult ChatThreadMessageRoom()
        {
            LetsChatHub dd = new LetsChatHub();

            string userList = string.Empty; 
            var ccc= dd.UserList;
   
            foreach(var item in ccc)
            {
                userList=userList+item.UserName+",";
            }
            ViewBag.connectedUser = userList;

            var currentUser = (Session["User"] as User).ID;
            ViewBag.currentUser = currentUser;

            var chatThread = chatThreadService.GetChatThreads()
                .OrderByDescending(od=>od.ChatThreadMessages.OrderByDescending(iod=>iod.SendDate).FirstOrDefault()?.SendDate)
                .Select(x => new ChatThreadModel()
                {
                    HelpCase = x.HelpCase,
                    User = x.User,
                    ID = x.ID,
                    UserID = x.UserID,
                    CaseID = x.CaseID,
                    Message = x.ChatThreadMessages.Where(y => y.ThreadID == x.ID).Select(z => z.Message).ToList(),
                    SeenCount = x.ChatThreadMessages.Where(y => y.ThreadID == x.ID && y.Seen == 0).Count()
                }).OrderBy(o=>o.CreationDate);
            return View(chatThread);
        }

        public ActionResult GetMessages(int ThreadID)
        {
            var chatThread = chatThreadService.GetChatThreadMessage(x => x.ThreadID == ThreadID);
            List<string> messagesList = new List<string>();
            foreach(var item in chatThread)
            {
                string mess ="<strong>"+(item.AdminID == null ? item.ChatThread.User.Name : item.User.Name)+ ": </strong >" + item.Message;
                messagesList.Add(mess);
            }
            var UnReaded = chatThread.Where(z=>z.Seen==0);
            foreach(var item in UnReaded)
            {
                item.Seen = 1;
                chatThreadService.UpdateChatThreadMessage(item);
            }
            return Json(messagesList, JsonRequestBehavior.AllowGet);
        }
    }
}