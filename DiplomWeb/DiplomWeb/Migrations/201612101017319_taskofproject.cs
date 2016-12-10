namespace DiplomWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskofproject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskOfProjects", "DateStart", c => c.DateTime());
            AddColumn("dbo.TaskOfProjects", "DataFinal", c => c.DateTime());
            AddColumn("dbo.TaskOfProjects", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskOfProjects", "Priority");
            DropColumn("dbo.TaskOfProjects", "DataFinal");
            DropColumn("dbo.TaskOfProjects", "DateStart");
        }
    }
}
