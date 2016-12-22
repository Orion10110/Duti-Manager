namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tasks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskOfProjects", "Project_Id", "dbo.Projects");
            DropIndex("dbo.TaskOfProjects", new[] { "Project_Id" });
            RenameColumn(table: "dbo.TaskOfProjects", name: "Project_Id", newName: "ProjectId");
            AlterColumn("dbo.TaskOfProjects", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskOfProjects", "ProjectId");
            AddForeignKey("dbo.TaskOfProjects", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskOfProjects", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TaskOfProjects", new[] { "ProjectId" });
            AlterColumn("dbo.TaskOfProjects", "ProjectId", c => c.Int());
            RenameColumn(table: "dbo.TaskOfProjects", name: "ProjectId", newName: "Project_Id");
            CreateIndex("dbo.TaskOfProjects", "Project_Id");
            AddForeignKey("dbo.TaskOfProjects", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
