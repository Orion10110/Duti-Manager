namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixreconstruct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VigilGroupsApplicationUsers", "VigilGroups_Id", "dbo.VigilGroups");
            DropForeignKey("dbo.VigilGroupsApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VigilGroupsVigils", "VigilGroups_Id", "dbo.VigilGroups");
            DropForeignKey("dbo.VigilGroupsVigils", "Vigil_Id", "dbo.Vigils");
            DropIndex("dbo.VigilGroupsApplicationUsers", new[] { "VigilGroups_Id" });
            DropIndex("dbo.VigilGroupsApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.VigilGroupsVigils", new[] { "VigilGroups_Id" });
            DropIndex("dbo.VigilGroupsVigils", new[] { "Vigil_Id" });
            CreateTable(
                "dbo.VigilApplicationUsers",
                c => new
                    {
                        Vigil_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Vigil_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Vigils", t => t.Vigil_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Vigil_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropTable("dbo.VigilGroups");
            DropTable("dbo.VigilGroupsApplicationUsers");
            DropTable("dbo.VigilGroupsVigils");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VigilGroupsVigils",
                c => new
                    {
                        VigilGroups_Id = c.Int(nullable: false),
                        Vigil_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VigilGroups_Id, t.Vigil_Id });
            
            CreateTable(
                "dbo.VigilGroupsApplicationUsers",
                c => new
                    {
                        VigilGroups_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.VigilGroups_Id, t.ApplicationUser_Id });
            
            CreateTable(
                "dbo.VigilGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.VigilApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VigilApplicationUsers", "Vigil_Id", "dbo.Vigils");
            DropIndex("dbo.VigilApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.VigilApplicationUsers", new[] { "Vigil_Id" });
            DropTable("dbo.VigilApplicationUsers");
            CreateIndex("dbo.VigilGroupsVigils", "Vigil_Id");
            CreateIndex("dbo.VigilGroupsVigils", "VigilGroups_Id");
            CreateIndex("dbo.VigilGroupsApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.VigilGroupsApplicationUsers", "VigilGroups_Id");
            AddForeignKey("dbo.VigilGroupsVigils", "Vigil_Id", "dbo.Vigils", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VigilGroupsVigils", "VigilGroups_Id", "dbo.VigilGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VigilGroupsApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VigilGroupsApplicationUsers", "VigilGroups_Id", "dbo.VigilGroups", "Id", cascadeDelete: true);
        }
    }
}
