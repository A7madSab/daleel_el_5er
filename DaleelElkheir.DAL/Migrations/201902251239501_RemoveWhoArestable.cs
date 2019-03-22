namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveWhoArestable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.WhoAre");
        }
        
        public override void Down()
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
    }
}
