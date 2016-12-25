namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpatronymic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Patronymic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Patronymic");
        }
    }
}
