namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarportRentSell : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPorts", "RentSellType", c => c.Int(nullable: false));
            AddColumn("dbo.CarTypes", "RentingSellingType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarTypes", "RentingSellingType");
            DropColumn("dbo.CarPorts", "RentSellType");
        }
    }
}
