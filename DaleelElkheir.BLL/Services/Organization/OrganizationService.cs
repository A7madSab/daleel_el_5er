using DaleelElkheir.BLL.Type;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Organizations
{
    public class OrganizationService : IOrganizationService
    {
        
        private readonly IUnitOfWork unitOfWork;

        public OrganizationService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        #region Organization
        public Organization GetOrganization(int id)
        {
            return unitOfWork.Repository<Organization>().GetById(id);
        }

        public List<Organization> GetOrganizations(Expression<Func<Organization, bool>> Predicate)
        {
            return unitOfWork.Repository<Organization>().Get(Predicate);
        }

        public List<Organization> GetOrganizations(OrgStatus? status=null)
        {
            
            int statusID = 0;
            if (status!=null)
            {
                statusID = (Int32)status;
            }
            return unitOfWork.Repository<Organization>().Get(w=>w.Status==(status==null ? w.Status :  statusID));
        }

        public void InsertOrganization(Organization _Organization)
        {
            unitOfWork.Repository<Organization>().Insert(_Organization);
            unitOfWork.Save();
        }

        public void UpdateOrganization(Organization _Organization)
        {
            unitOfWork.Repository<Organization>().Update(_Organization);
            unitOfWork.Save();
        }
        public void DeleteOrganization(int id)
        {
            unitOfWork.Repository<Organization>().Delete(id);
            unitOfWork.Save();
        }

        public List<Category> GetOrganizationCategorys(int OrgID)
        {
             var OrgCatList= unitOfWork.Repository<OrganizationCategory>().Get(x=>x.OrgID==OrgID);
             var CatList=unitOfWork.Repository<Category>().GetAll();

              return (from m in CatList
                     from s in OrgCatList
                     where m.ID == s.CategoryID
                     select m).ToList();
       

        }

        public List<OrganizationCategory> GetOrganizationCategory(Expression<Func<OrganizationCategory, bool>> Predicate)
        {
            return unitOfWork.Repository<OrganizationCategory>().Get(Predicate);
        }

        #endregion


        #region OrganizationCategory
        public OrganizationCategory GetOrganizationCategory(int id)
        {
            return unitOfWork.Repository<OrganizationCategory>().GetById(id);
        }

        public List<OrganizationCategory> GetOrganizationCategorys(Expression<Func<OrganizationCategory, bool>> Predicate)
        {
            return unitOfWork.Repository<OrganizationCategory>().Get(Predicate);
        }

        public List<OrganizationCategory> GetOrganizationCategory()
        {
            return unitOfWork.Repository<OrganizationCategory>().GetAll();
        }

        public void InsertOrganizationCategory(OrganizationCategory _OrgCateg)
        {
            unitOfWork.Repository<OrganizationCategory>().Insert(_OrgCateg);
            unitOfWork.Save();
        }

        public void UpdateOrganizationCategory(OrganizationCategory _OrgCateg)
        {
            unitOfWork.Repository<OrganizationCategory>().Update(_OrgCateg);
            unitOfWork.Save();
        }
        public void DeleteOrganizationCategory(int id)
        {
            unitOfWork.Repository<OrganizationCategory>().Delete(id);
            unitOfWork.Save();
        }

        #endregion


    }
}
