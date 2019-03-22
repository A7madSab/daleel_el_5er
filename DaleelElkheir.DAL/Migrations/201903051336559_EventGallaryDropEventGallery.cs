namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventGallaryDropEventGallery : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventGallery", "EventID", c => c.Int(nullable: true));
        }

        public override void Down()
        {
            AlterColumn("dbo.EventGallery", "EventID", c => c.Int(nullable: false));
        }
    }
}
