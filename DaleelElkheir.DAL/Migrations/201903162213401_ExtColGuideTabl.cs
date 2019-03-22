namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtColGuideTabl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guide", "Ext", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guide", "Ext");
        }
    }
}
