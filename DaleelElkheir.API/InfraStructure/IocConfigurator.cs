using DaleelElkheir.DAL.Repository;
using Unity;
using DaleelElkheir.BLL.Services.BloodBanks;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.BLL.Services.AboutUs;
using DaleelElkheir.BLL.Services.Categories;
using DaleelElkheir.BLL.Services.Regions;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.OurPrograms;
using DaleelElkheir.BLL.Services.Sponsors;
using DaleelElkheir.BLL.Services.CSRs;
using DaleelElkheir.BLL.Services.SeasonalProjects;
using DaleelElkheir.BLL.Services.TrusteesBoards;
using DaleelElkheir.BLL.Services.DeviceTokens;
using DaleelElkheir.BLL.Services.ChatThreads;
using DaleelElkheir.BLL.Services.Volunteers;
using DaleelElkheir.BLL.Services.Hospitals;
using DaleelElkheir.BLL.Services.OurStories;
using DaleelElkheir.BLL.Services.Donations;
using DaleelElkheir.BLL.Services.JobOffers;
using DaleelElkheir.BLL.Services.Guides;
using DaleelElkheir.BLL.Services.Keywords;
using DaleelElkheir.BLL.Services.ProductCategories;
using DaleelElkheir.BLL.Services.Products;
using DaleelElkheir.BLL.Services.Sellers;

namespace DaleelElkheir.API.Infrastructure
{
    public static class IocConfigurator
    {
        public static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IBloodBankService, BloodBankService>();
            container.RegisterType<IHospitalService, HospitalService>();
            container.RegisterType<ICaseService, CaseService>();
            container.RegisterType<IOrganizationService,OrganizationService>();
            container.RegisterType<IEventService, EventService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IAboutService, AboutService>();
            container.RegisterType<ICategoryService,CategoryService>();
            container.RegisterType<IRegionService, RegionService>();
            container.RegisterType<IFileDataService, FileDataService>();
            container.RegisterType<IOurProgramService, OurProgramService>();
            container.RegisterType<ISponsorService, SponsorService>(); 
            container.RegisterType<ICSRService, CSRService>();
            container.RegisterType<ISeasonalProjectService, SeasonalProjectService>();
            container.RegisterType<ITrusteesBoardService, TrusteesBoardService>();
            container.RegisterType<IDeviceTokenService, DeviceTokenService>();
            container.RegisterType<IChatThreadService, ChatThreadService>();
            container.RegisterType<IVolunteerService, VolunteerService>();
            container.RegisterType<IOurStoryService, OurStoryService>();
            container.RegisterType<IDonationService, DonationService>();
            container.RegisterType<IJobOfferService, JobOfferService>();
            container.RegisterType<IGuideServices, GuideServices>();
            container.RegisterType<IKeyworkServices, KeyWordServices>();
            container.RegisterType<IProductCategoryServices, ProductCategoryServices>();
            container.RegisterType<IProductServices, ProductServices>();
            container.RegisterType<ISellerServices, SellerServices>();
        }
    }
}