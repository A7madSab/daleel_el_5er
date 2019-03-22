namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProgramDescriptionProductcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProgramDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProgramDescription");
        }
    }
}
