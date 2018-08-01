namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addCarErrorOut : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarErrorRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarNumber = c.String(),
                        InTime = c.DateTime(nullable: false),
                        CarId = c.Long(),
                        CarPortId = c.Long(),
                        CarInCount = c.Int(nullable: false),
                        InType = c.Byte(nullable: false),
                        IsMonthTempIn = c.Boolean(nullable: false),
                        IsMonthTimeOutInt = c.Boolean(nullable: false),
                        ParkId = c.Int(nullable: false),
                        TempConvertMonthTime = c.DateTime(),
                        CarInPhotoId = c.Long(),
                        IsInSuccess = c.Boolean(nullable: false),
                        InCloudId = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        OutTime = c.DateTime(nullable: false),
                        Receivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdvancePayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OfferMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OfferTime = c.Time(precision: 7),
                        CarDiscountId = c.Long(),
                        OutType = c.Byte(nullable: false),
                        CarOutPhotoId = c.Long(),
                        OutPhotoTime = c.DateTime(),
                        PayType = c.Byte(nullable: false),
                        OutOfferType = c.Int(nullable: false),
                        Remark = c.String(),
                        IsErrorOut = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarErrorRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarDiscounts", t => t.CarDiscountId)
                .ForeignKey("dbo.CarInOutImages", t => t.CarInPhotoId)
                .ForeignKey("dbo.CarInOutImages", t => t.CarOutPhotoId)
                .ForeignKey("dbo.CarPorts", t => t.CarPortId)
                .ForeignKey("dbo.CarUsers", t => t.CarId)
                .ForeignKey("dbo.Park", t => t.ParkId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CarPortId)
                .Index(t => t.ParkId)
                .Index(t => t.CarInPhotoId)
                .Index(t => t.CarDiscountId)
                .Index(t => t.CarOutPhotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarErrorRecords", "ParkId", "dbo.Park");
            DropForeignKey("dbo.CarErrorRecords", "CarId", "dbo.CarUsers");
            DropForeignKey("dbo.CarErrorRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.CarErrorRecords", "CarOutPhotoId", "dbo.CarInOutImages");
            DropForeignKey("dbo.CarErrorRecords", "CarInPhotoId", "dbo.CarInOutImages");
            DropForeignKey("dbo.CarErrorRecords", "CarDiscountId", "dbo.CarDiscounts");
            DropIndex("dbo.CarErrorRecords", new[] { "CarOutPhotoId" });
            DropIndex("dbo.CarErrorRecords", new[] { "CarDiscountId" });
            DropIndex("dbo.CarErrorRecords", new[] { "CarInPhotoId" });
            DropIndex("dbo.CarErrorRecords", new[] { "ParkId" });
            DropIndex("dbo.CarErrorRecords", new[] { "CarPortId" });
            DropIndex("dbo.CarErrorRecords", new[] { "CarId" });
            DropTable("dbo.CarErrorRecords",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarErrorRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
