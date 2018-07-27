namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addblackList : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CarOutRecords", name: "CarDiscount_Id", newName: "CarDiscountId");
            RenameIndex(table: "dbo.CarOutRecords", name: "IX_CarDiscount_Id", newName: "IX_CarDiscountId");
            CreateTable(
                "dbo.BlackLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarNumber = c.String(),
                        OperationDate = c.DateTime(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CarDiscounts", "ParkId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarDiscounts", "ParkId");
            DropTable("dbo.BlackLists");
            RenameIndex(table: "dbo.CarOutRecords", name: "IX_CarDiscountId", newName: "IX_CarDiscount_Id");
            RenameColumn(table: "dbo.CarOutRecords", name: "CarDiscountId", newName: "CarDiscount_Id");
        }
    }
}
