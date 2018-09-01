namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editParkEntrance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkEntrances", "AreaId", c => c.Long(nullable: false));
            CreateIndex("dbo.ParkEntrances", "AreaId");
            AddForeignKey("dbo.ParkEntrances", "AreaId", "dbo.ParkAreas", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkEntrances", "AreaId", "dbo.ParkAreas");
            DropIndex("dbo.ParkEntrances", new[] { "AreaId" });
            DropColumn("dbo.ParkEntrances", "AreaId");
        }
    }
}
