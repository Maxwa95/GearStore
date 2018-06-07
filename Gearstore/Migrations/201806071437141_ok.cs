namespace gearproj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ok : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "Description", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
