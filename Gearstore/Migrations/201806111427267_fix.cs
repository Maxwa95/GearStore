namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Descriptions", "Length", c => c.Single());
            AlterColumn("dbo.Descriptions", "Size", c => c.Single());
            AlterColumn("dbo.Descriptions", "YearOfProduct", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Descriptions", "YearOfProduct", c => c.Int(nullable: false));
            AlterColumn("dbo.Descriptions", "Size", c => c.Single(nullable: false));
            AlterColumn("dbo.Descriptions", "Length", c => c.Single(nullable: false));
        }
    }
}
