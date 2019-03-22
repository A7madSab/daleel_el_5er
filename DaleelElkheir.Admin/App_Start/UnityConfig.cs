using DaleelElkheir.Admin.TriggerNotifications;
using DaleelElkheir.BLL.Services.AboutUs;
using DaleelElkheir.BLL.Services.BloodBanks;
using DaleelElkheir.BLL.Services.Cases;
using DaleelElkheir.BLL.Services.Categories;
using DaleelElkheir.BLL.Services.ChatThreads;
using DaleelElkheir.BLL.Services.Confirmations;
using DaleelElkheir.BLL.Services.CSRs;
using DaleelElkheir.BLL.Services.DeviceTokens;
using DaleelElkheir.BLL.Services.Events;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.BLL.Services.Hospitals;
using DaleelElkheir.BLL.Services.Informations;
using DaleelElkheir.BLL.Services.Organizations;
using DaleelElkheir.BLL.Services.OurPrograms;
using DaleelElkheir.BLL.Services.OurStories;
using DaleelElkheir.BLL.Services.Regions;
using DaleelElkheir.BLL.Services.SeasonalProjects;
using DaleelElkheir.BLL.Services.Sponsors;
using DaleelElkheir.BLL.Services.TrusteesBoards;
using DaleelElkheir.BLL.Services.Users;
using DaleelElkheir.BLL.Services.Volunteers;
using DaleelElkheir.DAL.Repository;
using DaleelElkheir.BLL.Services.JobOffers;
using DaleelElkheir.BLL.Services.CharityTypes;
using DaleelElkheir.BLL.Services.Donations;
using DaleelElkheir.BLL.Services.Guides;
using DaleelElkheir.BLL.Services.Keywords;
using DaleelElkheir.BLL.Services.Sellers;
using DaleelElkheir.BLL.Services.Products;
using DaleelElkheir.BLL.Services.ProductCategories;
using System;

using Unity;

namespace DaleelElkheir.Admin
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IBloodBankService, BloodBankService>();
            container.RegisterType<IHospitalService, HospitalService>();
            container.RegisterType<ICaseService, CaseService>();
            container.RegisterType<IOrganizationService, OrganizationService>();
            container.RegisterType<IEventService, EventService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IAboutService, AboutService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IRegionService, RegionService>();
            container.RegisterType<IFileDataService, FileDataService>();
            container.RegisterType<IOurProgramService, OurProgramService>();
            container.RegisterType<IInformationService, InformationService>();
            container.RegisterType<ICSRService, CSRService>();
            container.RegisterType<ISponsorService, SponsorService>();
            container.RegisterType<ISeasonalProjectService, SeasonalProjectService>();
            container.RegisterType<ITrusteesBoardService, TrusteesBoardService>();
            container.RegisterType<IConfirmationService, ConfirmationService>();
            container.RegisterType<ITriggerNotificationSender, TriggerNotificationSender>();
            container.RegisterType<IDeviceTokenService, DeviceTokenService>(); 
            container.RegisterType<IChatThreadService, ChatThreadService>();
            container.RegisterType<IVolunteerService, VolunteerService>();
            container.RegisterType<IOurStoryService, OurStoryService>();
            container.RegisterType<IJobOfferService, JobOfferService>();
            container.RegisterType<ICharityTypeServices, CharityTypeServices>();
            container.RegisterType<IDonationService, IDonationService>();
            container.RegisterType<IGuideServices, GuideServices>();
            container.RegisterType<IKeyworkServices, KeyWordServices>();
            container.RegisterType<IProductServices, ProductServices>();
            container.RegisterType<ISellerServices, SellerServices>();
            container.RegisterType<IProductCategoryServices, ProductCategoryServices>();



        }
    }
}