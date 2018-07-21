namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcarInRecord : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarInRecords", "CarInPhotoId", "dbo.CarInOutImages");
            DropForeignKey("dbo.CarInRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.CarInRecords", "CarId", "dbo.CarUsers");
            DropIndex("dbo.CarInRecords", new[] { "CarId" });
            DropIndex("dbo.CarInRecords", new[] { "CarPortId" });
            DropIndex("dbo.CarInRecords", new[] { "CarInPhotoId" });
            AlterColumn("dbo.CarInRecords", "CarId", c => c.Long());
            AlterColumn("dbo.CarInRecords", "CarPortId", c => c.Long());
            AlterColumn("dbo.CarInRecords", "CarInPhotoId", c => c.Long());
            CreateIndex("dbo.CarInRecords", "CarId");
            CreateIndex("dbo.CarInRecords", "CarPortId");
            CreateIndex("dbo.CarInRecords", "CarInPhotoId");
            AddForeignKey("dbo.CarInRecords", "CarInPhotoId", "dbo.CarInOutImages", "Id");
            AddForeignKey("dbo.CarInRecords", "CarPortId", "dbo.CarPorts", "Id");
            AddForeignKey("dbo.CarInRecords", "CarId", "dbo.CarUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarInRecords", "CarId", "dbo.CarUsers");
            DropForeignKey("dbo.CarInRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.CarInRecords", "CarInPhotoId", "dbo.CarInOutImages");
            DropIndex("dbo.CarInRecords", new[] { "CarInPhotoId" });
            DropIndex("dbo.CarInRecords", new[] { "CarPortId" });
            DropIndex("dbo.CarInRecords", new[] { "CarId" });
            AlterColumn("dbo.CarInRecords", "CarInPhotoId", c => c.Long(nullable: false));
            AlterColumn("dbo.CarInRecords", "CarPortId", c => c.Long(nullable: false));
            AlterColumn("dbo.CarInRecords", "CarId", c => c.Long(nullable: false));
            CreateIndex("dbo.CarInRecords", "CarInPhotoId");
            CreateIndex("dbo.CarInRecords", "CarPortId");
            CreateIndex("dbo.CarInRecords", "CarId");
            AddForeignKey("dbo.CarInRecords", "CarId", "dbo.CarUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CarInRecords", "CarPortId", "dbo.CarPorts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CarInRecords", "CarInPhotoId", "dbo.CarInOutImages", "Id", cascadeDelete: true);
        }
    }
}
