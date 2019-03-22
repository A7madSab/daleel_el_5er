namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileNameColDuide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guide", "FileName", c => c.String());
            DropColumn("dbo.Guide", "FileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guide", "FileID", c => c.Int());
            DropColumn("dbo.Guide", "FileName");
        }
    }
}
