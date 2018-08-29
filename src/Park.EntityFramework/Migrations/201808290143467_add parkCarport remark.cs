namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparkCarportremark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPorts", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarPorts", "Remark");
        }
    }
}
