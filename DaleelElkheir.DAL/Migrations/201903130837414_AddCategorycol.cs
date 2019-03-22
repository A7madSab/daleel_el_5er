namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategorycol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanySocialResponsibility", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanySocialResponsibility", "Category");
        }
    }
}
