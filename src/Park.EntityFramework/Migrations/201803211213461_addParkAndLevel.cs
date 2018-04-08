namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addParkAndLevel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Park",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsSync = c.Boolean(nullable: false),
                        ParkSoure = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        ParentId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 285),
                        Address = c.String(nullable: false),
                        CarportCount = c.Int(nullable: false),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AreaCode = c.String(nullable: false),
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
                "dbo.ParkLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 285),
                        LevelNumber = c.Int(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        TenantId = c.Int(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkLevel_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Park", "ParentId", "dbo.Park");
            DropIndex("dbo.Park", new[] { "ParentId" });
            DropTable("dbo.ParkLevels",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkLevel_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Park",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JinQuPark_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JinQuPark_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
