namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPogramDescHelpCaseTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HelpCase", "DescriptionProgram", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HelpCase", "DescriptionProgram");
        }
    }
}
