namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editentracePermission : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarTypes", "ParkEntrancePermission_Id", "dbo.ParkEntrancePermissions");
            DropIndex("dbo.CarTypes", new[] { "ParkEntrancePermission_Id" });
            AddColumn("dbo.ParkEntrancePermissions", "CarTypes", c => c.String());
            DropColumn("dbo.CarTypes", "ParkEntrancePermission_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarTypes", "ParkEntrancePermission_Id", c => c.Long());
            DropColumn("dbo.ParkEntrancePermissions", "CarTypes");
            CreateIndex("dbo.CarTypes", "ParkEntrancePermission_Id");
            AddForeignKey("dbo.CarTypes", "ParkEntrancePermission_Id", "dbo.ParkEntrancePermissions", "Id");
        }
    }
}
