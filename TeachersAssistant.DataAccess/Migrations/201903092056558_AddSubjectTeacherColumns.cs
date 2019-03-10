namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubjectTeacherColumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.Assignments", new[] { "SubjectId" });
            AddColumn("dbo.Assignments", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.AssignmentSubmissions", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.AssignmentSubmissions", "SubjectId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentSubmissions", "SubjectId");
            DropColumn("dbo.AssignmentSubmissions", "TeacherId");
            DropColumn("dbo.Assignments", "TeacherId");
            CreateIndex("dbo.Assignments", "SubjectId");
            AddForeignKey("dbo.Assignments", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
        }
    }
}
