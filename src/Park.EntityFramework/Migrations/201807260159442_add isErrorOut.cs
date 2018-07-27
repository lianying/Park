namespace Park.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisErrorOut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarOutRecords", "IsErrorOut", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarOutRecords", "IsErrorOut");
        }
    }
}
