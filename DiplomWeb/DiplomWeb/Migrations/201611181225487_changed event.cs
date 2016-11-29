namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedevent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVigils", "Title", c => c.String());
            AddColumn("dbo.RecordVigils", "Description", c => c.String());
            AddColumn("dbo.RecordVigils", "StartAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordVigils", "EndAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordVigils", "IsFullDay", c => c.Boolean(nullable: false));
            DropColumn("dbo.RecordVigils", "Name");
            DropColumn("dbo.RecordVigils", "DateVigil");
            DropColumn("dbo.RecordVigils", "Note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordVigils", "Note", c => c.String());
            AddColumn("dbo.RecordVigils", "DateVigil", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordVigils", "Name", c => c.String());
            DropColumn("dbo.RecordVigils", "IsFullDay");
            DropColumn("dbo.RecordVigils", "EndAt");
            DropColumn("dbo.RecordVigils", "StartAt");
            DropColumn("dbo.RecordVigils", "Description");
            DropColumn("dbo.RecordVigils", "Title");
        }
    }
}
