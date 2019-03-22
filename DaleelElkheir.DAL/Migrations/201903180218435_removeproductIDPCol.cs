namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeproductIDPCol : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductCategory", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategory", "ProductID", c => c.Int());
        }
    }
}
