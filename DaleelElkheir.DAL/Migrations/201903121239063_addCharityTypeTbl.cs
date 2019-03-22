namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCharityTypeTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharityType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CharityName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CharityType");
        }
    }
}
