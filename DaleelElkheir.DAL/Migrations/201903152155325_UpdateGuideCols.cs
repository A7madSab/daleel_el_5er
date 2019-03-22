namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGuideCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guide", "FileData_ID", c => c.Int());
            AlterColumn("dbo.Guide", "FileID", c => c.Int());
            CreateIndex("dbo.Guide", "FileData_ID");
            AddForeignKey("dbo.Guide", "FileData_ID", "dbo.FileData", "ID");
            DropColumn("dbo.Guide", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guide", "File", c => c.String());
            DropForeignKey("dbo.Guide", "FileData_ID", "dbo.FileData");
            DropIndex("dbo.Guide", new[] { "FileData_ID" });
            AlterColumn("dbo.Guide", "FileID", c => c.Int(nullable: false));
            DropColumn("dbo.Guide", "FileData_ID");
        }
    }
}
