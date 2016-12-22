namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Watcher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskOfProjectApplicationUsers",
                c => new
                    {
                        TaskOfProject_id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TaskOfProject_id, t.ApplicationUser_Id })
                .ForeignKey("dbo.TaskOfProjects", t => t.TaskOfProject_id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.TaskOfProject_id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskOfProjectApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskOfProjectApplicationUsers", "TaskOfProject_id", "dbo.TaskOfProjects");
            DropIndex("dbo.TaskOfProjectApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TaskOfProjectApplicationUsers", new[] { "TaskOfProject_id" });
            DropTable("dbo.TaskOfProjectApplicationUsers");
        }
    }
}
