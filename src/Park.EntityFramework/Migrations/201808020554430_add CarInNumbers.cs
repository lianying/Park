namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarInNumbers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarInRecords", "CarInNumbers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarInRecords", "CarInNumbers");
        }
    }
}
