namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPorts", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.CarPorts", "EntTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarPorts", "EntTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.CarPorts", "EndTime");
        }
    }
}
