namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status_Task : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskOfProjects", "TaskStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskOfProjects", "TaskStatus");
        }
    }
}
