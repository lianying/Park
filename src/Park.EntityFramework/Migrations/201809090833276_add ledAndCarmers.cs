namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addledAndCarmers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ip = c.String(),
                        Port = c.Long(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        EquipmentManufacturers = c.Int(nullable: false),
                        EntranceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkEntrances", t => t.EntranceId, cascadeDelete: false)
                .Index(t => t.EntranceId);
            
            CreateTable(
                "dbo.Leds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ip = c.String(),
                        Port = c.Long(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        EquipmentManufacturers = c.Int(nullable: false),
                        EntranceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkEntrances", t => t.EntranceId, cascadeDelete: false)
                .Index(t => t.EntranceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leds", "EntranceId", "dbo.ParkEntrances");
            DropForeignKey("dbo.Cameras", "EntranceId", "dbo.ParkEntrances");
            DropIndex("dbo.Leds", new[] { "EntranceId" });
            DropIndex("dbo.Cameras", new[] { "EntranceId" });
            DropTable("dbo.Leds");
            DropTable("dbo.Cameras");
        }
    }
}
