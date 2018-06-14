namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Userconnections",
                c => new
                    {
                        UserconnectionsId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        Connected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserconnectionsId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestsId = c.Int(nullable: false, identity: true),
                        NeededId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Received = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RequestsId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.NeededProducts", t => t.NeededId, cascadeDelete: true)
                .Index(t => t.NeededId)
                .Index(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "xo", c => c.String());
            DropForeignKey("dbo.Requests", "NeededId", "dbo.NeededProducts");
            DropForeignKey("dbo.Requests", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Userconnections", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "CompanyId" });
            DropIndex("dbo.Requests", new[] { "NeededId" });
            DropIndex("dbo.Userconnections", new[] { "UserId" });
            DropTable("dbo.Requests");
            DropTable("dbo.Userconnections");
        }
    }
}
