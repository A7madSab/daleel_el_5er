namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileIDGuidetbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guide", "FileID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guide", "FileID");
        }
    }
}
