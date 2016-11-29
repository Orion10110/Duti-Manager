namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVigils", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordVigils", "Color");
        }
    }
}
