namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cameras", "Name", c => c.String());
            AddColumn("dbo.Leds", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leds", "Name");
            DropColumn("dbo.Cameras", "Name");
        }
    }
}
