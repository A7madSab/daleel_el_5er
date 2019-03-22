namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuideNamecol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guide", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guide", "Name");
        }
    }
}
