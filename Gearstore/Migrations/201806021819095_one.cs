namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false, maxLength: 255),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false, maxLength: 255),
                        BrandId = c.Int(),
                    })
                .PrimaryKey(t => t.ModelId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Categories_Model",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoriesId = c.Int(),
                        ModelId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoriesId)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .Index(t => t.CategoriesId)
                .Index(t => t.ModelId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoriesId = c.Int(nullable: false, identity: true),
                        CategoriesName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoriesId);
            
            CreateTable(
                "dbo.modelsproducts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        modelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Models", t => t.modelId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.modelId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 255),
                        PlaceOfOrigin = c.String(nullable: false, maxLength: 45),
                        DateOfPublish = c.DateTime(nullable: false),
                        Price = c.Single(nullable: false),
                        Discount = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.productId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        HomePhone = c.String(nullable: false),
                        view = c.Int(),
                        Description = c.String(nullable: false, maxLength: 255),
                        Rate = c.Single(nullable: false),
                        userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        usertype = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        city = c.String(nullable: false),
                        profileImage = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        FeedBackId = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 255),
                        Userid = c.String(nullable: false, maxLength: 128),
                        Productid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedBackId)
                .ForeignKey("dbo.Products", t => t.Productid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid, cascadeDelete: true)
                .Index(t => t.Userid)
                .Index(t => t.Productid);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImgUrl = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SimilaritiesProducts",
                c => new
                    {
                        SimilaritiesProductsId = c.Int(nullable: false, identity: true),
                        NeededProductsId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SimilaritiesProductsId)
                .ForeignKey("dbo.NeededProducts", t => t.NeededProductsId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.NeededProductsId)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.NeededProducts",
                c => new
                    {
                        NeededProductsId = c.Int(nullable: false, identity: true),
                        ImagePath = c.Int(nullable: false),
                        FullName = c.String(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        StatuseResponce = c.String(nullable: false),
                        TextResponce = c.String(),
                    })
                .PrimaryKey(t => t.NeededProductsId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        currentprice = c.Single(nullable: false),
                        OrderId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailsId)
                .ForeignKey("dbo.OrderInfoes", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.OrderInfoes",
                c => new
                    {
                        OrderInfoId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        TotalCost = c.String(nullable: false),
                        SelectedAdd = c.String(nullable: false),
                        OrderStatus = c.String(nullable: false),
                        userid = c.String(maxLength: 128),
                        ShippingCompanyid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderInfoId)
                .ForeignKey("dbo.ShippingCompanies", t => t.ShippingCompanyid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.userid)
                .Index(t => t.ShippingCompanyid);
            
            CreateTable(
                "dbo.ShippingCompanies",
                c => new
                    {
                        ShippingCompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        MaxDeliveryDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShippingCompanyId);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        DescriptionId = c.Int(nullable: false, identity: true),
                        ProdId = c.Int(nullable: false),
                        Length = c.Single(nullable: false),
                        Size = c.Single(nullable: false),
                        PartNumber = c.String(nullable: false),
                        ModelFlexibility = c.String(nullable: false),
                        MoreDetails = c.String(nullable: false),
                        YearOfProduct = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DescriptionId)
                .ForeignKey("dbo.Products", t => t.ProdId, cascadeDelete: true)
                .Index(t => t.ProdId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Descriptions", "ProdId", "dbo.Products");
            DropForeignKey("dbo.modelsproducts", "productId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "productId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.OrderInfoes");
            DropForeignKey("dbo.OrderInfoes", "userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderInfoes", "ShippingCompanyid", "dbo.ShippingCompanies");
            DropForeignKey("dbo.SimilaritiesProducts", "productId", "dbo.Products");
            DropForeignKey("dbo.SimilaritiesProducts", "NeededProductsId", "dbo.NeededProducts");
            DropForeignKey("dbo.Images", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedBacks", "Userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedBacks", "Productid", "dbo.Products");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.modelsproducts", "modelId", "dbo.Models");
            DropForeignKey("dbo.Categories_Model", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Categories_Model", "CategoriesId", "dbo.Categories");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Descriptions", new[] { "ProdId" });
            DropIndex("dbo.OrderInfoes", new[] { "ShippingCompanyid" });
            DropIndex("dbo.OrderInfoes", new[] { "userid" });
            DropIndex("dbo.OrderDetails", new[] { "productId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.SimilaritiesProducts", new[] { "productId" });
            DropIndex("dbo.SimilaritiesProducts", new[] { "NeededProductsId" });
            DropIndex("dbo.Images", new[] { "ProductId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.FeedBacks", new[] { "Productid" });
            DropIndex("dbo.FeedBacks", new[] { "Userid" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Companies", new[] { "userid" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropIndex("dbo.modelsproducts", new[] { "modelId" });
            DropIndex("dbo.modelsproducts", new[] { "productId" });
            DropIndex("dbo.Categories_Model", new[] { "ModelId" });
            DropIndex("dbo.Categories_Model", new[] { "CategoriesId" });
            DropIndex("dbo.Models", new[] { "BrandId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Descriptions");
            DropTable("dbo.ShippingCompanies");
            DropTable("dbo.OrderInfoes");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.NeededProducts");
            DropTable("dbo.SimilaritiesProducts");
            DropTable("dbo.Images");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Companies");
            DropTable("dbo.Products");
            DropTable("dbo.modelsproducts");
            DropTable("dbo.Categories");
            DropTable("dbo.Categories_Model");
            DropTable("dbo.Models");
            DropTable("dbo.Brands");
        }
    }
}
