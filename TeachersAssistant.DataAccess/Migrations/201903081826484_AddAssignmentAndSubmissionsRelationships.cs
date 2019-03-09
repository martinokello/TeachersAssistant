namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssignmentAndSubmissionsRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignmentSubmissions", "AssignmentId", "dbo.Assignments");
            AddForeignKey("dbo.AssignmentSubmissions", "AssignmentId", "dbo.Assignments", "AssignmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignmentSubmissions", "AssignmentId", "dbo.Assignments");
            AddForeignKey("dbo.AssignmentSubmissions", "AssignmentId", "dbo.Assignments", "AssignmentId", cascadeDelete: true);
        }
    }
}
