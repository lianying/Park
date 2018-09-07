namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcarUserGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarUsers", "AreaId", "dbo.ParkAreas");
            DropIndex("dbo.CarUsers", new[] { "AreaId" });
            CreateTable(
                "dbo.CarUserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        AreaId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkAreas", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.AreaId);
            
            AddColumn("dbo.CarUsers", "GroupId", c => c.Int(nullable: true));
            AddColumn("dbo.CarUsers", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.CarUsers", "Remark", c => c.String());
            CreateIndex("dbo.CarUsers", "GroupId");
            AddForeignKey("dbo.CarUsers", "GroupId", "dbo.CarUserGroups", "Id", cascadeDelete: true);
            DropColumn("dbo.CarUsers", "AreaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarUsers", "AreaId", c => c.Long(nullable: false));
            DropForeignKey("dbo.CarUsers", "GroupId", "dbo.CarUserGroups");
            DropForeignKey("dbo.CarUserGroups", "AreaId", "dbo.ParkAreas");
            DropIndex("dbo.CarUserGroups", new[] { "AreaId" });
            DropIndex("dbo.CarUsers", new[] { "GroupId" });
            DropColumn("dbo.CarUsers", "Remark");
            DropColumn("dbo.CarUsers", "UserType");
            DropColumn("dbo.CarUsers", "GroupId");
            DropTable("dbo.CarUserGroups");
            CreateIndex("dbo.CarUsers", "AreaId");
            AddForeignKey("dbo.CarUsers", "AreaId", "dbo.ParkAreas", "Id", cascadeDelete: true);
        }
    }
}
