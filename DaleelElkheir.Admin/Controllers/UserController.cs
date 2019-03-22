using DaleelElkheir.Admin.Chating;
using DaleelElkheir.Admin.Filtter;
using DaleelElkheir.Admin.Models.Users;
using DaleelElkheir.BLL.Services.ChatThreads;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.BLL.Type;
using DaleelElkheir.DAL.Domain;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DaleelElkheir.Admin.Controllers
{
    [AuthorizeUser(Roles = "DaleelElkheir")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IChatThreadService chatService;

        private readonly IOrganizationService OrganizationService;


        public UserController(IUserService _userService, IOrganizationService _OrganizationService, IChatThreadService _ChatService)
        {
            this.userService = _userService;
            this.OrganizationService = _OrganizationService;
            this.chatService = _ChatService;
        }
        public ActionResult UserList()
        {
            IList<SelectListItem> OrgList = OrganizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Value = x.NameEn, Text = x.NameEn }).ToList();
            OrgList.Insert(0, new SelectListItem { Text = "select Organization", Value = "" });
            ViewBag.Organization = OrgList;

            var users = userService.GetUsers();

            return View(users);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    var User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                    var Email = User.Identity.Name;
                    var _user = userService.GetUsers(x => x.Email == Email).FirstOrDefault();
                    Session["User"] = _user;
                    ViewBag.connectedUser = _user.ID;                  

                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult CreateUser()
        {
            
            ViewBag.Organization = "";

            IList<SelectListItem> UserTypeList = userService.GetUserTypes().Where(x=>x.ID!=3).Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            UserTypeList.Insert(0, new SelectListItem { Text = "Select User Type", Value = "" });
            ViewBag.UserType = UserTypeList;

            return View();
        }

        public ActionResult CreateUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                
                var _User = new User();
                _User.Name = model.Name;
                _User.Email = model.Email;
                _User.Password = model.Password;
                _User.UserName = model.UserName;
                _User.Mobile = model.Mobile;
                _User.UserTypeID = model.UserTypeID;
                _User.OrganizationID = model.OrganizationID;
               

                userService.InsertUser(_User);
                return RedirectToAction("UserList");
            }
            return RedirectToAction("CreateUser");
        }


        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login_Model model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = userService.GetUsers(x => x.Email == model.Email && x.Password == model.Password && (x.UserTypeID == 1 || x.UserTypeID == 2)).FirstOrDefault();

            if (user != null)
            {
                Session["User"] = user;
                FormsAuthentication.SetAuthCookie(model.Email, false);

                var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(200000), false, user.UserType.Name);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "User");
            }

            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["User"] = null;
            // return RedirectToAction("Login");

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult UpdateUser(int UserID)
        {
            IList<SelectListItem> OrganizationList = OrganizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Text = x.NameEn, Value = x.ID.ToString() }).ToList();
            OrganizationList.Insert(0, new SelectListItem { Text = "Select Organization", Value = "" });
            ViewBag.Organization = OrganizationList;

            IList<SelectListItem> UserTypeList = userService.GetUserTypes().Where(x => x.ID != 3).Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            UserTypeList.Insert(0, new SelectListItem { Text = "Select User Type", Value = "" });
            ViewBag.UserType = UserTypeList;

            var _User = userService.GetUser(UserID);


            var _UserModel = new UserModel()
            {
                ID = _User.ID,
                Name = _User.Name,
                Email = _User.Email,
                Password = _User.Password,
                UserName = _User.UserName,
                Mobile = _User.Mobile,
                UserTypeID=_User.UserTypeID,
                OrganizationID=_User.OrganizationID
                 
            };
            return View(_UserModel);
        }

        public ActionResult UpdateUser(UserModel model)
        {
     
            var _User = new User()
            {
                ID = model.ID,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                Mobile = model.Mobile,                             
                UserTypeID=model.UserTypeID,
                OrganizationID=model.OrganizationID

            };
            userService.UpdateUser(_User);
            return RedirectToAction("UserList");
        }

        public ActionResult DeleteUser(int UserID)
        {

            //deleting chat thread
            var chatThreads = chatService.GetChatThreads(w => w.UserID == UserID);
            foreach (var thread in chatThreads)
            {
                chatService.DeleteChatThread(thread.ID);
            }
            userService.DeleteUser(UserID);
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public ActionResult GetOrganizations()
        {
            IList<SelectListItem> OrganizationList = OrganizationService.GetOrganizations(OrgStatus.Approved).Select(x => new SelectListItem { Text = x.NameEn, Value = x.ID.ToString() }).ToList();
                    
            return Json(OrganizationList, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult DeleteUser(int UserID)
        //{

        //    // userService.DeleteUser(UserID);
        //    //var res = "true";
        //    return Json(new { result = false, message = "the record is already in use" }, JsonRequestBehavior.AllowGet);
        //}
    }
}