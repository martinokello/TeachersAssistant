namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludeGrades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentSubmissions", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentSubmissions", "Grade");
        }
    }
}
