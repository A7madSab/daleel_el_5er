using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }


        #region User Services
        public User GetUser(int id)
        {
            return unitOfWork.Repository<User>().GetById(id);
        }

        public List<User> GetUsers()
        {
            return unitOfWork.Repository<User>().GetAll();
        }

        public List<User> GetUsers(Expression<Func<User, bool>> predicate)
        {
            return unitOfWork.Repository<User>().Get(predicate);
        }

        public void InsertUser(User user)
        {
            unitOfWork.Repository<User>().Insert(user);
            unitOfWork.Save();
        }

        public void UpdateUser(User user)
        {
            unitOfWork.Repository<User>().Update(user);
            unitOfWork.Save();
        }

        public void DeleteUser(int id)
        {
            unitOfWork.Repository<User>().Delete(id);
            unitOfWork.Save();
        }

        public bool ValidateSecurityToken(Guid securityToken)
        {
            return unitOfWork.Repository<UserDevice>().Get(w => w.SecurityToken == securityToken).Any();
        }

        public User DoLogin(string Email,string Password,string DeviceToken,string facebook_ID,string google_ID)
        {
            var user = GetUsers(w => (w.Email.ToLower() == Email.ToLower() && w.Password == Password)||((w.Facebook_ID==facebook_ID && facebook_ID!=null && facebook_ID!=""))|| ((w.Google_ID == google_ID && google_ID != null && google_ID != ""))).FirstOrDefault();
            if (user != null)
            {
                //Generate Token
                UserDevice userDeviceObj = new UserDevice
                {
                    UserID = user.ID,
                    SecurityToken = Guid.NewGuid(),
                    DeviceToken = DeviceToken
                };

                InsertUserDvice(userDeviceObj);

                return user;
            }
            return null;
        }

        public bool DoLogout(Guid SecurityToken)
        {
            var userDevice = unitOfWork.Repository<UserDevice>().Get(m => m.SecurityToken == SecurityToken); 
            if(userDevice!=null)
            {
                unitOfWork.Repository<UserDevice>().Delete(userDevice);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool UpdateDeviceToken(Guid SecurityToken,string DeviceToken)
        {
            var userDevice=unitOfWork.Repository<UserDevice>().Get(m=>m.SecurityToken==SecurityToken).FirstOrDefault();
            if (userDevice != null)
            {
                userDevice.DeviceToken = DeviceToken;
                unitOfWork.Repository<UserDevice>().Update(userDevice);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

      
        #endregion

        #region UserDevices
        public void InsertUserDvice(UserDevice userDevice)
        {
            unitOfWork.Repository<UserDevice>().Insert(userDevice);
            unitOfWork.Save();
        }

        public void UpdateUserDevice(UserDevice userDevice)
        {
            unitOfWork.Repository<UserDevice>().Update(userDevice);
            unitOfWork.Save();
        }

        public void DeleteUserDevice(int Id)
        {
            unitOfWork.Repository<UserDevice>().Delete(Id);
            unitOfWork.Save();
        }

        public UserDevice GetToken(Guid token)
        {
            var userDevice = unitOfWork.Repository<UserDevice>().Get(m => m.SecurityToken == token).FirstOrDefault();
            return userDevice;
        }

        public List<UserDevice> GetUsersDevices()
        {
            return unitOfWork.Repository<UserDevice>().GetAll().Where(w=>!string.IsNullOrEmpty(w.DeviceToken)).ToList();
        }

        #endregion

        #region UserTypes

        public List<UserType> GetUserTypes()
        {
            return unitOfWork.Repository<UserType>().GetAll();
        }

        //public void InsertUserType(UserType userType)
        //{
        //    unitOfWork.Repository<UserType>().Insert(userType);
        //    unitOfWork.Save();
        //}

        //public void UpdateUserType(UserType userType)
        //{
        //    unitOfWork.Repository<UserType>().Update(userType);
        //    unitOfWork.Save();
        //}

        //public void DeleteUserType(int Id)
        //{
        //    unitOfWork.Repository<UserType>().Delete(Id);
        //    unitOfWork.Save();
        //}
        #endregion

        #region UserCategory

        public List<UserCategory> GetUserCategory()
        {
            return unitOfWork.Repository<UserCategory>().GetAll();
        }

        public List<UserCategory> GetUserCategories(Expression<Func<UserCategory, bool>> predicate)
        {
            return unitOfWork.Repository<UserCategory>().Get(predicate);
        }

        public void InsertUserCategory(UserCategory _userCategory)
        {
            unitOfWork.Repository<UserCategory>().Insert(_userCategory);
            unitOfWork.Save();
        }

        public void UpdateUserCategory(UserCategory _UserCategory)
        {
            unitOfWork.Repository<UserCategory>().Update(_UserCategory);
            unitOfWork.Save();
        }

        public void DeleteUserCategory(int Id)
        {
            unitOfWork.Repository<UserCategory>().Delete(Id);
            unitOfWork.Save();
        }

        public List<Category> GetFavoriteCategoryForUser(int userID)
        {
            var UserCategory = unitOfWork.Repository<UserCategory>().Get(x=>x.UserID==userID);
            var Categories = unitOfWork.Repository<Category>().GetAll();

            var res = (from s in UserCategory
                       join m in Categories on s.CategoryID equals m.ID
                       select m).ToList();
            return res;
        }
        #endregion

        #region UserOrganization

        public List<UserOrg> GetUserOrganization()
        {
            return unitOfWork.Repository<UserOrg>().GetAll();
        }

        public List<UserOrg> GetUserOrganizations(Expression<Func<UserOrg, bool>> predicate)
        {
            return unitOfWork.Repository<UserOrg>().Get(predicate);
        }

        public void InsertUserOrganization(UserOrg _UserOrganization)
        {
            unitOfWork.Repository<UserOrg>().Insert(_UserOrganization);
            unitOfWork.Save();
        }

        public void UpdateUserOrganization(UserOrg _UserOrganization)
        {
            unitOfWork.Repository<UserOrg>().Update(_UserOrganization);
            unitOfWork.Save();
        }

        public void DeleteUserOrganization(int Id)
        {
            unitOfWork.Repository<UserOrg>().Delete(Id);
            unitOfWork.Save();
        }

        public List<Organization> GetFavoriteOrganizationForUser(int userID)
        {
            var UserOrg = unitOfWork.Repository<UserOrg>().Get(x => x.UserID == userID);
            var Organizations = unitOfWork.Repository<Organization>().GetAll();

            var res = (from s in UserOrg
                       join m in Organizations on s.OrgID equals m.ID
                       select m).ToList();
            return res;
        }
        public List<Organization> GetUnfollowOrganizationForUser(int userID)
        {
            var UserOrg = unitOfWork.Repository<UserOrg>().Get(x => x.UserID == userID);
            var remainedOrgs = unitOfWork.Repository<Organization>().GetAll().Except(UserOrg.Select(s=>s.Organization)).ToList();
            return remainedOrgs;
        }

        public List<Category> GetUnfollowCategoryForUser(int userID)
        {
            var UserCateg = unitOfWork.Repository<UserCategory>().Get(x => x.UserID == userID);
            var remainedCategs = unitOfWork.Repository<Category>().GetAll().Except(UserCateg.Select(s => s.Category)).ToList();
            return remainedCategs;
        }
        #endregion

        #region UserCases
        public List<UserCase> GetUserCases()
        {
            return unitOfWork.Repository<UserCase>().GetAll();
        }

        public List<UserCase> GetUserCases(Expression<Func<UserCase, bool>> predicate)
        {
            return unitOfWork.Repository<UserCase>().Get(predicate);
        }
        public void InsertUserCase(UserCase _UserCase)
        {
            unitOfWork.Repository<UserCase>().Insert(_UserCase);
            unitOfWork.Save();
        }

        public void UpdateUserCase(UserCase _UserCase)
        {
            unitOfWork.Repository<UserCase>().Update(_UserCase);
            unitOfWork.Save();
        }

        public void DeleteUserCase(int Id)
        {
            unitOfWork.Repository<UserCase>().Delete(Id);
            unitOfWork.Save();
        }

        public List<HelpCase> GetParticipateCasesForUser(int userID)
        {
            var UserCases = unitOfWork.Repository<UserCase>().Get(x => x.UserID == userID);
            var Cases = unitOfWork.Repository<HelpCase>().GetAll();

            var res = (from s in UserCases
                       join m in Cases on s.CaseID equals m.ID
                       select m).ToList();
            return res;
        }
        #endregion
    }
}
