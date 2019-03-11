namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotesToSubmission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentSubmissions", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentSubmissions", "Notes");
        }
    }
}
