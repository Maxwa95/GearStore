namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetwoii : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ss");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ss", c => c.String());
        }
    }
}
