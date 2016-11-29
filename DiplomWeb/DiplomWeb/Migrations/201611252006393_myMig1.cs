namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myMig1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RecordVigils", "Editable");
            DropColumn("dbo.RecordVigils", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordVigils", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.RecordVigils", "Editable", c => c.Boolean());
        }
    }
}
