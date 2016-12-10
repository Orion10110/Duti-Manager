namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FIlesCRMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Project_Id = c.Int(),
                        TaskOfProject_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .ForeignKey("dbo.TaskOfProjects", t => t.TaskOfProject_id)
                .Index(t => t.Project_Id)
                .Index(t => t.TaskOfProject_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FIlesCRMs", "TaskOfProject_id", "dbo.TaskOfProjects");
            DropForeignKey("dbo.FIlesCRMs", "Project_Id", "dbo.Projects");
            DropIndex("dbo.FIlesCRMs", new[] { "TaskOfProject_id" });
            DropIndex("dbo.FIlesCRMs", new[] { "Project_Id" });
            DropTable("dbo.FIlesCRMs");
        }
    }
}
