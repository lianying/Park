namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparkOpertor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Park", "PropertyParty", c => c.String());
            AddColumn("dbo.Park", "Operator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Park", "Operator");
            DropColumn("dbo.Park", "PropertyParty");
        }
    }
}
