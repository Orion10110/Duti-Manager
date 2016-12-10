namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datenullabl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "DateStart", c => c.DateTime());
            AlterColumn("dbo.Projects", "DataFinal", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "DataFinal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projects", "DateStart", c => c.DateTime(nullable: false));
        }
    }
}
