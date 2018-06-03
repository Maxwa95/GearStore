namespace gearproj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "state", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "state");
        }
    }
}
