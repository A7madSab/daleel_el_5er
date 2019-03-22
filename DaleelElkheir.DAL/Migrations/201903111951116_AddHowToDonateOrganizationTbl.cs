namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHowToDonateOrganizationTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "HowToDonate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organization", "HowToDonate");
        }
    }
}
