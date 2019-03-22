using DaleelElkheir.Admin.TriggerNotifications;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.ChatThreads;
using DaleelElkheir.BLL.Services.DeviceTokens;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DaleelElkheir.Admin.Chating
{
    //[HubName("letsChatHub")]
    public class LetsChatHub : Hub
    {
        
        private readonly ITriggerNotificationSender triggerNotification;
        private readonly ICaseService caseService;
        private IUserService userService;

        public LetsChatHub()
        {
            
            this.triggerNotification = DependencyResolver.Current.GetService<ITriggerNotificationSender>();
            this.userService = DependencyResolver.Current.GetService<IUserService>();
            this.caseService = DependencyResolver.Current.GetService<ICaseService>();
        }

        public List<ChatUser> UserList
        {
            get
            {
                return LoggedUsers.GetInstance.UserList;
            }
        }

        public void Connect(string userID)
        {
            var id = Context.ConnectionId;
            if (UserList.Count(x => x.UserName == userID) == 0)
            {
                UserList.Add(new ChatUser { ConnectionID = id, UserName = userID });

            }
            Clients.All.AddAvailableUser(userID);
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var connection = UserList.FirstOrDefault(x => x.ConnectionID == Context.ConnectionId);
            var deletedUserName = "";
            if (connection != null)
            {
                deletedUserName = connection.UserName;
                UserList.Remove(connection);
            }
            Clients.All.RemoveUser(deletedUserName);
            return base.OnDisconnected(true);

        }

        //return list of all active connections
        public List<string> GetAllActiveConnections()
        {
            return UserList.Select(S => S.UserName).ToList();
        }

        public void Send(string currentUser, string userID, string caseID, string message)
        {
            DaleelElkheirModel db = new DaleelElkheirModel();
            UnitOfWork chatThreadService = new UnitOfWork(db);
            var currentUserconn = UserList;

            List<string> usersList = new List<string>();
            foreach (var item in currentUserconn)
            {
                var userName = chatThreadService.Repository<User>().GetById(int.Parse(item.UserName));
                if (userName.UserTypeID == 1 || userName.ID == int.Parse(userID))
                {
                    usersList.Add(item.ConnectionID);
                }
            }

            var userNam = "";
            bool IsAdmin = true;
            if (currentUser != "" && userID != "")
            {
                userNam = chatThreadService.Repository<User>().GetById(int.Parse(currentUser)).Name;
                IsAdmin = true;

                if (!UserList.Any(w=>w.UserName==userID))
                {
                    InsertMessageNotification(int.Parse(currentUser), int.Parse(userID), message);
                }
            }
            else if (currentUser == "" && userID != "")
            {
                userNam = chatThreadService.Repository<User>().GetById(int.Parse(userID)).Name;
                IsAdmin = false;
            }
            var chatCase = caseService.GetCase(int.Parse(caseID));
            Clients.Clients(usersList).appendNewMessage(userNam, IsAdmin, message,chatCase.NameEn);

            var usrName = int.Parse(userID);
            var casID = int.Parse(caseID);

            var userThread = chatThreadService.Repository<ChatThread>().Get(x => x.UserID == usrName && x.CaseID == casID);

            ChatThread newThread = new ChatThread();
            if (userThread.Count == 0)
            {

                newThread.CreationDate = DateTime.Now;
                newThread.UserID = int.Parse(userID);
                newThread.CaseID = int.Parse(caseID);
                chatThreadService.Repository<ChatThread>().Insert(newThread);
                chatThreadService.Save();
            }
            else
            {
                newThread = userThread.OrderByDescending(o => o.ID).FirstOrDefault();
            }
            ChatThreadMessage newThreadMessage = new ChatThreadMessage();
            newThreadMessage.SendDate = DateTime.Now;
            newThreadMessage.ThreadID = newThread.ID;
            if (currentUser != "")
            {
                newThreadMessage.AdminID = int.Parse(currentUser);
            }
            newThreadMessage.Message = message;

            var connectionIDs = currentUserconn.Select(y => y.UserName);
            var AdminConnection = chatThreadService.Repository<User>().Get(x => connectionIDs.ToList().Contains(x.ID.ToString()));
            if (!AdminConnection.Any(x => x.UserTypeID == 1))
            {
                newThreadMessage.Seen = 0;
            }
            else
            {
                newThreadMessage.Seen = 1;
            }
            chatThreadService.Repository<ChatThreadMessage>().Insert(newThreadMessage);
            chatThreadService.Save();
        }

        public void InsertMessageNotification(int currentUser, int userId, string message)
        {
            var user = userService.GetUser(userId);
            var UserDevices = userService.GetUsersDevices().Where(x => x.User == user);
            //var UserDevices = user.;
            if (user.UserDevices != null)
            {
                var userDivs = UserDevices.AsQueryable();
                var isSent = triggerNotification.Send(currentUser, userDivs, "New Messgae", message, message, 2);
            }
        }
    }
    public class Users
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
    }

    public class LoggedUsers
    {
        private LoggedUsers() { }

        public List<ChatUser> UserList = null;

        public static LoggedUsers instance = null;

        public static LoggedUsers GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoggedUsers() { UserList = new List<ChatUser>() };
                }
                return instance;
            }
        }

    }

    public class ChatUser
    {
        public string UserName { get; set; }
        public string ConnectionID { get; set; }
    }
}