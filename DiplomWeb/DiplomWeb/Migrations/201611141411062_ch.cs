namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ch : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Vigils", name: "ApplicationRole_Id", newName: "Role_Id");
            RenameIndex(table: "dbo.Vigils", name: "IX_ApplicationRole_Id", newName: "IX_Role_Id");
            AddColumn("dbo.Vigils", "RoleID", c => c.Int());
            DropColumn("dbo.Vigils", "ApplicationRoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vigils", "ApplicationRoleID", c => c.Int());
            DropColumn("dbo.Vigils", "RoleID");
            RenameIndex(table: "dbo.Vigils", name: "IX_Role_Id", newName: "IX_ApplicationRole_Id");
            RenameColumn(table: "dbo.Vigils", name: "Role_Id", newName: "ApplicationRole_Id");
        }
    }
}
