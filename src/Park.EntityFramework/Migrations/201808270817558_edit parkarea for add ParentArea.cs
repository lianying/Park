namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editparkareaforaddParentArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkAreas", "ParkAreaTempCarports", c => c.Int(nullable: false));
            AddColumn("dbo.ParkAreas", "ParkAreaFixedCarports", c => c.Int(nullable: false));
            DropColumn("dbo.ParkAreas", "ParkAreaRentableCarports");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkAreas", "ParkAreaRentableCarports", c => c.Int(nullable: false));
            DropColumn("dbo.ParkAreas", "ParkAreaFixedCarports");
            DropColumn("dbo.ParkAreas", "ParkAreaTempCarports");
        }
    }
}
