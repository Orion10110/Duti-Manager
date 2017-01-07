namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changefieldname : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FilesCRMs", new[] { "TaskOfProject_id" });
            DropIndex("dbo.TaskOfProjectApplicationUsers", new[] { "TaskOfProject_id" });
            CreateIndex("dbo.FilesCRMs", "TaskOfProject_Id");
            CreateIndex("dbo.TaskOfProjectApplicationUsers", "TaskOfProject_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TaskOfProjectApplicationUsers", new[] { "TaskOfProject_Id" });
            DropIndex("dbo.FilesCRMs", new[] { "TaskOfProject_Id" });
            CreateIndex("dbo.TaskOfProjectApplicationUsers", "TaskOfProject_id");
            CreateIndex("dbo.FilesCRMs", "TaskOfProject_id");
        }
    }
}
