namespace DaleelElkheir.DAL.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DaleelElkheirModel : DbContext
    {
        public DaleelElkheirModel()
            : base("name=DaleelElkheirConnection")
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<BloodBank> BloodBanks { get; set; }
        public virtual DbSet<BloodBankContact> BloodBankContacts { get; set; }
        public virtual DbSet<CaseConfirmation> CaseConfirmations { get; set; }
        public virtual DbSet<CaseStatu> CaseStatus { get; set; }
        public virtual DbSet<CaseType> CaseTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ChatThread> ChatThreads { get; set; }
        public virtual DbSet<ChatThreadMessage> ChatThreadMessages { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CompanySocialResponsibility> CompanySocialResponsibilities { get; set; }
        public virtual DbSet<CSRActivity> CSRActivities { get; set; }
        public virtual DbSet<DeviceToken> DeviceTokens { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventForActivity> EventForActivities { get; set; }
        public virtual DbSet<EventGallery> EventGalleries { get; set; }
        public virtual DbSet<FileData> FileDatas { get; set; }
        public virtual DbSet<Governorate> Governorates { get; set; }
        public virtual DbSet<HelpCase> HelpCases { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<HospitalContact> HospitalContacts { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationCategory> OrganizationCategories { get; set; }
        public virtual DbSet<OurProgram> OurPrograms { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<SeasonalProject> SeasonalProjects { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<TrusteesBoard> TrusteesBoards { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCase> UserCases { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }
        public virtual DbSet<UserDevice> UserDevices { get; set; }
        public virtual DbSet<UserOrg> UserOrgs { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<volunteer> volunteers { get; set; }
        public virtual DbSet<VolunteerCategory> VolunteerCategories { get; set; }
        public virtual DbSet<OurStory> OurStories { get; set; }
        public virtual DbSet<JobOffer> JobOffers { get; set; }
        public virtual DbSet<CharityType> CharityTypes { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<KeyWord> KeyWord { get; set; }
        public virtual DbSet<Guide> Guide { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.EventForActivities)
                .WithRequired(e => e.Activity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CaseConfirmation>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.CaseConfirmation)
                .HasForeignKey(e => e.ConfirmationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CaseConfirmation>()
                .HasMany(e => e.HelpCases)
                .WithRequired(e => e.CaseConfirmation)
                .HasForeignKey(e => e.ConfirmationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CaseStatu>()
                .HasMany(e => e.HelpCases)
                .WithOptional(e => e.CaseStatu)
                .HasForeignKey(e => e.CaseStatusID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatThread>()
                .HasMany(e => e.ChatThreadMessages)
                .WithRequired(e => e.ChatThread)
                .HasForeignKey(e => e.ThreadID);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanySocialResponsibility>()
                .HasMany(e => e.CSRActivities)
                .WithRequired(e => e.CompanySocialResponsibility)
                .HasForeignKey(e => e.CSR_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventGalleries)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.CompanySocialResponsibilities)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.FileID);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.Events)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.ImageID);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.HelpCases)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.ImageFileID);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.Information)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.ImageID);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.Organizations)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.LogoFileID);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.Sponsors)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.ImageID);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.TrusteesBoards)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.ImageID);

            modelBuilder.Entity<FileData>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.FileData)
                .HasForeignKey(e => e.ImageID);

            modelBuilder.Entity<HelpCase>()
                .HasMany(e => e.ChatThreads)
                .WithRequired(e => e.HelpCase)
                .HasForeignKey(e => e.CaseID);

            modelBuilder.Entity<HelpCase>()
                .HasMany(e => e.UserCases)
                .WithOptional(e => e.HelpCase)
                .HasForeignKey(e => e.CaseID);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.Organization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Organization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.HelpCases)
                .WithOptional(e => e.Organization)
                .HasForeignKey(e => e.OrgID);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.OrganizationCategories)
                .WithOptional(e => e.Organization)
                .HasForeignKey(e => e.OrgID);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.UserOrgs)
                .WithOptional(e => e.Organization)
                .HasForeignKey(e => e.OrgID);

            modelBuilder.Entity<OurProgram>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.OurProgram)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OurProgram>()
                .HasMany(e => e.HelpCases)
                .WithRequired(e => e.OurProgram)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SeasonalProject>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.SeasonalProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatThreads)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatThreadMessages)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AdminID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.HelpCases)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserCases)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserCategories)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserOrgs)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();
        }

        object placeHolderVariable;

        //public System.Data.Entity.DbSet<DaleelElkheir.Admin.Models.ProductCategories.ProductCategoriesModel> ProductCategoriesModels { get; set; }
        //public System.Data.Entity.DbSet<DaleelElkheir.Admin.Models.Product.ProductModel> ProductModels { get; set; }
        //public System.Data.Entity.DbSet<DaleelElkheir.Admin.Models.Guide.GuideModel> GuideModels { get; set; }
        //public System.Data.Entity.DbSet<DaleelElkheir.Admin.Models.Guide.GuideModel> GuideModels { get; set; }
        //public System.Data.Entity.DbSet<DaleelElkheir.API.Models.CSRs.CSRCompanyModel> CSRCompanyModels { get; set; }
    }
}
