namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addParkAndLevel1 : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.ParkLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 285),
                        LevelNumber = c.Int(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(maxLength: 285),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        ParkId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ParkLevel_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterColumn("dbo.Park", "ParkSoure", c => c.String(maxLength: 285));
            AlterColumn("dbo.Park", "CloudId", c => c.String(maxLength: 285));
            AlterColumn("dbo.Park", "AreaCode", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.ParkLevels", "CloudId", c => c.String(maxLength: 285));
            DropColumn("dbo.ParkLevels", "TenantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkLevels", "TenantId", c => c.Int());
            AlterColumn("dbo.ParkLevels", "CloudId", c => c.String());
            AlterColumn("dbo.Park", "AreaCode", c => c.String(nullable: false));
            AlterColumn("dbo.Park", "CloudId", c => c.String());
            AlterColumn("dbo.Park", "ParkSoure", c => c.String());
            AlterTableAnnotations(
                "dbo.ParkLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 285),
                        LevelNumber = c.Int(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(maxLength: 285),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        ParkId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ParkLevel_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
