namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixs : DbMigration
    {
        public override void Up()
        {
        
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Model_ModelId", c => c.Int());
            CreateIndex("dbo.Products", "Model_ModelId");
            AddForeignKey("dbo.Products", "Model_ModelId", "dbo.Models", "ModelId");
        }
    }
}
