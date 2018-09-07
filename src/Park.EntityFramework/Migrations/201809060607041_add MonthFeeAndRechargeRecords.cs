namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMonthFeeAndRechargeRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthFees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CarTypeId = c.Long(nullable: false),
                        Month = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quarter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HalfYear = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Year = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarTypes", t => t.CarTypeId, cascadeDelete: false)
                .Index(t => t.CarTypeId);
            
            CreateTable(
                "dbo.RechargeRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Receivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualHarvest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Long(nullable: false),
                        CarPortId = c.Long(nullable: false),
                        CarNumbers = c.String(),
                        ExtensionCount = c.Int(nullable: false),
                        OldDate = c.DateTime(nullable: false),
                        NewDate = c.DateTime(nullable: false),
                        Remark = c.String(),
                        SysUserId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        CloudId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarPorts", t => t.CarPortId, cascadeDelete: false)
                .ForeignKey("dbo.CarUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.AbpUsers", t => t.SysUserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CarPortId)
                .Index(t => t.SysUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RechargeRecords", "SysUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.RechargeRecords", "UserId", "dbo.CarUsers");
            DropForeignKey("dbo.RechargeRecords", "CarPortId", "dbo.CarPorts");
            DropForeignKey("dbo.MonthFees", "CarTypeId", "dbo.CarTypes");
            DropIndex("dbo.RechargeRecords", new[] { "SysUserId" });
            DropIndex("dbo.RechargeRecords", new[] { "CarPortId" });
            DropIndex("dbo.RechargeRecords", new[] { "UserId" });
            DropIndex("dbo.MonthFees", new[] { "CarTypeId" });
            DropTable("dbo.RechargeRecords");
            DropTable("dbo.MonthFees");
        }
    }
}
