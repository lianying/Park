namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editparkcarportpark : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarPorts", "ParkId", "dbo.Park");
            DropIndex("dbo.CarPorts", new[] { "ParkId" });
            AlterColumn("dbo.CarPorts", "ParkId", c => c.Int(nullable: false));
            CreateIndex("dbo.CarPorts", "ParkId");
            AddForeignKey("dbo.CarPorts", "ParkId", "dbo.Park", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarPorts", "ParkId", "dbo.Park");
            DropIndex("dbo.CarPorts", new[] { "ParkId" });
            AlterColumn("dbo.CarPorts", "ParkId", c => c.Int());
            CreateIndex("dbo.CarPorts", "ParkId");
            AddForeignKey("dbo.CarPorts", "ParkId", "dbo.Park", "Id");
        }
    }
}
