namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtcategorytbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Product", "CategoryID", c => c.Int());
            CreateIndex("dbo.Product", "CategoryID");
            AddForeignKey("dbo.Product", "CategoryID", "dbo.ProductCategory", "ID");
            DropColumn("dbo.Product", "ProductType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductType", c => c.String());
            DropForeignKey("dbo.Product", "CategoryID", "dbo.ProductCategory");
            DropIndex("dbo.Product", new[] { "CategoryID" });
            DropColumn("dbo.Product", "CategoryID");
            DropTable("dbo.ProductCategory");
        }
    }
}
