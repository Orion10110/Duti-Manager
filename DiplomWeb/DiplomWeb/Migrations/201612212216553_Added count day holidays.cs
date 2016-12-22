namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcountdayholidays : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Holidays", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Holidays", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            AddColumn("dbo.AspNetUsers", "CountHolidayDays", c => c.Int(nullable: false));
            AlterColumn("dbo.Holidays", "DateStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Holidays", "DataFinal", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Holidays", "DataFinal", c => c.DateTime());
            AlterColumn("dbo.Holidays", "DateStart", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "CountHolidayDays");
            RenameIndex(table: "dbo.Holidays", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Holidays", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
