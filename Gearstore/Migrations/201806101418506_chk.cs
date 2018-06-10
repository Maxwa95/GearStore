namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "xo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "xo");
        }
    }
}
