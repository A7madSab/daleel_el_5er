namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.About",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BriefEn = c.String(),
                        BriefAr = c.String(),
                        VisionEn = c.String(),
                        VisionAr = c.String(),
                        Mobile = c.String(maxLength: 20),
                        ContactNumber = c.String(maxLength: 20),
                        EmergencyNumber = c.String(maxLength: 20),
                        FacebookCount = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        WebSite = c.String(maxLength: 100),
                        BloodBankHelpsAcount = c.Int(),
                        Message = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Target = c.String(maxLength: 700),
                        Price = c.Decimal(precision: 18, scale: 3),
                        SeasonalProjectID = c.Int(nullable: false),
                        Region = c.String(maxLength: 400),
                        OrganizationID = c.Int(nullable: false),
                        JoinStatus = c.Int(nullable: false),
                        Approval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organization", t => t.OrganizationID)
                .ForeignKey("dbo.SeasonalProject", t => t.SeasonalProjectID)
                .Index(t => t.SeasonalProjectID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.EventForActivity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TitleEn = c.String(nullable: false, maxLength: 200),
                        TitleAr = c.String(nullable: false, maxLength: 200),
                        DescriptionEn = c.String(maxLength: 300),
                        DescriptionAr = c.String(maxLength: 300),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        ActivityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Activity", t => t.ActivityID)
                .Index(t => t.ActivityID);
            
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                        LogoFileID = c.Int(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                        AddressEn = c.String(maxLength: 100),
                        AddressAr = c.String(maxLength: 100),
                        CityID = c.Int(),
                        AreaID = c.Int(),
                        Latitude = c.String(maxLength: 200),
                        Longitude = c.String(maxLength: 200),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileData", t => t.LogoFileID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.Area", t => t.AreaID)
                .Index(t => t.LogoFileID)
                .Index(t => t.CityID)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                        CityID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                        GovernorateID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Governorate", t => t.GovernorateID)
                .Index(t => t.GovernorateID);
            
            CreateTable(
                "dbo.BloodBank",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                        TitleEn = c.String(maxLength: 100),
                        TitleAr = c.String(maxLength: 100),
                        CityID = c.Int(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.BloodBankContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BloodBankID = c.Int(),
                        ContactName = c.String(maxLength: 50),
                        ContactNumber = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BloodBank", t => t.BloodBankID)
                .Index(t => t.BloodBankID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TitleEn = c.String(maxLength: 100),
                        TitleAr = c.String(maxLength: 100),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        AddressEn = c.String(maxLength: 100),
                        AddressAr = c.String(maxLength: 100),
                        Link = c.String(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                        CreationDate = c.DateTime(storeType: "date"),
                        ImageID = c.Int(),
                        UserID = c.Int(nullable: false),
                        OurProgramID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        Mobile = c.String(maxLength: 50),
                        ConfirmationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CaseConfirmation", t => t.ConfirmationID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.FileData", t => t.ImageID)
                .ForeignKey("dbo.OurProgram", t => t.OurProgramID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.Organization", t => t.OrganizationID)
                .Index(t => t.ImageID)
                .Index(t => t.UserID)
                .Index(t => t.OurProgramID)
                .Index(t => t.OrganizationID)
                .Index(t => t.CategoryID)
                .Index(t => t.CityID)
                .Index(t => t.ConfirmationID);
            
            CreateTable(
                "dbo.CaseConfirmation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HelpCase",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                        CaseCode = c.String(maxLength: 50),
                        ContactNumber = c.String(maxLength: 100),
                        ImageFileID = c.Int(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                        DueDate = c.DateTime(storeType: "date"),
                        OrgID = c.Int(),
                        CityID = c.Int(),
                        CategoryID = c.Int(),
                        CaseTypeID = c.Int(),
                        CaseStatusID = c.Int(),
                        RequiredAmount = c.Double(),
                        CurrentAmount = c.Double(),
                        CreateDate = c.DateTime(),
                        UserID = c.Int(nullable: false),
                        OurProgramID = c.Int(nullable: false),
                        SharedID = c.Guid(),
                        ConfirmationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CaseStatus", t => t.CaseStatusID)
                .ForeignKey("dbo.CaseType", t => t.CaseTypeID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.FileData", t => t.ImageFileID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.OurProgram", t => t.OurProgramID)
                .ForeignKey("dbo.CaseConfirmation", t => t.ConfirmationID)
                .ForeignKey("dbo.Organization", t => t.OrgID)
                .Index(t => t.ImageFileID)
                .Index(t => t.OrgID)
                .Index(t => t.CityID)
                .Index(t => t.CategoryID)
                .Index(t => t.CaseTypeID)
                .Index(t => t.CaseStatusID)
                .Index(t => t.UserID)
                .Index(t => t.OurProgramID)
                .Index(t => t.ConfirmationID);
            
            CreateTable(
                "dbo.CaseStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CaseType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameAr = c.String(maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrganizationCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrgID = c.Int(),
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.Organization", t => t.OrgID)
                .Index(t => t.OrgID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.UserCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100),
                        Name = c.String(maxLength: 200),
                        Mobile = c.String(maxLength: 20),
                        Address = c.String(maxLength: 200),
                        UserTypeID = c.Int(),
                        ImageID = c.Int(),
                        Description = c.String(maxLength: 200),
                        Facebook_ID = c.String(maxLength: 200),
                        Google_ID = c.String(maxLength: 200),
                        VerifyCode = c.String(maxLength: 50),
                        OrganizationID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileData", t => t.ImageID)
                .ForeignKey("dbo.Organization", t => t.OrganizationID)
                .ForeignKey("dbo.UserType", t => t.UserTypeID)
                .Index(t => t.UserTypeID)
                .Index(t => t.ImageID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.ChatThreadMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ThreadID = c.Int(nullable: false),
                        AdminID = c.Int(),
                        Message = c.String(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        Seen = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChatThread", t => t.ThreadID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.AdminID)
                .Index(t => t.ThreadID)
                .Index(t => t.AdminID);
            
            CreateTable(
                "dbo.ChatThread",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CaseID = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.HelpCase", t => t.CaseID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.FileData",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Extenstion = c.String(maxLength: 300),
                        FileBinary = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanySocialResponsibility",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameAr = c.String(maxLength: 200),
                        NameEn = c.String(maxLength: 200),
                        FileID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileData", t => t.FileID)
                .Index(t => t.FileID);
            
            CreateTable(
                "dbo.CSRActivity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TitleEn = c.String(nullable: false, maxLength: 200),
                        TitleAr = c.String(nullable: false, maxLength: 200),
                        DescriptionEn = c.String(maxLength: 400),
                        DescriptionAr = c.String(maxLength: 400),
                        CSR_ID = c.Int(nullable: false),
                        ActivityDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CompanySocialResponsibility", t => t.CSR_ID)
                .Index(t => t.CSR_ID);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TitleEn = c.String(nullable: false, maxLength: 200),
                        TitleAr = c.String(nullable: false, maxLength: 200),
                        NewsDate = c.DateTime(storeType: "date"),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                        ImageID = c.Int(),
                        VideoLink = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileData", t => t.ImageID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Sponsor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 200),
                        NameAr = c.String(maxLength: 200),
                        Link = c.String(maxLength: 50),
                        ImageID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileData", t => t.ImageID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.TrusteesBoard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameAr = c.String(nullable: false, maxLength: 200),
                        NameEn = c.String(nullable: false, maxLength: 200),
                        TitleAr = c.String(maxLength: 300),
                        TitleEn = c.String(maxLength: 300),
                        ImageID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileData", t => t.ImageID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.UserCase",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        CaseID = c.Int(),
                        CreationDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.HelpCase", t => t.CaseID)
                .Index(t => t.UserID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.UserDevice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        DeviceToken = c.String(),
                        SecurityToken = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserOrg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        OrgID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Organization", t => t.OrgID)
                .Index(t => t.UserID)
                .Index(t => t.OrgID);
            
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VolunteerCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VolunteerID = c.Int(),
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.volunteer", t => t.VolunteerID)
                .Index(t => t.VolunteerID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.volunteer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Email = c.String(maxLength: 50),
                        Contact = c.String(maxLength: 50),
                        Job = c.String(maxLength: 500),
                        Nationality = c.String(maxLength: 500),
                        DaysAvailable = c.String(maxLength: 500),
                        AboutQuestion = c.String(maxLength: 500),
                        VoulunteeringMethod = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OurProgram",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TitleEn = c.String(nullable: false, maxLength: 200),
                        TitleAr = c.String(nullable: false, maxLength: 200),
                        DescriptionEn = c.String(maxLength: 300),
                        DescriptionAr = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventGallery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(nullable: false),
                        Ext = c.String(maxLength: 400),
                        Name = c.String(maxLength: 200),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Governorate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SeasonalProject",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(nullable: false, maxLength: 200),
                        NameAr = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeviceToken",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceTokenKey = c.String(nullable: false, maxLength: 500),
                        UserKey = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HospitalContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HospitalID = c.Int(),
                        ContactName = c.String(maxLength: 50),
                        ContactNumber = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hospital", t => t.HospitalID)
                .Index(t => t.HospitalID);
            
            CreateTable(
                "dbo.Hospital",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameEn = c.String(maxLength: 100),
                        NameAr = c.String(maxLength: 100),
                        TitleEn = c.String(maxLength: 100),
                        TitleAr = c.String(maxLength: 100),
                        CityID = c.Int(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        Title = c.String(),
                        TitleAr = c.String(),
                        Body = c.String(),
                        BodyAr = c.String(),
                        FromUserID = c.Int(),
                        Date = c.DateTime(),
                        Type = c.Int(),
                        Seen = c.Boolean(),
                        TransID = c.Int(),
                        TransType = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NotificationType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Program",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 500),
                        Descr = c.String(),
                        ImageFileID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HospitalContact", "HospitalID", "dbo.Hospital");
            DropForeignKey("dbo.Hospital", "CityID", "dbo.City");
            DropForeignKey("dbo.Activity", "SeasonalProjectID", "dbo.SeasonalProject");
            DropForeignKey("dbo.UserOrg", "OrgID", "dbo.Organization");
            DropForeignKey("dbo.OrganizationCategory", "OrgID", "dbo.Organization");
            DropForeignKey("dbo.HelpCase", "OrgID", "dbo.Organization");
            DropForeignKey("dbo.Event", "OrganizationID", "dbo.Organization");
            DropForeignKey("dbo.Organization", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Organization", "CityID", "dbo.City");
            DropForeignKey("dbo.City", "GovernorateID", "dbo.Governorate");
            DropForeignKey("dbo.Event", "CityID", "dbo.City");
            DropForeignKey("dbo.EventGallery", "EventID", "dbo.Event");
            DropForeignKey("dbo.HelpCase", "ConfirmationID", "dbo.CaseConfirmation");
            DropForeignKey("dbo.UserCase", "CaseID", "dbo.HelpCase");
            DropForeignKey("dbo.HelpCase", "OurProgramID", "dbo.OurProgram");
            DropForeignKey("dbo.Event", "OurProgramID", "dbo.OurProgram");
            DropForeignKey("dbo.HelpCase", "CityID", "dbo.City");
            DropForeignKey("dbo.ChatThread", "CaseID", "dbo.HelpCase");
            DropForeignKey("dbo.VolunteerCategory", "VolunteerID", "dbo.volunteer");
            DropForeignKey("dbo.VolunteerCategory", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.User", "UserTypeID", "dbo.UserType");
            DropForeignKey("dbo.UserOrg", "UserID", "dbo.User");
            DropForeignKey("dbo.UserDevice", "UserID", "dbo.User");
            DropForeignKey("dbo.UserCategories", "UserID", "dbo.User");
            DropForeignKey("dbo.UserCase", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "OrganizationID", "dbo.Organization");
            DropForeignKey("dbo.HelpCase", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "ImageID", "dbo.FileData");
            DropForeignKey("dbo.TrusteesBoard", "ImageID", "dbo.FileData");
            DropForeignKey("dbo.Sponsor", "ImageID", "dbo.FileData");
            DropForeignKey("dbo.Organization", "LogoFileID", "dbo.FileData");
            DropForeignKey("dbo.Information", "ImageID", "dbo.FileData");
            DropForeignKey("dbo.HelpCase", "ImageFileID", "dbo.FileData");
            DropForeignKey("dbo.Event", "ImageID", "dbo.FileData");
            DropForeignKey("dbo.CompanySocialResponsibility", "FileID", "dbo.FileData");
            DropForeignKey("dbo.CSRActivity", "CSR_ID", "dbo.CompanySocialResponsibility");
            DropForeignKey("dbo.Event", "UserID", "dbo.User");
            DropForeignKey("dbo.ChatThread", "UserID", "dbo.User");
            DropForeignKey("dbo.ChatThreadMessage", "AdminID", "dbo.User");
            DropForeignKey("dbo.ChatThreadMessage", "ThreadID", "dbo.ChatThread");
            DropForeignKey("dbo.UserCategories", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.OrganizationCategory", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.HelpCase", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Event", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.HelpCase", "CaseTypeID", "dbo.CaseType");
            DropForeignKey("dbo.HelpCase", "CaseStatusID", "dbo.CaseStatus");
            DropForeignKey("dbo.Event", "ConfirmationID", "dbo.CaseConfirmation");
            DropForeignKey("dbo.BloodBank", "CityID", "dbo.City");
            DropForeignKey("dbo.BloodBankContact", "BloodBankID", "dbo.BloodBank");
            DropForeignKey("dbo.Area", "CityID", "dbo.City");
            DropForeignKey("dbo.Activity", "OrganizationID", "dbo.Organization");
            DropForeignKey("dbo.EventForActivity", "ActivityID", "dbo.Activity");
            DropIndex("dbo.Hospital", new[] { "CityID" });
            DropIndex("dbo.HospitalContact", new[] { "HospitalID" });
            DropIndex("dbo.EventGallery", new[] { "EventID" });
            DropIndex("dbo.VolunteerCategory", new[] { "CategoryID" });
            DropIndex("dbo.VolunteerCategory", new[] { "VolunteerID" });
            DropIndex("dbo.UserOrg", new[] { "OrgID" });
            DropIndex("dbo.UserOrg", new[] { "UserID" });
            DropIndex("dbo.UserDevice", new[] { "UserID" });
            DropIndex("dbo.UserCase", new[] { "CaseID" });
            DropIndex("dbo.UserCase", new[] { "UserID" });
            DropIndex("dbo.TrusteesBoard", new[] { "ImageID" });
            DropIndex("dbo.Sponsor", new[] { "ImageID" });
            DropIndex("dbo.Information", new[] { "ImageID" });
            DropIndex("dbo.CSRActivity", new[] { "CSR_ID" });
            DropIndex("dbo.CompanySocialResponsibility", new[] { "FileID" });
            DropIndex("dbo.ChatThread", new[] { "CaseID" });
            DropIndex("dbo.ChatThread", new[] { "UserID" });
            DropIndex("dbo.ChatThreadMessage", new[] { "AdminID" });
            DropIndex("dbo.ChatThreadMessage", new[] { "ThreadID" });
            DropIndex("dbo.User", new[] { "OrganizationID" });
            DropIndex("dbo.User", new[] { "ImageID" });
            DropIndex("dbo.User", new[] { "UserTypeID" });
            DropIndex("dbo.UserCategories", new[] { "CategoryID" });
            DropIndex("dbo.UserCategories", new[] { "UserID" });
            DropIndex("dbo.OrganizationCategory", new[] { "CategoryID" });
            DropIndex("dbo.OrganizationCategory", new[] { "OrgID" });
            DropIndex("dbo.HelpCase", new[] { "ConfirmationID" });
            DropIndex("dbo.HelpCase", new[] { "OurProgramID" });
            DropIndex("dbo.HelpCase", new[] { "UserID" });
            DropIndex("dbo.HelpCase", new[] { "CaseStatusID" });
            DropIndex("dbo.HelpCase", new[] { "CaseTypeID" });
            DropIndex("dbo.HelpCase", new[] { "CategoryID" });
            DropIndex("dbo.HelpCase", new[] { "CityID" });
            DropIndex("dbo.HelpCase", new[] { "OrgID" });
            DropIndex("dbo.HelpCase", new[] { "ImageFileID" });
            DropIndex("dbo.Event", new[] { "ConfirmationID" });
            DropIndex("dbo.Event", new[] { "CityID" });
            DropIndex("dbo.Event", new[] { "CategoryID" });
            DropIndex("dbo.Event", new[] { "OrganizationID" });
            DropIndex("dbo.Event", new[] { "OurProgramID" });
            DropIndex("dbo.Event", new[] { "UserID" });
            DropIndex("dbo.Event", new[] { "ImageID" });
            DropIndex("dbo.BloodBankContact", new[] { "BloodBankID" });
            DropIndex("dbo.BloodBank", new[] { "CityID" });
            DropIndex("dbo.City", new[] { "GovernorateID" });
            DropIndex("dbo.Area", new[] { "CityID" });
            DropIndex("dbo.Organization", new[] { "AreaID" });
            DropIndex("dbo.Organization", new[] { "CityID" });
            DropIndex("dbo.Organization", new[] { "LogoFileID" });
            DropIndex("dbo.EventForActivity", new[] { "ActivityID" });
            DropIndex("dbo.Activity", new[] { "OrganizationID" });
            DropIndex("dbo.Activity", new[] { "SeasonalProjectID" });
            DropTable("dbo.Program");
            DropTable("dbo.NotificationType");
            DropTable("dbo.Notification");
            DropTable("dbo.Hospital");
            DropTable("dbo.HospitalContact");
            DropTable("dbo.DeviceToken");
            DropTable("dbo.SeasonalProject");
            DropTable("dbo.Governorate");
            DropTable("dbo.EventGallery");
            DropTable("dbo.OurProgram");
            DropTable("dbo.volunteer");
            DropTable("dbo.VolunteerCategory");
            DropTable("dbo.UserType");
            DropTable("dbo.UserOrg");
            DropTable("dbo.UserDevice");
            DropTable("dbo.UserCase");
            DropTable("dbo.TrusteesBoard");
            DropTable("dbo.Sponsor");
            DropTable("dbo.Information");
            DropTable("dbo.CSRActivity");
            DropTable("dbo.CompanySocialResponsibility");
            DropTable("dbo.FileData");
            DropTable("dbo.ChatThread");
            DropTable("dbo.ChatThreadMessage");
            DropTable("dbo.User");
            DropTable("dbo.UserCategories");
            DropTable("dbo.OrganizationCategory");
            DropTable("dbo.Category");
            DropTable("dbo.CaseType");
            DropTable("dbo.CaseStatus");
            DropTable("dbo.HelpCase");
            DropTable("dbo.CaseConfirmation");
            DropTable("dbo.Event");
            DropTable("dbo.BloodBankContact");
            DropTable("dbo.BloodBank");
            DropTable("dbo.City");
            DropTable("dbo.Area");
            DropTable("dbo.Organization");
            DropTable("dbo.EventForActivity");
            DropTable("dbo.Activity");
            DropTable("dbo.About");
        }
    }
}
