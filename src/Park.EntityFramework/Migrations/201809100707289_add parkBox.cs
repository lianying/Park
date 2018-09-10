namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparkBox : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkBoxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Ip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cameras", "BoxId", c => c.Int());
            AddColumn("dbo.Leds", "BoxId", c => c.Int());
            CreateIndex("dbo.Cameras", "BoxId");
            CreateIndex("dbo.Leds", "BoxId");
            AddForeignKey("dbo.Cameras", "BoxId", "dbo.ParkBoxes", "Id");
            AddForeignKey("dbo.Leds", "BoxId", "dbo.ParkBoxes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leds", "BoxId", "dbo.ParkBoxes");
            DropForeignKey("dbo.Cameras", "BoxId", "dbo.ParkBoxes");
            DropIndex("dbo.Leds", new[] { "BoxId" });
            DropIndex("dbo.Cameras", new[] { "BoxId" });
            DropColumn("dbo.Leds", "BoxId");
            DropColumn("dbo.Cameras", "BoxId");
            DropTable("dbo.ParkBoxes");
        }
    }
}
