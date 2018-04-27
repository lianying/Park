namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcarUserTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarUsers", "ParkArea_Id", "dbo.ParkAreas");
            DropIndex("dbo.CarUsers", new[] { "ParkArea_Id" });
            RenameColumn(table: "dbo.CarUsers", name: "ParkArea_Id", newName: "AreaId");
            AlterColumn("dbo.CarUsers", "AreaId", c => c.Long(nullable: false));
            CreateIndex("dbo.CarUsers", "AreaId");
            AddForeignKey("dbo.CarUsers", "AreaId", "dbo.ParkAreas", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarUsers", "AreaId", "dbo.ParkAreas");
            DropIndex("dbo.CarUsers", new[] { "AreaId" });
            AlterColumn("dbo.CarUsers", "AreaId", c => c.Long());
            RenameColumn(table: "dbo.CarUsers", name: "AreaId", newName: "ParkArea_Id");
            CreateIndex("dbo.CarUsers", "ParkArea_Id");
            AddForeignKey("dbo.CarUsers", "ParkArea_Id", "dbo.ParkAreas", "Id");
        }
    }
}
