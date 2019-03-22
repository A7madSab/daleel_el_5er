namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProgramDescriptionOrganizationTbl1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "DescriptionProgram", c => c.String());
            DropColumn("dbo.Organization", "DescriptionProject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organization", "DescriptionProject", c => c.String());
            DropColumn("dbo.Organization", "DescriptionProgram");
        }
    }
}
