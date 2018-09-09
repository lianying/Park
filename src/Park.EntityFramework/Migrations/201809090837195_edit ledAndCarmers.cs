namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editledAndCarmers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cameras", "IsSuccess", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cameras", "CloudId", c => c.String());
            AddColumn("dbo.Leds", "IsSuccess", c => c.Boolean(nullable: false));
            AddColumn("dbo.Leds", "CloudId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leds", "CloudId");
            DropColumn("dbo.Leds", "IsSuccess");
            DropColumn("dbo.Cameras", "CloudId");
            DropColumn("dbo.Cameras", "IsSuccess");
        }
    }
}
