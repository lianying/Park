namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class dropdevice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Devices", "EntranceId", "dbo.ParkEntrances");
            DropIndex("dbo.Devices", new[] { "EntranceId" });
            DropTable("dbo.Devices",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Device_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Ip = c.String(),
                        Port = c.Long(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        EquipmentManufacturers = c.Int(nullable: false),
                        EntranceId = c.Long(),
                        TenantId = c.Int(),
                        DeviceType = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Device_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Devices", "EntranceId");
            AddForeignKey("dbo.Devices", "EntranceId", "dbo.ParkEntrances", "Id");
        }
    }
}
