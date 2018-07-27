namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editentrancePermissiontypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarTypes", "ParkEntrancePermission_Id", c => c.Long());
            CreateIndex("dbo.CarTypes", "ParkEntrancePermission_Id");
            AddForeignKey("dbo.CarTypes", "ParkEntrancePermission_Id", "dbo.ParkEntrancePermissions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarTypes", "ParkEntrancePermission_Id", "dbo.ParkEntrancePermissions");
            DropIndex("dbo.CarTypes", new[] { "ParkEntrancePermission_Id" });
            DropColumn("dbo.CarTypes", "ParkEntrancePermission_Id");
        }
    }
}
