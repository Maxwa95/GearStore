namespace Gearstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_needed : DbMigration
    {
        public override void Up()
        {
          

        }
        
        public override void Down()
        {
           
            DropColumn("dbo.Products", "xo");
        }
    }
}
