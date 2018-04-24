namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addcarTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CustomName = c.String(),
                        Warring = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.Int(nullable: false),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarTypes_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkAreas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ParkId = c.Int(nullable: false),
                        AreaName = c.String(),
                        parkAreaCarports = c.Int(nullable: false),
                        parkAreaRentableCarports = c.Int(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkAreas_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Park", t => t.ParkId, cascadeDelete: false)
                .Index(t => t.ParkId);
            
            CreateTable(
                "dbo.Park",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsSync = c.Boolean(nullable: false),
                        ParkSoure = c.String(maxLength: 285),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(maxLength: 285),
                        ParentId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 285),
                        Address = c.String(nullable: false),
                        CarportCount = c.Int(nullable: false),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AreaCode = c.String(nullable: false, maxLength: 16),
                        TenantId = c.Int(),
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
                    { "DynamicFilter_JinQuPark_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JinQuPark_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Park", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.ParkEntrancePermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CarTypes = c.String(),
                        IsTempCarIn = c.Boolean(nullable: false),
                        IsTempCarConfirm = c.Boolean(nullable: false),
                        IsTempCarZeoPayOut = c.Boolean(nullable: false),
                        IsNonVehicleIn = c.Boolean(nullable: false),
                        NoNumberOptions = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkEntrancePermission_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkEntrances",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntranceName = c.String(),
                        EntranceType = c.Int(nullable: false),
                        ParkLevelId = c.Long(nullable: false),
                        PermissionId = c.Long(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkEntrances_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkEntrancePermissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.ParkLevels", t => t.ParkLevelId, cascadeDelete: true)
                .Index(t => t.ParkLevelId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.ParkLevels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 285),
                        LevelNumber = c.Int(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(maxLength: 285),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        AreaId = c.Long(nullable: false),
                        ParkId = c.Int(nullable: false),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkLevels_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Park", t => t.ParkId, cascadeDelete: true)
                .ForeignKey("dbo.ParkAreas", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.AreaId)
                .Index(t => t.ParkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkEntrances", "ParkLevelId", "dbo.ParkLevels");
            DropForeignKey("dbo.ParkLevels", "AreaId", "dbo.ParkAreas");
            DropForeignKey("dbo.ParkLevels", "ParkId", "dbo.Park");
            DropForeignKey("dbo.ParkEntrances", "PermissionId", "dbo.ParkEntrancePermissions");
            DropForeignKey("dbo.ParkAreas", "ParkId", "dbo.Park");
            DropForeignKey("dbo.Park", "ParentId", "dbo.Park");
            DropIndex("dbo.ParkLevels", new[] { "ParkId" });
            DropIndex("dbo.ParkLevels", new[] { "AreaId" });
            DropIndex("dbo.ParkEntrances", new[] { "PermissionId" });
            DropIndex("dbo.ParkEntrances", new[] { "ParkLevelId" });
            DropIndex("dbo.Park", new[] { "ParentId" });
            DropIndex("dbo.ParkAreas", new[] { "ParkId" });
            DropTable("dbo.ParkLevels",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkLevels_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ParkEntrances",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkEntrances_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ParkEntrancePermissions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkEntrancePermission_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Park",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JinQuPark_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JinQuPark_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ParkAreas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkAreas_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CarTypes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarTypes_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
