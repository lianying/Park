namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcarOutRecord : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarOutRecords", "CarInPhotoId", "dbo.CarInOutImages");
            DropForeignKey("dbo.CarOutRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.CarOutRecords", "CarId", "dbo.CarUsers");
            DropIndex("dbo.CarOutRecords", new[] { "CarId" });
            DropIndex("dbo.CarOutRecords", new[] { "CarPortId" });
            DropIndex("dbo.CarOutRecords", new[] { "CarInPhotoId" });
            AddColumn("dbo.CarInRecords", "InType", c => c.Byte(nullable: false));
            AlterColumn("dbo.CarOutRecords", "CarId", c => c.Long());
            AlterColumn("dbo.CarOutRecords", "CarPortId", c => c.Long());
            AlterColumn("dbo.CarOutRecords", "CarInPhotoId", c => c.Long());
            CreateIndex("dbo.CarOutRecords", "CarId");
            CreateIndex("dbo.CarOutRecords", "CarPortId");
            CreateIndex("dbo.CarOutRecords", "CarInPhotoId");
            AddForeignKey("dbo.CarOutRecords", "CarInPhotoId", "dbo.CarInOutImages", "Id");
            AddForeignKey("dbo.CarOutRecords", "CarPortId", "dbo.CarPorts", "Id");
            AddForeignKey("dbo.CarOutRecords", "CarId", "dbo.CarUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarOutRecords", "CarId", "dbo.CarUsers");
            DropForeignKey("dbo.CarOutRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.CarOutRecords", "CarInPhotoId", "dbo.CarInOutImages");
            DropIndex("dbo.CarOutRecords", new[] { "CarInPhotoId" });
            DropIndex("dbo.CarOutRecords", new[] { "CarPortId" });
            DropIndex("dbo.CarOutRecords", new[] { "CarId" });
            AlterColumn("dbo.CarOutRecords", "CarInPhotoId", c => c.Long(nullable: false));
            AlterColumn("dbo.CarOutRecords", "CarPortId", c => c.Long(nullable: false));
            AlterColumn("dbo.CarOutRecords", "CarId", c => c.Long(nullable: false));
            DropColumn("dbo.CarInRecords", "InType");
            CreateIndex("dbo.CarOutRecords", "CarInPhotoId");
            CreateIndex("dbo.CarOutRecords", "CarPortId");
            CreateIndex("dbo.CarOutRecords", "CarId");
            AddForeignKey("dbo.CarOutRecords", "CarId", "dbo.CarUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CarOutRecords", "CarPortId", "dbo.CarPorts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CarOutRecords", "CarInPhotoId", "dbo.CarInOutImages", "Id", cascadeDelete: true);
        }
    }
}
