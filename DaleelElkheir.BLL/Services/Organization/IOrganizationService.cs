using DaleelElkheir.BLL.Type;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Organizations
{
    public interface IOrganizationService
    {
        #region OrganizationServices
        Organization GetOrganization(int id);

        List<Organization> GetOrganizations(Expression<Func<Organization, bool>> Predicate);

        List<Organization> GetOrganizations(OrgStatus? status = null);

        void InsertOrganization(Organization _Organization);

        void UpdateOrganization(Organization _Organization);

        void DeleteOrganization(int id);

        List<Category> GetOrganizationCategorys(int OrgID);

        List<OrganizationCategory> GetOrganizationCategory(Expression<Func<OrganizationCategory, bool>> Predicate);
        #endregion

        #region OrganizationCategory
        OrganizationCategory GetOrganizationCategory(int id);

        List<OrganizationCategory> GetOrganizationCategorys(Expression<Func<OrganizationCategory, bool>> Predicate);

        List<OrganizationCategory> GetOrganizationCategory();

        void InsertOrganizationCategory(OrganizationCategory _OrgCateg);

        void UpdateOrganizationCategory(OrganizationCategory _OrgCateg);
        void DeleteOrganizationCategory(int id);

        #endregion
    }
}
