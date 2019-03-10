namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRElationAssingmentSubject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "SubjectId", "dbo.Subjects");
            AddForeignKey("dbo.Assignments", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "SubjectId", "dbo.Subjects");
            AddForeignKey("dbo.Assignments", "SubjectId", "dbo.Subjects", "SubjectId");
        }
    }
}
