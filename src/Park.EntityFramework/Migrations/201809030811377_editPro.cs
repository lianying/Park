namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkEntrances", "AreaId", "dbo.ParkAreas");
            DropIndex("dbo.ParkEntrances", new[] { "AreaId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ParkEntrances", "AreaId");
            AddForeignKey("dbo.ParkEntrances", "AreaId", "dbo.ParkAreas", "Id", cascadeDelete: true);
        }
    }
}
