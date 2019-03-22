namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProgramDescriptionOrganizationTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "DescriptionProject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organization", "DescriptionProject");
        }
    }
}
