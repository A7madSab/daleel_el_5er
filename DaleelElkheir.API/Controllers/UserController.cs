using DaleelElkheir.API.Filter;
using DaleelElkheir.API.Infrastructure;
using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Cases;
using DaleelElkheir.API.Models.Categories;
using DaleelElkheir.API.Models.Organizations;
using DaleelElkheir.API.Models.Users;
using DaleelElkheir.BLL.Services.ChatThreads;
using DaleelElkheir.BLL.Services.DeviceTokens;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        private readonly IOrganizationService organizationService;
        private readonly IFileDataService FileDataService;
        private readonly IDeviceTokenService deviceTokenService;
        private readonly IChatThreadService chatThreadService;
        public UserController(IUserService _userService, IOrganizationService _organizationService, IFileDataService _FileDataService, IDeviceTokenService _deviceTokenService, IChatThreadService _chatThreadService)
        {
            this.userService = _userService;
            this.organizationService = _organizationService;
            this.FileDataService = _FileDataService;
            this.deviceTokenService = _deviceTokenService;
            this.chatThreadService = _chatThreadService;
        }

        [HttpPost, AllowAnonymous]
        public IHttpActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var userLogin = userService.DoLogin(loginModel.Email, loginModel.Password, loginModel.DeviceToken,loginModel.Facebook_ID,loginModel.google_ID);
                if (userLogin != null)
                {
                    Login_Request login = new Login_Request() { UserID = userLogin.ID, SecurityToken = userLogin.UserDevices.FirstOrDefault().SecurityToken, userType = userLogin.UserType.ID };
                    return Ok(new BaseResponse(login));
                }
                else
                {
                    if (loginModel.Password != "")
                    {
                        return Ok(new BaseResponse(HttpStatusCode.NotFound, "Email or Password incorrect"));
                    }
                    else if(loginModel.Facebook_ID != "")
                    {
                        return Ok(new BaseResponse(HttpStatusCode.NotFound, "facebook ID not registered"));
                    }
                    else if (loginModel.google_ID != "")
                    {
                        return Ok(new BaseResponse(HttpStatusCode.NotFound, "google ID not registered"));
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult UpdateDeviceToken(UpdateDeviceTokenModel deviceTokenModel)
        {
            if (ModelState.IsValid)
            {
                bool res = userService.UpdateDeviceToken(deviceTokenModel.SecurityToken, deviceTokenModel.DeviceToken);
                if (res)
                {
                    return Ok(new BaseResponse());
                }
                else
                {
                    return Ok(new BaseResponse(HttpStatusCode.NotFound, "Security Token Not Found"));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult Logout(LogoutModel logoutModel)
        {
            if (ModelState.IsValid)
            {
                var res = userService.DoLogout(logoutModel.SecurityToken);
                if (res)
                {
                    return Ok(new BaseResponse());
                }
                else
                {
                    return Ok(new BaseResponse(HttpStatusCode.NotFound, "Logout failed"));
                }
            }
            return BadRequest(ModelState);
        }

        // [HttpPost, AuthorizationRequired]
        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetUserProfile(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUsers(x => x.ID == model.UserID).FirstOrDefault();
                if (user != null && user.UserTypeID == 3)
                {
                    var profileModel = new UserProfileModel
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Mobile = user.Mobile,
                        Image = user.FileData != null ? user.FileData.FileBinary : null,
                    };
                    return Ok(new BaseResponse(profileModel));
                }
                else
                {
                    return Ok(new BaseResponse(HttpStatusCode.NotFound, "User Not Found"));
                }

            }
            return BadRequest(ModelState);

         }


        public IHttpActionResult ForgetPassword(ForgetPasswordRequest Request)
        {

            if (ModelState.IsValid)
            {
                User user = userService.GetUsers(q => q.Email == Request.Email.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    Random r = new Random();
                    int code = r.Next(1000, 9999);
                    user.VerifyCode = code.ToString();
                    userService.UpdateUser(user);
                    MailSender mail = new MailSender();
                    mail.SendMail(user.Email, "Forgot Password", "Your code is " + code);
                    return Ok(new BaseResponse());
                }
                else
                {
                    return Ok(new BaseResponse("User Doesn't exist"));
                }
            }
            return BadRequest(ModelState);
        
        }

        public IHttpActionResult VerifyCode(VerifyCodeRequest Request)
        {

            if (ModelState.IsValid)
            {
                User user = userService.GetUsers().FirstOrDefault(q => q.Email.ToLower() == Request.Email.ToLower());

                if (user != null)
                {
                    string VCode = Request.code.ToString();
                    if (user.VerifyCode == VCode)
                    {
                        return Ok(new BaseResponse());
                    }
                    else
                    {
                        return Ok(new BaseResponse("Code is incorrect"));
                    }
                }
                else
                {
                    return Ok(new BaseResponse("no user with this email"));
                }
            }
            return BadRequest(ModelState);
        }

        public IHttpActionResult ResetPassword(ResetPasswordRequest Request)
        {
            
            if(ModelState.IsValid)
            {

                User user = userService.GetUsers().FirstOrDefault(q => q.Email.ToLower() == Request.Email.ToLower() && (q.VerifyCode == Request.Code));
                if (user != null)
                {
                    user.Password = Request.Password;
                    userService.UpdateUser(user);
                    return Ok(new BaseResponse());
                }
                else
                {
                    return Ok(new BaseResponse("User doesn't exist"));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult EditUserProfile(EditProfileModel request)
        {
            if (ModelState.IsValid)
            {
                User UserObj = userService.GetUser(request.UserID);

                byte[] imageData = Convert.FromBase64String(request.Image); 
                FileData image = new FileData();

                if (request.Image.Length > 0)
                {

                    if (UserObj.ImageID != null)
                    {
                        image = new FileData()
                        {
                            ID = int.Parse(UserObj.ImageID.ToString()),
                            FileBinary = imageData
                        };

                        FileDataService.UpdateFileData(image);
                    }
                    else
                    {
                        image = new FileData()
                        {
                            FileBinary = imageData
                        };

                        FileDataService.InsertFileData(image);
                    }
                }
                UserObj.Name = request.Name;
                UserObj.UserName = request.Name;
                UserObj.Password = request.Password;
                UserObj.Mobile = request.Mobile;
                UserObj.Facebook_ID = request.Facebook_ID;
                UserObj.Google_ID = request.Google_ID;
                UserObj.ImageID = image.ID != 0 ? image.ID : UserObj.ImageID;

                userService.UpdateUser(UserObj);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }


        //[HttpPost, AuthorizationRequired]
        //public IHttpActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = userService.GetUsers(x => x.ID == model.UserID && x.Password == model.OldPassword).FirstOrDefault();
        //        if (user != null)
        //        {
        //            user.Password = model.NewPassword;
        //            userService.UpdateUser(user);
        //            return Ok(new BaseResponse());
        //        }
        //        else
        //        {
        //            return Ok(new BaseResponse(HttpStatusCode.NotFound, "Password Not Correct"));
        //        }
        //    }
        //    return BadRequest(ModelState);
        //}

        //[HttpPost]
        //public IHttpActionResult ForgotPassword(ForgotPasswordRequest model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = userService.GetUsers(m => m.Email == model.Email).FirstOrDefault();
        //        if (user != null)
        //        {
        //            Thread MailThread = new Thread(delegate ()
        //            {
        //                string body = string.Format("User: {0} need to reset his password", model.Email);
        //                string mailSubject = "Change password request";
        //                string mailBody = body;
        //                MailSender mailSender = new MailSender();
        //                mailSender.SendMail(ConfigurationManager.AppSettings["SendMail.From"].ToString(), mailSubject, mailBody, false);
        //            });
        //            MailThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
        //            MailThread.Start();
        //            return Ok(new BaseResponse());
        //        }
        //        return Ok(new BaseResponse(HttpStatusCode.InternalServerError, "Invalid Email"));
        //    }
        //    return BadRequest(ModelState);

        //}

        [HttpPost]
        public IHttpActionResult ContactUs(ContactRequest model)
        {
            if (ModelState.IsValid)
            {
                Thread MailThread = new Thread(delegate ()
                {
                    string body = string.Format("New Message \n Name: {0}\n Email : {1}\n Mobile: {2} \n. DonorIn: {3} \n. Message: {4}", model.Name ,model.Email.ToString(), model.Mobile, model.DonorIn, model.Message);
                    string mailSubject = string.Format(" New {0}", "Message");
                    string mailBody = body;
                    MailSender mailSender = new MailSender();
                    mailSender.SendMail(ConfigurationManager.AppSettings["SendMail.From"].ToString(), mailSubject, mailBody, false);
                });
                MailThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
                MailThread.Start();
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult ContactDaleelElKheirPharmacy(ContactRequest model)
        {
            if (ModelState.IsValid)
            {
                string messageType = "Donor";
                switch (model.Type)
                {
                    case "1":
                        messageType = "Donor";
                        break;
                    case "2":
                        messageType = "Need";
                        break;
                    default:
                        messageType = "Donor";
                        break;
                }
                Thread MailThread = new Thread(delegate ()
                {
                    string body = string.Format("New Message \n Name: {0}\n Email : {1}\n Mobile: {2} \n. Type: {3} \n. Message: {4}", model.Name, model.Email.ToString(), model.Mobile, messageType, model.Message);
                    string mailSubject = string.Format(" New {0}", "Message");
                    string mailBody = body;
                    MailSender mailSender = new MailSender();
                    mailSender.SendMail(ConfigurationManager.AppSettings["SendMail.From"].ToString(), mailSubject, mailBody, false);
                });
                MailThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
                MailThread.Start();
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Password=="" && model.Facebook_ID=="" && model.Google_ID=="")     
                {
                    return Ok(new BaseResponse("you must insert Password Or Facebook_ID Or Google_ID"));
                }
                else
                {
                    var usr = userService.GetUsers(x=>(x.Email==model.Email&& model.Email!="") ||(x.Facebook_ID==model.Facebook_ID && model.Facebook_ID!="") ||(x.Google_ID==model.Google_ID && model.Google_ID!="")).FirstOrDefault();
                    if (usr == null)
                    {
                        var imag = new FileData();
                        if (model.Image != "")
                        {
                            byte[] img = Convert.FromBase64String(model.Image);

                            if (model.Image.Length > 0)
                            {

                                imag = new FileData()
                                {
                                    FileBinary = img
                                };

                                FileDataService.InsertFileData(imag);
                            }
                        }
                        User newUser = new User()
                        {
                            Name = model.Name,
                            UserName=model.Name,
                            Email = model.Email,
                            Password = model.Password,
                            Mobile = model.Mobile,
                            Facebook_ID = model.Facebook_ID,
                            Google_ID = model.Google_ID,
                            UserTypeID = 3,

                        };
                        if (imag.ID != 0)
                            newUser.ImageID = imag.ID;

                        userService.InsertUser(newUser);
                        return Ok(new BaseResponse());
                    }
                    else
                    {
                        if (usr.Password != "")
                        {
                            return Ok(new BaseResponse("you already registered"));
                        }
                        else if (usr.Facebook_ID != "")
                        {
                            return Ok(new BaseResponse("you already registered by facebook"));
                        }
                        else if (usr.Google_ID != "")
                        {
                            return Ok(new BaseResponse("you already registered by google"));
                        }
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetFavoriteCategoryForUser(UserModel model)
        {
            if(ModelState.IsValid)
            {
                var userCategories = userService.GetFavoriteCategoryForUser(model.UserID);
                List<CategoryModel> categoryList = new List<CategoryModel>();
                foreach(var item in userCategories)
                {
                    var CatModel = new CategoryModel() {
                         ID=item.ID,
                         Name=model.Lang=="ar"?item.NameAr:item.NameEn
                    };
                    categoryList.Add(CatModel);
                }
                return Ok(new BaseResponse(categoryList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult AddFavoriteCategoryForUser(FavoriteCategoryModel model)
        {
            if(ModelState.IsValid)
            {
                var _userCategory = userService.GetUserCategories(x => x.UserID == model.UserID && x.CategoryID == model.CategoryID);

                if (_userCategory.Count > 0)
                {
                    return Ok(new BaseResponse("you joined this Category before"));
                }
                else
                {
                    var userCategory = new UserCategory() { UserID = model.UserID, CategoryID = model.CategoryID };
                    userService.InsertUserCategory(userCategory);
                    return Ok(new BaseResponse("you joined this Category"));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult CategoryUnfollow(FavoriteCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var userCategory = userService.GetUserCategories(x=>x.UserID==model.UserID&&x.CategoryID==model.CategoryID).FirstOrDefault();
                if(userCategory!=null)
                userService.DeleteUserCategory(userCategory.ID);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult OrganizationUnfollow(FavoriteOrganizationModel model)
        {
            if (ModelState.IsValid)
            {
                var userOrganization = userService.GetUserOrganizations(x => x.UserID == model.UserID && x.OrgID == model.OrganizationID).FirstOrDefault();
                if(userOrganization!=null)
                userService.DeleteUserOrganization(userOrganization.ID);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult CasesUnFollow(ParticipateCasesModel model)
        {
            if (ModelState.IsValid)
            {
                var userCases = userService.GetUserCases(x => x.UserID == model.UserID && x.CaseID == model.CaseID).FirstOrDefault();
                if(userCases!=null)
                 userService.DeleteUserCase(userCases.ID);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }


        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetFavoriteOrganizationForUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var userOrganizations = userService.GetFavoriteOrganizationForUser(model.UserID);
                List<OrganizationModel> OrganizationList = new List<OrganizationModel>();
                foreach (var item in userOrganizations)
                {
                    var CatModel = new OrganizationModel()
                    {
                        ID = item.ID,
                        Name = model.Lang == "ar" ? item.NameAr : item.NameEn,
                        Logo = item.FileData != null ? item.FileData.Extenstion : null,
                        Address = model.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        Description = model.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Governorate = model.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Area = model.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Categories = getGategoryByOrg(item.ID, model.Lang)

                    };
                    OrganizationList.Add(CatModel);
                }
                return Ok(new BaseResponse(OrganizationList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetUnfollowOrganizationForUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var userOrganizations = userService.GetUnfollowOrganizationForUser(model.UserID);
                List<OrganizationModel> OrganizationList = new List<OrganizationModel>();
                foreach (var item in userOrganizations)
                {
                    var CatModel = new OrganizationModel()
                    {
                        ID = item.ID,
                        Name = model.Lang == "ar" ? item.NameAr : item.NameEn,
                        Logo = item.FileData != null ? item.FileData.Extenstion : null,
                        Address = model.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        Description = model.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Governorate = model.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Area = model.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Categories = getGategoryByOrg(item.ID, model.Lang)

                    };
                    OrganizationList.Add(CatModel);
                }
                return Ok(new BaseResponse(OrganizationList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetUnfollowCategoryForUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var userCategories = userService.GetUnfollowCategoryForUser(model.UserID);
                List<CategoryModel> categoryList = new List<CategoryModel>();
                foreach (var item in userCategories)
                {
                    var CatModel = new CategoryModel()
                    {
                        ID = item.ID,
                        Name = model.Lang == "ar" ? item.NameAr : item.NameEn
                    };
                    categoryList.Add(CatModel);
                }
                return Ok(new BaseResponse(categoryList));
            }
            return BadRequest(ModelState);
        }

        public List<CategModel> getGategoryByOrg(int orgId, string lang)
        {
            var CatList = organizationService.GetOrganizationCategorys(orgId);
            List<CategModel> CategoryModelList = new List<CategModel>();
            foreach (var item in CatList)
            {

                CategoryModelList.Add(new CategModel() { Name = lang == "ar" ? item.NameAr : item.NameEn });
            }
            return CategoryModelList;
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult AddFavoriteOrganizationForUser(FavoriteOrganizationModel model)
        {
            if (ModelState.IsValid)
            {
                var _userOrganization = userService.GetUserOrganizations(x=>x.UserID == model.UserID &&x.OrgID == model.OrganizationID);

                if (_userOrganization.Count > 0)
                {
                    return Ok(new BaseResponse("you joined this Organization before"));
                }
                else
                {
                    var userOrganization = new UserOrg() { UserID = model.UserID, OrgID = model.OrganizationID };
                    userService.InsertUserOrganization(userOrganization);
                    return Ok(new BaseResponse("you joined this Organization"));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetParticipateCasesForUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var userCases = userService.GetParticipateCasesForUser(model.UserID);
                List<CaseModel> casesList = new List<CaseModel>();
                foreach (var item in userCases)
                {
                    var CasModel = new CaseModel()
                    {
                        ID = item.ID,
                        Name = model.Lang == "ar" ? item.NameAr : item.NameEn,
                        Organization = model.Lang == "ar" ? item.Organization.NameAr : item.Organization.NameEn,
                        Category = model.Lang == "ar" ? item.Category.NameAr : item.Category.NameEn,
                        CaseType = model.Lang == "ar" ? item.CaseType.NameAr : item.CaseType.NameEn,
                        CaseStatus = item.CaseStatu.Name,
                        City = model.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Governorate = model.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Image = item.FileData != null ? item.FileData.Extenstion : null,
                        DueDate = item.DueDate != null ? item.DueDate.Value.ToShortDateString() : ""
                    };
                    casesList.Add(CasModel);
                }
                return Ok(new BaseResponse(casesList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult AddParticipateCasesForUser(ParticipateCasesModel model)
        {
            if (ModelState.IsValid)
            {
                var _UserCases = userService.GetUserCases(x=>x.UserID == model.UserID && x.CaseID == model.CaseID);
                if (_UserCases.Count > 0)
                {
                    return Ok(new BaseResponse("you joined this Case before"));
                }
                else
                {
                    var userCases = new UserCase() { UserID = model.UserID, CaseID = model.CaseID };
                    userService.InsertUserCase(userCases);

                    var userThread = chatThreadService.GetChatThread(x => x.UserID == model.UserID && x.CaseID == model.CaseID);

                    ChatThread newThread = new ChatThread();
                    if (userThread.Count == 0)
                    {

                        newThread.CreationDate = DateTime.Now;
                        newThread.UserID = model.UserID;
                        newThread.CaseID = model.CaseID;
                        chatThreadService.InsertChatThread(newThread);
                    }

                    return Ok(new BaseResponse("you joined this Case"));

                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult Subscribe(SubscribeRequest model)
        {
            if (ModelState.IsValid)
            {
                Thread MailThread = new Thread(delegate ()
                {
                    string body = string.Format("New Subscription \n  Email : {0}",model.Email.ToString());
                    string mailSubject = string.Format(" New {0}", "Subscription");
                    string mailBody = body;
                    MailSender mailSender = new MailSender();
                    mailSender.SendMail(ConfigurationManager.AppSettings["SendSubscripeMail.From"].ToString(), mailSubject, mailBody, false);
                });
                MailThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
                MailThread.Start();
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult AddDeviceToken(DeviceTokenRequest request)
        {
            if (ModelState.IsValid)
            {
                DeviceToken token = new DeviceToken() { DeviceTokenKey = request.DeviceTokenKey, UserKey = Guid.NewGuid() };
                deviceTokenService.InsertDeviceToken(token);
                return Ok(new BaseResponse(token));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult UpdateDeviceTokenKey(UpdateDeviceTokenRequest request)
        {
            if (ModelState.IsValid)
            {
                var tok = deviceTokenService.GetDeviceToken(x => x.UserKey == request.UserKey).FirstOrDefault();
                if (tok != null)
                {
                    tok.DeviceTokenKey = request.DeviceTokenKey;
                    deviceTokenService.UpdateDeviceToken(tok);
                }
                return Ok(new BaseResponse(tok));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult RemoveDeviceToken(RemoveTokenRequest request)
        {
            if (ModelState.IsValid)
            {
                var tok = deviceTokenService.GetDeviceToken(x => x.UserKey == request.UserKey).FirstOrDefault();
                if (tok != null)
                {
                    deviceTokenService.DeleteDeviceToken(tok.ID);
                }
                return Ok(new BaseResponse(tok));
            }
            return BadRequest(ModelState);
        }

    }
}
