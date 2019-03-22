namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWhoAreTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WhoAre",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TextEn = c.String(),
                        TextAr = c.String(),
                        Video = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WhoAre");
        }
    }
}
