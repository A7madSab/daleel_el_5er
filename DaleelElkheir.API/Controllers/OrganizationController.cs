using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Organizations;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Type;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class OrganizationController : ApiController
    {
        private readonly IOrganizationService OrganizationService;

        public OrganizationController(IOrganizationService _OrganizationService)
        {
            this.OrganizationService = _OrganizationService;
        }

        public List<CategModel> getGategoryByOrg(int orgId,string lang)
        {
            var CatList= OrganizationService.GetOrganizationCategorys(orgId);
            List<CategModel> CategoryModelList = new List<CategModel>();
            foreach (var item in CatList)
            {
                CategoryModelList.Add(new CategModel() {Name=lang == "ar" ? item.NameAr : item.NameEn });
            }
            return CategoryModelList;
        }

        [HttpPost]
        public IHttpActionResult GetOrganizations(BaseRequest request)
        {
            if(ModelState.IsValid)
            {
                var organizations = OrganizationService.GetOrganizations(OrgStatus.Approved);
                List<OrganizationModel> OrganizationList = new List<OrganizationModel>();
                foreach (var item in organizations)
                {
                    var organizationModel = new OrganizationModel
                    {
                        ID = item.ID,
                        Name = request.Lang == "ar" ? item.NameAr : item.NameEn,
                        Logo = item.FileData != null ? item.FileData.Extenstion : null,
                        Address = request.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Governorate = request.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Area = request.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Categories = getGategoryByOrg(item.ID, request.Lang),
                        DescriptionProgram = item.DescriptionProgram,
                        HowToDonate = item.HowToDonate,
                    };
                    OrganizationList.Add(organizationModel);
                }
                return Ok(new BaseResponse(OrganizationList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetOrganizationsName(BaseRequest request)
        {
            if (ModelState.IsValid)
            {
                var organizations = OrganizationService.GetOrganizations(OrgStatus.Approved).Select(m=> new {ID=m.ID,Name=request.Lang == "ar" ? m.NameAr : m.NameEn });

                return Ok(new BaseResponse(organizations));
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetOrganizationAcount()
        {
            if (ModelState.IsValid)
            {
                var OrganizationAcount = OrganizationService.GetOrganizations(OrgStatus.Approved).Count();
                return Ok(new BaseResponse(OrganizationAcount));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetFilterOrganizations(OrganizationFilterRequest request)
        {
            if (ModelState.IsValid)
            {
                var organizations = OrganizationService.GetOrganizations(OrgStatus.Approved);


                if (request.RegionID != null)
                {
                    organizations = organizations.Where(x => x.CityID == request.RegionID).ToList();
                }

                if (request.GovernorateID != null)
                {
                    organizations = organizations.Where(x => x.City.GovernorateID == request.GovernorateID).ToList();
                }

                if (request.CategoryID != null)
                {
                    var org = OrganizationService.GetOrganizationCategory(x=>x.CategoryID==request.CategoryID).Select(y=>y.OrgID);
                    organizations = organizations.Where(x => org.Contains(x.ID)).ToList();
                    
                }


                List<OrganizationModel> OrganizationList = new List<OrganizationModel>();
                foreach (var item in organizations)
                {
                    var organizationModel = new OrganizationModel
                    {

                        ID = item.ID,
                        Name = request.Lang == "ar" ? item.NameAr : item.NameEn,
                        Logo = item.FileData != null ? item.FileData.Extenstion : null,
                        Address = request.Lang == "ar" ? item.AddressAr : item.AddressEn,
                        Description = request.Lang == "ar" ? item.DescriptionAr : item.DescriptionEn,
                        Governorate = request.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        Area = request.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Categories = getGategoryByOrg(item.ID, request.Lang),
                        DescriptionProgram = item.DescriptionProgram,
                        HowToDonate = item.HowToDonate,
                    };
                    OrganizationList.Add(organizationModel);
                }
                return Ok(new BaseResponse(OrganizationList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetOrganizationsByRegion(OrganizationRegionRequest request)
        {
            if (ModelState.IsValid)
            {
                int statusID = (Int32)OrgStatus.Approved;   
                var organizations = OrganizationService.GetOrganizations(x=>x.CityID==request.RegionID && x.Status==statusID);
                List<OrganizationsByRegionModel> OrganizationList = new List<OrganizationsByRegionModel>();
                foreach (var item in organizations)
                {
                    var organizationModel = new OrganizationsByRegionModel
                    {

                        ID = item.ID,
                        Name = request.Lang == "ar" ? item.NameAr : item.NameEn,
                        Area = request.Lang == "ar" ? item.City.NameAr : item.City.NameEn,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    };
                    OrganizationList.Add(organizationModel);
                }
                return Ok(new BaseResponse(OrganizationList));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult RegisterOrganization(OrganizationSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                OrganizationService.InsertOrganization(new Organization()
                {
                    NameEn=model.NameEn,
                    NameAr=model.NameAr,
                    AddressEn=model.AddressEn,
                    AddressAr=model.AddressAr,
                    DescriptionEn=model.DescriptionEn,
                    DescriptionAr=model.DescriptionAr,
                    Longitude=model.Longitude,
                    Latitude=model.Latitude,
                    CityID=model.CityID,
                    AreaID=model.AreaID,
                    Status=(Int32)OrgStatus.Created
                });
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }


    }
}
