namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcarOutRecordTempConvertMonthTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarOutRecords", "TempConvertMonthTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarOutRecords", "TempConvertMonthTime", c => c.DateTime(nullable: false));
        }
    }
}
