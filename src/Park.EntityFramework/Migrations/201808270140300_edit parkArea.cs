namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editparkArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkAreas", "ParentAreaId", c => c.Long());
            CreateIndex("dbo.ParkAreas", "ParentAreaId");
            AddForeignKey("dbo.ParkAreas", "ParentAreaId", "dbo.ParkAreas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkAreas", "ParentAreaId", "dbo.ParkAreas");
            DropIndex("dbo.ParkAreas", new[] { "ParentAreaId" });
            DropColumn("dbo.ParkAreas", "ParentAreaId");
        }
    }
}
