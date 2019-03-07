namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableQA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QAHelpRequests",
                c => new
                    {
                        QAHelpRequestId = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        StudentRole = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsScheduled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QAHelpRequestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QAHelpRequests");
        }
    }
}
