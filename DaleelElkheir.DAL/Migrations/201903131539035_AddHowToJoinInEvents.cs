namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHowToJoinInEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "HowToJoin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "HowToJoin");
        }
    }
}
