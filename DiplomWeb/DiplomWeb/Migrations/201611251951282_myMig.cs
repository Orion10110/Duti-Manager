namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVigils", "Editable", c => c.Boolean());
            AddColumn("dbo.RecordVigils", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordVigils", "Discriminator");
            DropColumn("dbo.RecordVigils", "Editable");
        }
    }
}
