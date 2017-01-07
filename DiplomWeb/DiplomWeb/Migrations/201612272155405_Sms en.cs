namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Smsen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SMSNotifications", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SMSNotifications");
        }
    }
}
