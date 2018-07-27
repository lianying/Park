namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class closeAuditCarUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarUsers", "IsSuccess", c => c.Boolean(nullable: false));
            AddColumn("dbo.CarUsers", "CloudId", c => c.String());
            DropColumn("dbo.CarUsers", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarUsers", "CreatorUserId", c => c.Long());
            DropColumn("dbo.CarUsers", "CloudId");
            DropColumn("dbo.CarUsers", "IsSuccess");
        }
    }
}
