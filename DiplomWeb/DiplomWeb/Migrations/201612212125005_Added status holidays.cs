namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedstatusholidays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holidays", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Holidays", "Status");
        }
    }
}
