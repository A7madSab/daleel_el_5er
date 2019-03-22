namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOurStoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OurStory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VideoURL = c.String(),
                        BriefEnglish = c.String(),
                        BriefArabic = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OurStory");
        }
    }
}
