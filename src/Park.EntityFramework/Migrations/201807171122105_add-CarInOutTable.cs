namespace Park.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addCarInOutTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarDiscounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DiscountId = c.String(),
                        DiscountType = c.Byte(nullable: false),
                        CarNumber = c.String(maxLength: 20),
                        DiscountValue = c.String(maxLength: 200),
                        DiscountExpire = c.DateTime(nullable: false),
                        CloudParkId = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarDiscount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarInRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarNumber = c.String(),
                        InTime = c.DateTime(nullable: false),
                        CarId = c.Long(nullable: false),
                        CarPortId = c.Long(nullable: false),
                        CarInCount = c.Int(nullable: false),
                        IsMonthTempIn = c.Boolean(nullable: false),
                        IsMonthTimeOutInt = c.Boolean(nullable: false),
                        TempConvertMonthTime = c.DateTime(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        CarInPhotoId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarInRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarInOutImages", t => t.CarInPhotoId, cascadeDelete: false)
                .ForeignKey("dbo.CarPorts", t => t.CarPortId, cascadeDelete: true)
                .ForeignKey("dbo.CarUsers", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CarPortId)
                .Index(t => t.CarInPhotoId);
            
            CreateTable(
                "dbo.CarInOutImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Path = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarOutRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarNumber = c.String(),
                        InTime = c.DateTime(nullable: false),
                        CarId = c.Long(nullable: false),
                        CarPortId = c.Long(nullable: false),
                        CarInCount = c.Int(nullable: false),
                        InType = c.Byte(nullable: false),
                        IsMonthTempIn = c.Boolean(nullable: false),
                        IsMonthTimeOutInt = c.Boolean(nullable: false),
                        TempConvertMonthTime = c.DateTime(nullable: false),
                        CarInPhotoId = c.Long(nullable: false),
                        IsInSuccess = c.Boolean(nullable: false),
                        InCloudId = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        OutTime = c.DateTime(nullable: false),
                        Pay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OfferMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OfferTime = c.Time(precision: 7),
                        OutType = c.Byte(nullable: false),
                        CarOutPhotoId = c.Long(nullable: false),
                        OutPhotoTime = c.DateTime(),
                        PayType = c.Byte(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        CarDiscount_Id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarOutRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarDiscounts", t => t.CarDiscount_Id)
                .ForeignKey("dbo.CarInOutImages", t => t.CarInPhotoId, cascadeDelete: false)
                .ForeignKey("dbo.CarInOutImages", t => t.CarOutPhotoId, cascadeDelete: false)
                .ForeignKey("dbo.CarPorts", t => t.CarPortId, cascadeDelete: true)
                .ForeignKey("dbo.CarUsers", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CarPortId)
                .Index(t => t.CarInPhotoId)
                .Index(t => t.CarOutPhotoId)
                .Index(t => t.CarDiscount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarOutRecords", "CarId", "dbo.CarUsers");
            DropForeignKey("dbo.CarOutRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.CarOutRecords", "CarOutPhotoId", "dbo.CarInOutImages");
            DropForeignKey("dbo.CarOutRecords", "CarInPhotoId", "dbo.CarInOutImages");
            DropForeignKey("dbo.CarOutRecords", "CarDiscount_Id", "dbo.CarDiscounts");
            DropForeignKey("dbo.CarInRecords", "CarId", "dbo.CarUsers");
            DropForeignKey("dbo.CarInRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.CarInRecords", "CarInPhotoId", "dbo.CarInOutImages");
            DropIndex("dbo.CarOutRecords", new[] { "CarDiscount_Id" });
            DropIndex("dbo.CarOutRecords", new[] { "CarOutPhotoId" });
            DropIndex("dbo.CarOutRecords", new[] { "CarInPhotoId" });
            DropIndex("dbo.CarOutRecords", new[] { "CarPortId" });
            DropIndex("dbo.CarOutRecords", new[] { "CarId" });
            DropIndex("dbo.CarInRecords", new[] { "CarInPhotoId" });
            DropIndex("dbo.CarInRecords", new[] { "CarPortId" });
            DropIndex("dbo.CarInRecords", new[] { "CarId" });
            DropTable("dbo.CarOutRecords",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarOutRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CarInOutImages");
            DropTable("dbo.CarInRecords",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarInRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CarDiscounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CarDiscount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
