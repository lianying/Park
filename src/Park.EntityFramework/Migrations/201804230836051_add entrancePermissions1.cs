namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addentrancePermissions1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CarTypes", new[] { "ParkEntrancePermission_Id" });
            RenameColumn(table: "dbo.ParkEntrancePermissions", name: "ParkEntrancePermission_Id", newName: "CarTypes_Id");
            DropPrimaryKey("dbo.CarTypes");
            CreateTable(
                "dbo.CarTypePermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        PermissionId = c.Long(nullable: false),
                        CarTypeId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarTypePermissions_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.CarTypes", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.CarTypes", "Id");
            CreateIndex("dbo.ParkEntrancePermissions", "CarTypes_Id");
            DropColumn("dbo.CarTypes", "ParkEntrancePermission_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarTypes", "ParkEntrancePermission_Id", c => c.Long());
            DropIndex("dbo.ParkEntrancePermissions", new[] { "CarTypes_Id" });
            DropPrimaryKey("dbo.CarTypes");
            AlterColumn("dbo.CarTypes", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.CarTypePermissions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarTypePermissions_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            AddPrimaryKey("dbo.CarTypes", "Id");
            RenameColumn(table: "dbo.ParkEntrancePermissions", name: "CarTypes_Id", newName: "ParkEntrancePermission_Id");
            CreateIndex("dbo.CarTypes", "ParkEntrancePermission_Id");
        }
    }
}
