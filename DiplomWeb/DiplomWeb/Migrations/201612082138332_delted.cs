namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Delted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Delted");
        }
    }
}
