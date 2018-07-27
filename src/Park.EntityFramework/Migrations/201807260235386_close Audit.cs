namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class closeAudit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CarInRecords", "CreatorUserId");
            DropColumn("dbo.CarOutRecords", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarOutRecords", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CarInRecords", "CreatorUserId", c => c.Long());
        }
    }
}
