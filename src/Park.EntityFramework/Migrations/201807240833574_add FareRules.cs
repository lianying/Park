namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFareRules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FareRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        parkId = c.Int(nullable: false),
                        CarTypeId = c.Long(nullable: false),
                        Name = c.String(),
                        FreeTime = c.Single(nullable: false),
                        IsStartTopMoney = c.Boolean(nullable: false),
                        DayMaxMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        preFeeDate = c.Single(nullable: false),
                        IsInculdeFeeTime = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarTypes", t => t.CarTypeId, cascadeDelete: true)
                .Index(t => t.CarTypeId);
            
            CreateTable(
                "dbo.RangeTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        FeeMinutes = c.Double(nullable: false),
                        FeeMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TopMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinSpanTime = c.Double(nullable: false),
                        FareRuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FareRules", t => t.FareRuleId, cascadeDelete: true)
                .Index(t => t.FareRuleId);
            
            AddColumn("dbo.CarInRecords", "CarInPermission", c => c.Int(nullable: false));
            AddColumn("dbo.CarInRecords", "AdvancePayment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CarOutRecords", "Receivable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CarOutRecords", "AdvancePayment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CarOutRecords", "OutOfferType", c => c.Int(nullable: false));
            AddColumn("dbo.CarOutRecords", "Remark", c => c.String());
            AlterColumn("dbo.CarTypes", "Category", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RangeTimes", "FareRuleId", "dbo.FareRules");
            DropForeignKey("dbo.FareRules", "CarTypeId", "dbo.CarTypes");
            DropIndex("dbo.RangeTimes", new[] { "FareRuleId" });
            DropIndex("dbo.FareRules", new[] { "CarTypeId" });
            AlterColumn("dbo.CarTypes", "Category", c => c.Int(nullable: false));
            DropColumn("dbo.CarOutRecords", "Remark");
            DropColumn("dbo.CarOutRecords", "OutOfferType");
            DropColumn("dbo.CarOutRecords", "AdvancePayment");
            DropColumn("dbo.CarOutRecords", "Receivable");
            DropColumn("dbo.CarInRecords", "AdvancePayment");
            DropColumn("dbo.CarInRecords", "CarInPermission");
            DropTable("dbo.RangeTimes");
            DropTable("dbo.FareRules");
        }
    }
}
