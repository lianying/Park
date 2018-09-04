namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editentraces : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkEntrances", "ParkLevelId", "dbo.ParkLevels");
            DropIndex("dbo.ParkEntrances", new[] { "ParkLevelId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ParkEntrances", "ParkLevelId");
            AddForeignKey("dbo.ParkEntrances", "ParkLevelId", "dbo.ParkLevels", "Id", cascadeDelete: true);
        }
    }
}
