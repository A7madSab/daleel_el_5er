using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Users
{
    public interface IUserService
    {
        //User Services
        User GetUser(int id);
        List<User> GetUsers();
        List<User> GetUsers(Expression<Func<User, bool>> predicate);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User DoLogin(string Email, string Password, string DeviceToken, string facebook_ID, string google_ID);
        bool DoLogout(Guid SecurityToken);
        bool UpdateDeviceToken(Guid SecurityToken, string DeviceToken);

        bool ValidateSecurityToken(Guid securityToken);


       
        //UserDevice Services
        void InsertUserDvice(UserDevice userDevice);
        void UpdateUserDevice(UserDevice userDevice);
        void DeleteUserDevice(int Id);
        UserDevice GetToken(Guid token);
        List<UserDevice> GetUsersDevices();
        //User Type
        List<UserType> GetUserTypes();

        #region UserCategory

        List<UserCategory> GetUserCategory();
        List<UserCategory> GetUserCategories(Expression<Func<UserCategory, bool>> predicate);
        void InsertUserCategory(UserCategory _userCategory);

        void UpdateUserCategory(UserCategory _UserCategory);

        void DeleteUserCategory(int Id);

        List<Category> GetFavoriteCategoryForUser(int userID);
        #endregion

        #region UserOrganization

        List<UserOrg> GetUserOrganization();
        List<UserOrg> GetUserOrganizations(Expression<Func<UserOrg, bool>> predicate);
        void InsertUserOrganization(UserOrg _UserOrganization);

        void UpdateUserOrganization(UserOrg _UserOrganization);

        void DeleteUserOrganization(int Id);

        List<Organization> GetFavoriteOrganizationForUser(int userID);

        List<Organization> GetUnfollowOrganizationForUser(int userID);
        #endregion

        #region UserCases
        List<UserCase> GetUserCases();
        List<UserCase> GetUserCases(Expression<Func<UserCase, bool>> predicate);
        void InsertUserCase(UserCase _UserCase);

        void UpdateUserCase(UserCase _UserCase);

        void DeleteUserCase(int Id);

        List<HelpCase> GetParticipateCasesForUser(int userID);

        List<Category> GetUnfollowCategoryForUser(int userID);
        #endregion


    }
}
