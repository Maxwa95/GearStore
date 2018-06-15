namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
 
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestsId = c.Int(nullable: false, identity: true),
                        NeededId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Received = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RequestsId);
            
            CreateTable(
                "dbo.Userconnections",
                c => new
                    {
                        UserconnectionsId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        Connected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserconnectionsId);
            
            AddColumn("dbo.NeededProducts", "brandid", c => c.Int(nullable: false));
            AddColumn("dbo.NeededProducts", "modelid", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderInfoes", "ShippingCompanyid", "dbo.ShippingCompanies");
            DropIndex("dbo.OrderInfoes", new[] { "ShippingCompanyid" });
            AlterColumn("dbo.NeededProducts", "StatuseResponce", c => c.String(nullable: false));
            AlterColumn("dbo.OrderInfoes", "ShippingCompanyid", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderInfoes", "SelectedAdd", c => c.String(nullable: false));
            CreateIndex("dbo.Requests", "CompanyId");
            CreateIndex("dbo.Requests", "NeededId");
            CreateIndex("dbo.Userconnections", "UserId");
            CreateIndex("dbo.NeededProducts", "brandid");
            CreateIndex("dbo.NeededProducts", "modelid");
            CreateIndex("dbo.OrderInfoes", "ShippingCompanyid");
            AddForeignKey("dbo.OrderInfoes", "ShippingCompanyid", "dbo.ShippingCompanies", "ShippingCompanyId", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "NeededId", "dbo.NeededProducts", "NeededProductsId", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
            AddForeignKey("dbo.Userconnections", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.NeededProducts", "modelid", "dbo.Models", "ModelId", cascadeDelete: true);
            AddForeignKey("dbo.NeededProducts", "brandid", "dbo.Brands", "BrandId", cascadeDelete: true);
        }
    }
}
