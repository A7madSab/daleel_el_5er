namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFileIDDuideCol : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guide", "FileData_ID", "dbo.FileData");
            DropIndex("dbo.Guide", new[] { "FileData_ID" });
            DropColumn("dbo.Guide", "FileData_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guide", "FileData_ID", c => c.Int());
            CreateIndex("dbo.Guide", "FileData_ID");
            AddForeignKey("dbo.Guide", "FileData_ID", "dbo.FileData", "ID");
        }
    }
}
