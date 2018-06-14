namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ii : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SimilaritiesProducts", "NeededProductsId", "dbo.NeededProducts");
            DropForeignKey("dbo.SimilaritiesProducts", "productId", "dbo.Products");
            DropIndex("dbo.SimilaritiesProducts", new[] { "NeededProductsId" });
            DropIndex("dbo.SimilaritiesProducts", new[] { "productId" });
            AddColumn("dbo.NeededProducts", "userid", c => c.String(maxLength: 128));
            CreateIndex("dbo.NeededProducts", "userid");
            AddForeignKey("dbo.NeededProducts", "userid", "dbo.AspNetUsers", "Id");
            DropTable("dbo.SimilaritiesProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SimilaritiesProducts",
                c => new
                    {
                        SimilaritiesProductsId = c.Int(nullable: false, identity: true),
                        NeededProductsId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SimilaritiesProductsId);
            
            DropForeignKey("dbo.NeededProducts", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.NeededProducts", new[] { "userid" });
            DropColumn("dbo.NeededProducts", "userid");
            CreateIndex("dbo.SimilaritiesProducts", "productId");
            CreateIndex("dbo.SimilaritiesProducts", "NeededProductsId");
            AddForeignKey("dbo.SimilaritiesProducts", "productId", "dbo.Products", "productId", cascadeDelete: true);
            AddForeignKey("dbo.SimilaritiesProducts", "NeededProductsId", "dbo.NeededProducts", "NeededProductsId", cascadeDelete: true);
        }
    }
}
