namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcarNumberUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarNumbers", "Contact", c => c.String());
            AddColumn("dbo.CarNumbers", "Phone", c => c.String());
            AddColumn("dbo.CarNumbers", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarNumbers", "Remark");
            DropColumn("dbo.CarNumbers", "Phone");
            DropColumn("dbo.CarNumbers", "Contact");
        }
    }
}
