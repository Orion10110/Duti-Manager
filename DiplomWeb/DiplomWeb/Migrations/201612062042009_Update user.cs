namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecondName", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailNotifications", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ImageAvatar", c => c.String());
            DropColumn("dbo.AspNetUsers", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "ImageAvatar");
            DropColumn("dbo.AspNetUsers", "DateBirth");
            DropColumn("dbo.AspNetUsers", "EmailNotifications");
            DropColumn("dbo.AspNetUsers", "SecondName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
