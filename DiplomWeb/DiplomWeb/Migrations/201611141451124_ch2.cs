namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ch2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RecordVigils", name: "ApplicationUser_Id", newName: "ApplicationUserID");
            RenameColumn(table: "dbo.Vigils", name: "Role_Id", newName: "ApplicationRoleID");
            RenameIndex(table: "dbo.Vigils", name: "IX_Role_Id", newName: "IX_ApplicationRoleID");
            RenameIndex(table: "dbo.RecordVigils", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserID");
            DropColumn("dbo.RecordVigils", "UserID");
            DropColumn("dbo.Vigils", "RoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vigils", "RoleID", c => c.Int());
            AddColumn("dbo.RecordVigils", "UserID", c => c.Int());
            RenameIndex(table: "dbo.RecordVigils", name: "IX_ApplicationUserID", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Vigils", name: "IX_ApplicationRoleID", newName: "IX_Role_Id");
            RenameColumn(table: "dbo.Vigils", name: "ApplicationRoleID", newName: "Role_Id");
            RenameColumn(table: "dbo.RecordVigils", name: "ApplicationUserID", newName: "ApplicationUser_Id");
        }
    }
}
