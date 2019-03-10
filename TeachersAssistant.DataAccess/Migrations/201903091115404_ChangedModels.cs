namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentSubmissions", "AssignmentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentSubmissions", "AssignmentName");
        }
    }
}
