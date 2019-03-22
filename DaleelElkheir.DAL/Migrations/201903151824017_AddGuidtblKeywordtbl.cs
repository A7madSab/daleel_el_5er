namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidtblKeywordtbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guide",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        File = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KeyWord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                        GuideID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Guide", t => t.GuideID, cascadeDelete: true)
                .Index(t => t.GuideID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KeyWord", "GuideID", "dbo.Guide");
            DropIndex("dbo.KeyWord", new[] { "GuideID" });
            DropTable("dbo.KeyWord");
            DropTable("dbo.Guide");
        }
    }
}
