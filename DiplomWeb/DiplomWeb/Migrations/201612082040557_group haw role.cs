namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grouphawrole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupProjects", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupProjects", "Project_Id", "dbo.Projects");
            DropIndex("dbo.GroupProjects", new[] { "Group_Id" });
            DropIndex("dbo.GroupProjects", new[] { "Project_Id" });
            AddColumn("dbo.Groups", "Project_Id", c => c.Int());
            CreateIndex("dbo.Groups", "Project_Id");
            AddForeignKey("dbo.Groups", "Project_Id", "dbo.Projects", "Id");
            DropTable("dbo.GroupProjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupProjects",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Project_Id });
            
            DropForeignKey("dbo.Groups", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Groups", new[] { "Project_Id" });
            DropColumn("dbo.Groups", "Project_Id");
            CreateIndex("dbo.GroupProjects", "Project_Id");
            CreateIndex("dbo.GroupProjects", "Group_Id");
            AddForeignKey("dbo.GroupProjects", "Project_Id", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupProjects", "Group_Id", "dbo.Groups", "Id", cascadeDelete: true);
        }
    }
}
