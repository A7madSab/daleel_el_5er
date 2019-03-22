using DaleelElkheir.API.Models;
using DaleelElkheir.BLL.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using Unity;

namespace DaleelElkheir.API.Filter
{
    public class AuthorizationRequiredAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private const string Token = "SecurityToken";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            BaseResponse res = new BaseResponse();
            try
            {
               
                var provider = WebApiConfig.container.Resolve<IUserService>();
                

                if (filterContext.Request.Headers.Contains(Token))
                {
                    var tokenValue = filterContext.Request.Headers.GetValues(Token).FirstOrDefault();

                    // Validate Token
                    if (provider != null && provider.ValidateSecurityToken(new Guid(tokenValue)) == false)
                    {

                        res.IsSuccess = false;
                        res.StatusCode = 401;
                        res.ErrorMessage = "You need to login";
                        filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, res);
                    }
                }
                else
                {
                    res.IsSuccess = false;
                    res.StatusCode = 401;
                    res.ErrorMessage = "You need to login";
                    //filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, res);
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                res.ErrorMessage = ex.InnerException.Message ?? ex.Message;
                //filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, res);
            }
            base.OnActionExecuting(filterContext);
        }

    }

}