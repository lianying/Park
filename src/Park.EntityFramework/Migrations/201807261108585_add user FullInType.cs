namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserFullInType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarUsers", "FullInType", c => c.Int(nullable: false));
            DropColumn("dbo.ParkEntrancePermissions", "CarTypes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkEntrancePermissions", "CarTypes", c => c.String());
            DropColumn("dbo.CarUsers", "FullInType");
        }
    }
}
