namespace DaleelElkheir.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductSellerTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ProductType = c.String(),
                        FileName = c.String(),
                        Ext = c.String(),
                        SellerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Seller", t => t.SellerID)
                .Index(t => t.SellerID);
            
            CreateTable(
                "dbo.Seller",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contract = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "SellerID", "dbo.Seller");
            DropIndex("dbo.Product", new[] { "SellerID" });
            DropTable("dbo.Seller");
            DropTable("dbo.Product");
        }
    }
}
