namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addcarUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarNumbers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarNumber = c.String(),
                        CarUserId = c.Long(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarNumbers_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarUsers", t => t.CarUserId, cascadeDelete: true)
                .Index(t => t.CarUserId);
            
            CreateTable(
                "dbo.CarUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Sex = c.Int(nullable: false),
                        Phone = c.String(),
                        ParkId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ParkArea_Id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarUsers_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Park", t => t.ParkId, cascadeDelete: true)
                .ForeignKey("dbo.ParkAreas", t => t.ParkArea_Id)
                .Index(t => t.ParkId)
                .Index(t => t.ParkArea_Id);
            
            CreateTable(
                "dbo.CarPorts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarportSerial = c.String(),
                        AreaId = c.Long(nullable: false),
                        LevelId = c.Long(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EntTime = c.DateTime(nullable: false),
                        IsRent = c.Boolean(nullable: false),
                        CarPortTypeId = c.Long(nullable: false),
                        IsSharing = c.Boolean(nullable: false),
                        HasChargingPile = c.Boolean(nullable: false),
                        IsRealCarports = c.Boolean(nullable: false),
                        CarUserId = c.Long(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarPort_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarTypes", t => t.CarPortTypeId, cascadeDelete: false)
                .ForeignKey("dbo.CarUsers", t => t.CarUserId, cascadeDelete: false)
                .ForeignKey("dbo.ParkAreas", t => t.AreaId, cascadeDelete: false)
                .ForeignKey("dbo.ParkLevels", t => t.LevelId, cascadeDelete: false)
                .Index(t => t.AreaId)
                .Index(t => t.LevelId)
                .Index(t => t.CarPortTypeId)
                .Index(t => t.CarUserId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkEntrances", t => t.EntranceId)
                .Index(t => t.EntranceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "EntranceId", "dbo.ParkEntrances");
            DropForeignKey("dbo.CarPorts", "LevelId", "dbo.ParkLevels");
            DropForeignKey("dbo.CarPorts", "AreaId", "dbo.ParkAreas");
            DropForeignKey("dbo.CarPorts", "CarUserId", "dbo.CarUsers");
            DropForeignKey("dbo.CarPorts", "CarPortTypeId", "dbo.CarTypes");
            DropForeignKey("dbo.CarNumbers", "CarUserId", "dbo.CarUsers");
            DropForeignKey("dbo.CarUsers", "ParkArea_Id", "dbo.ParkAreas");
            DropForeignKey("dbo.CarUsers", "ParkId", "dbo.Park");
            DropIndex("dbo.Devices", new[] { "EntranceId" });
            DropIndex("dbo.CarPorts", new[] { "CarUserId" });
            DropIndex("dbo.CarPorts", new[] { "CarPortTypeId" });
            DropIndex("dbo.CarPorts", new[] { "LevelId" });
            DropIndex("dbo.CarPorts", new[] { "AreaId" });
            DropIndex("dbo.CarUsers", new[] { "ParkArea_Id" });
            DropIndex("dbo.CarUsers", new[] { "ParkId" });
            DropIndex("dbo.CarNumbers", new[] { "CarUserId" });
            DropTable("dbo.Devices",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Device_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CarPorts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarPort_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CarUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarUsers_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CarNumbers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarNumbers_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
