namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparkcarportpark : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarPorts", "CarUserId", "dbo.CarUsers");
            DropIndex("dbo.CarPorts", new[] { "CarUserId" });
            AddColumn("dbo.CarPorts", "ParkId", c => c.Int());
            AlterColumn("dbo.CarPorts", "CarUserId", c => c.Long());
            CreateIndex("dbo.CarPorts", "ParkId");
            CreateIndex("dbo.CarPorts", "CarUserId");
            AddForeignKey("dbo.CarPorts", "ParkId", "dbo.Park", "Id");
            AddForeignKey("dbo.CarPorts", "CarUserId", "dbo.CarUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarPorts", "CarUserId", "dbo.CarUsers");
            DropForeignKey("dbo.CarPorts", "ParkId", "dbo.Park");
            DropIndex("dbo.CarPorts", new[] { "CarUserId" });
            DropIndex("dbo.CarPorts", new[] { "ParkId" });
            AlterColumn("dbo.CarPorts", "CarUserId", c => c.Long(nullable: false));
            DropColumn("dbo.CarPorts", "ParkId");
            CreateIndex("dbo.CarPorts", "CarUserId");
            AddForeignKey("dbo.CarPorts", "CarUserId", "dbo.CarUsers", "Id", cascadeDelete: true);
        }
    }
}
