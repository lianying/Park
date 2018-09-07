namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarUserContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarUsers", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarUsers", "Contact");
        }
    }
}
