namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilesCRMs", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilesCRMs", "FileName");
        }
    }
}
