namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class desktopuserpassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DesctopPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DesctopPassword");
        }
    }
}
