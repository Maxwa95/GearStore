namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2 : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Model_ModelId", "dbo.Models");
            DropIndex("dbo.Products", new[] { "Model_ModelId" });
            AlterColumn("dbo.Products", "Model_ModelId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Products", name: "Model_ModelId", newName: "Modelid");
            CreateIndex("dbo.Products", "Modelid");
            AddForeignKey("dbo.Products", "Modelid", "dbo.Models", "ModelId", cascadeDelete: true);
        }
    }
}
