namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventProgamDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "DescriptionProgram", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "DescriptionProgram");
        }
    }
}
