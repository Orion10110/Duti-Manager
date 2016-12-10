namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtaskofproject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskOfProjects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FromWhomId = c.String(nullable: false, maxLength: 128),
                        ForWhomId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.ForWhomId)
                .ForeignKey("dbo.AspNetUsers", t => t.FromWhomId)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.FromWhomId)
                .Index(t => t.ForWhomId)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupProjects",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Project_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskOfProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.GroupProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.GroupProjects", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.TaskOfProjects", "FromWhomId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskOfProjects", "ForWhomId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupProjects", new[] { "Project_Id" });
            DropIndex("dbo.GroupProjects", new[] { "Group_Id" });
            DropIndex("dbo.TaskOfProjects", new[] { "Project_Id" });
            DropIndex("dbo.TaskOfProjects", new[] { "ForWhomId" });
            DropIndex("dbo.TaskOfProjects", new[] { "FromWhomId" });
            DropTable("dbo.GroupProjects");
            DropTable("dbo.Projects");
            DropTable("dbo.TaskOfProjects");
        }
    }
}
