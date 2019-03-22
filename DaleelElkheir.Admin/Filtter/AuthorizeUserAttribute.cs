using DaleelElkheir.Admin.Chating;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DaleelElkheir.Admin.Filtter
{
   
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private string[] UserRoles { get; set; }

        public AuthorizeUserAttribute(params string[] roless) : base()
        {
            this.Roles = string.Join(",", roless);
            HttpContext.Current.Session["User"] = null;
        }
        // Custom property
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            UserRoles = Roles.Split(Convert.ToChar(","));

            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                    var dd = HttpContext.Current.User.Identity.Name;
                    bool res = false;
                    foreach(var rol in UserRoles)
                    {
                        if (HttpContext.Current.User.IsInRole(rol.ToString()))
                        {
                            DaleelElkheirModel db = new DaleelElkheirModel();
                            UnitOfWork userService = new UnitOfWork(db);
                            var user = userService.Repository<User>().Get(x=>x.Email==dd).FirstOrDefault();
                            HttpContext.Current.Session["User"] = user;                            
                            res = true;
                            break;
                        }

                    }
                    return res;
                }
                else
                {
                    HttpContext.Current.Session["User"] = null;
                    return false;
                }
            }
            else
            {
                HttpContext.Current.Session["User"] = null;
                return false;
            }

        }



        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "User",
                                action = "Login"
                            })
                        );
        }


    }
}