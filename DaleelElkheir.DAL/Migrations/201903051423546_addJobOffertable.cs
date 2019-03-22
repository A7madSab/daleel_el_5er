namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addJobOffertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobOffer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        Employer = c.String(),
                        DescritpionAr = c.String(),
                        DescritpionEn = c.String(),
                        ContactInfo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobOffer");
        }
    }
}
