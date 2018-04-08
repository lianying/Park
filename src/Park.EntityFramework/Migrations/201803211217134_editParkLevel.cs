namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editParkLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkLevels", "ParkId", c => c.Int(nullable: false));
            CreateIndex("dbo.ParkLevels", "ParkId");
            AddForeignKey("dbo.ParkLevels", "ParkId", "dbo.Park", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkLevels", "ParkId", "dbo.Park");
            DropIndex("dbo.ParkLevels", new[] { "ParkId" });
            DropColumn("dbo.ParkLevels", "ParkId");
        }
    }
}
