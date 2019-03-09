namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssignmentAndSubmissions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        StudentRole = c.String(),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        AssignmentName = c.String(),
                        Description = c.String(),
                        DateAssigned = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.AssignmentSubmissions",
                c => new
                    {
                        AssignmentSubmissionId = c.Int(nullable: false, identity: true),
                        AssignmentId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                        FilePath = c.String(),
                        StudentRole = c.String(),
                    })
                .PrimaryKey(t => t.AssignmentSubmissionId)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .Index(t => t.AssignmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignmentSubmissions", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.Assignments", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.Students");
            DropIndex("dbo.AssignmentSubmissions", new[] { "AssignmentId" });
            DropIndex("dbo.Assignments", new[] { "SubjectId" });
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            DropTable("dbo.AssignmentSubmissions");
            DropTable("dbo.Assignments");
        }
    }
}
