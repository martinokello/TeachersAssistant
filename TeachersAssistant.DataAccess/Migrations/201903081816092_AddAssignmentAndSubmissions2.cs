namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssignmentAndSubmissions2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.Students");
            AddColumn("dbo.Assignments", "IsSubmitted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AssignmentSubmissions", "IsSubmitted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.AssignmentSubmissions", "StudentId");
            AddForeignKey("dbo.AssignmentSubmissions", "StudentId", "dbo.Students", "StudentId");
            AddForeignKey("dbo.Assignments", "StudentId", "dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AssignmentSubmissions", "StudentId", "dbo.Students");
            DropIndex("dbo.AssignmentSubmissions", new[] { "StudentId" });
            DropColumn("dbo.AssignmentSubmissions", "IsSubmitted");
            DropColumn("dbo.Assignments", "IsSubmitted");
            AddForeignKey("dbo.Assignments", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
