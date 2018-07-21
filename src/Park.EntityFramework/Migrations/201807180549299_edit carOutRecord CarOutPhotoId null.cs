namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcarOutRecordCarOutPhotoIdnull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarOutRecords", "CarOutPhotoId", "dbo.CarInOutImages");
            DropIndex("dbo.CarOutRecords", new[] { "CarOutPhotoId" });
            AlterColumn("dbo.CarOutRecords", "CarOutPhotoId", c => c.Long());
            CreateIndex("dbo.CarOutRecords", "CarOutPhotoId");
            AddForeignKey("dbo.CarOutRecords", "CarOutPhotoId", "dbo.CarInOutImages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarOutRecords", "CarOutPhotoId", "dbo.CarInOutImages");
            DropIndex("dbo.CarOutRecords", new[] { "CarOutPhotoId" });
            AlterColumn("dbo.CarOutRecords", "CarOutPhotoId", c => c.Long(nullable: false));
            CreateIndex("dbo.CarOutRecords", "CarOutPhotoId");
            AddForeignKey("dbo.CarOutRecords", "CarOutPhotoId", "dbo.CarInOutImages", "Id", cascadeDelete: true);
        }
    }
}
