namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempConvertMonthTimeisNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarInRecords", "TempConvertMonthTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarInRecords", "TempConvertMonthTime", c => c.DateTime(nullable: false));
        }
    }
}
