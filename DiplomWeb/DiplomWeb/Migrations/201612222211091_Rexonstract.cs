namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rexonstract : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroupApplicationUsers", newName: "ApplicationUserGroups");
            DropForeignKey("dbo.Vigils", "ApplicationRoleID", "dbo.AspNetRoles");
            DropIndex("dbo.Vigils", new[] { "ApplicationRoleID" });
            DropPrimaryKey("dbo.ApplicationUserGroups");
            CreateTable(
                "dbo.VigilGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VigilGroupsApplicationUsers",
                c => new
                    {
                        VigilGroups_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.VigilGroups_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.VigilGroups", t => t.VigilGroups_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.VigilGroups_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.VigilGroupsVigils",
                c => new
                    {
                        VigilGroups_Id = c.Int(nullable: false),
                        Vigil_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VigilGroups_Id, t.Vigil_Id })
                .ForeignKey("dbo.VigilGroups", t => t.VigilGroups_Id, cascadeDelete: true)
                .ForeignKey("dbo.Vigils", t => t.Vigil_Id, cascadeDelete: true)
                .Index(t => t.VigilGroups_Id)
                .Index(t => t.Vigil_Id);
            
            AddColumn("dbo.Vigils", "Days", c => c.String());
            AddPrimaryKey("dbo.ApplicationUserGroups", new[] { "ApplicationUser_Id", "Group_Id" });
            DropColumn("dbo.Vigils", "ApplicationRoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vigils", "ApplicationRoleID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.VigilGroupsVigils", "Vigil_Id", "dbo.Vigils");
            DropForeignKey("dbo.VigilGroupsVigils", "VigilGroups_Id", "dbo.VigilGroups");
            DropForeignKey("dbo.VigilGroupsApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VigilGroupsApplicationUsers", "VigilGroups_Id", "dbo.VigilGroups");
            DropIndex("dbo.VigilGroupsVigils", new[] { "Vigil_Id" });
            DropIndex("dbo.VigilGroupsVigils", new[] { "VigilGroups_Id" });
            DropIndex("dbo.VigilGroupsApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.VigilGroupsApplicationUsers", new[] { "VigilGroups_Id" });
            DropPrimaryKey("dbo.ApplicationUserGroups");
            DropColumn("dbo.Vigils", "Days");
            DropTable("dbo.VigilGroupsVigils");
            DropTable("dbo.VigilGroupsApplicationUsers");
            DropTable("dbo.VigilGroups");
            AddPrimaryKey("dbo.ApplicationUserGroups", new[] { "Group_Id", "ApplicationUser_Id" });
            CreateIndex("dbo.Vigils", "ApplicationRoleID");
            AddForeignKey("dbo.Vigils", "ApplicationRoleID", "dbo.AspNetRoles", "Id");
            RenameTable(name: "dbo.ApplicationUserGroups", newName: "GroupApplicationUsers");
        }
    }
}
