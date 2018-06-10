namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twoii : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ss", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ss");
        }
    }
}
