namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class closeAuditCarNumberCarPort : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CarPorts", "CreatorUserId");
            DropColumn("dbo.CarNumbers", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarNumbers", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CarPorts", "CreatorUserId", c => c.Long());
        }
    }
}
