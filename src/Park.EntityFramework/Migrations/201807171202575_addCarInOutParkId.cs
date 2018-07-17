namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarInOutParkId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarInRecords", "ParkId", c => c.Int(nullable: false));
            AddColumn("dbo.CarOutRecords", "ParkId", c => c.Int(nullable: false));
            CreateIndex("dbo.CarInRecords", "ParkId");
            CreateIndex("dbo.CarOutRecords", "ParkId");
            AddForeignKey("dbo.CarInRecords", "ParkId", "dbo.Park", "Id", cascadeDelete: false);
            AddForeignKey("dbo.CarOutRecords", "ParkId", "dbo.Park", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarOutRecords", "ParkId", "dbo.Park");
            DropForeignKey("dbo.CarInRecords", "ParkId", "dbo.Park");
            DropIndex("dbo.CarOutRecords", new[] { "ParkId" });
            DropIndex("dbo.CarInRecords", new[] { "ParkId" });
            DropColumn("dbo.CarOutRecords", "ParkId");
            DropColumn("dbo.CarInRecords", "ParkId");
        }
    }
}
