namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
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
                        TeacherId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        AssignmentName = c.String(),
                        Description = c.String(),
                        DateAssigned = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        FilePath = c.String(),
                        IsSubmitted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentFirsName = c.String(),
                        StudentLastName = c.String(),
                        StudentType = c.Int(nullable: false),
                        EmailAddress = c.String(),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        CourseDescription = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.AssignmentSubmissions",
                c => new
                    {
                        AssignmentSubmissionId = c.Int(nullable: false, identity: true),
                        AssignmentId = c.Int(nullable: false),
                        AssignmentName = c.String(),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                        FilePath = c.String(),
                        StudentRole = c.String(),
                        IsSubmitted = c.Boolean(nullable: false),
                        Grade = c.String(),
                        GradeNumeric = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.AssignmentSubmissionId)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.AssignmentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.BookingTimes",
                c => new
                    {
                        BookingTimeId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookingTimeId);
            
            CreateTable(
                "dbo.CalendarBookings",
                c => new
                    {
                        CalendarBookingId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        StudentTypeId = c.String(),
                        StudentId = c.Int(nullable: false),
                        BookingTimeId = c.Int(nullable: false),
                        TeacherFullName = c.String(),
                        StudentFullName = c.String(),
                        Description = c.String(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CalendarBookingId)
                .ForeignKey("dbo.BookingTimes", t => t.BookingTimeId, cascadeDelete: true)
                .Index(t => t.BookingTimeId);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        ClassroomId = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        CalendarId = c.Int(nullable: false),
                        StudentType = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassroomId)
                .ForeignKey("dbo.CalendarBookings", t => t.CalendarId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.CalendarId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        FirsName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            CreateTable(
                "dbo.FreeDocuments",
                c => new
                    {
                        FreeDocumentId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FreeDocumentId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.FreeDocumentStudents",
                c => new
                    {
                        FreeDocumentStudentId = c.Int(nullable: false, identity: true),
                        FreeDocumentId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        StudentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FreeDocumentStudentId)
                .ForeignKey("dbo.FreeDocuments", t => t.FreeDocumentId, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.FreeDocumentId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.FreeVideos",
                c => new
                    {
                        FreeVideoId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FreeVideoId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.FreeVideoStudents",
                c => new
                    {
                        FreeVideoStudentId = c.Int(nullable: false, identity: true),
                        FreeVideoId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        StudentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FreeVideoStudentId)
                .ForeignKey("dbo.FreeVideos", t => t.FreeVideoId, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.FreeVideoId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.ItemOrders",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        order_id_fk = c.Int(),
                        numberOrdered = c.Int(),
                        product_name = c.String(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Orders", t => t.order_id_fk)
                .Index(t => t.order_id_fk);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderId = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        status = c.String(),
                        order_date = c.DateTime(),
                        order_gross = c.Decimal(precision: 18, scale: 2),
                        email = c.String(),
                        paid_for = c.Boolean(),
                    })
                .PrimaryKey(t => t.orderId);
            
            CreateTable(
                "dbo.PaidDocuments",
                c => new
                    {
                        PaidDocumentId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PaidDocumentId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.PaidDocumentStudents",
                c => new
                    {
                        PaidDocumentStudentId = c.Int(nullable: false, identity: true),
                        PaidDocumentId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        StudentType = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaidDocumentStudentId)
                .ForeignKey("dbo.PaidDocuments", t => t.PaidDocumentId, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.PaidDocumentId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.PaidVideos",
                c => new
                    {
                        PaidVideoId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PaidVideoId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.PaidVideoStudents",
                c => new
                    {
                        PaidVideoStudentId = c.Int(nullable: false, identity: true),
                        PaidVideoId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        StudentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaidVideoStudentId)
                .ForeignKey("dbo.PaidVideos", t => t.PaidVideoId, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.PaidVideoId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
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
            
            CreateTable(
                "dbo.SHOP_PRODS",
                c => new
                    {
                        prodId = c.Int(nullable: false, identity: true),
                        prodCode = c.String(),
                        prodName = c.String(),
                        prodPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        prodDesc = c.String(),
                        IsPaidDocument = c.Boolean(nullable: false),
                        IsPaidVideo = c.Boolean(nullable: false),
                        DocumentId = c.Int(nullable: false),
                        rating = c.Int(),
                        numberOfVoters = c.Int(),
                    })
                .PrimaryKey(t => t.prodId);
            
            CreateTable(
                "dbo.StudentResources",
                c => new
                    {
                        StudentResourceId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        SubjectId = c.Int(nullable: false),
                        RoleName = c.String(),
                        CourseId = c.Int(nullable: false),
                        StudentResourceName = c.String(),
                    })
                .PrimaryKey(t => t.StudentResourceId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.StudentTypes",
                c => new
                    {
                        StudentTypeId = c.Int(nullable: false, identity: true),
                        StudentTypeName = c.String(),
                    })
                .PrimaryKey(t => t.StudentTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentResources", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.PaidVideoStudents", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.PaidVideoStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.PaidVideoStudents", "PaidVideoId", "dbo.PaidVideos");
            DropForeignKey("dbo.PaidVideos", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.PaidDocumentStudents", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.PaidDocumentStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.PaidDocumentStudents", "PaidDocumentId", "dbo.PaidDocuments");
            DropForeignKey("dbo.PaidDocuments", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ItemOrders", "order_id_fk", "dbo.Orders");
            DropForeignKey("dbo.FreeVideoStudents", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.FreeVideoStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.FreeVideoStudents", "FreeVideoId", "dbo.FreeVideos");
            DropForeignKey("dbo.FreeVideos", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.FreeDocumentStudents", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.FreeDocumentStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.FreeDocumentStudents", "FreeDocumentId", "dbo.FreeDocuments");
            DropForeignKey("dbo.FreeDocuments", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Classrooms", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Classrooms", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Subjects", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Classrooms", "CalendarId", "dbo.CalendarBookings");
            DropForeignKey("dbo.CalendarBookings", "BookingTimeId", "dbo.BookingTimes");
            DropForeignKey("dbo.AssignmentSubmissions", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AssignmentSubmissions", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentResources", new[] { "SubjectId" });
            DropIndex("dbo.PaidVideoStudents", new[] { "SubjectId" });
            DropIndex("dbo.PaidVideoStudents", new[] { "StudentId" });
            DropIndex("dbo.PaidVideoStudents", new[] { "PaidVideoId" });
            DropIndex("dbo.PaidVideos", new[] { "SubjectId" });
            DropIndex("dbo.PaidDocumentStudents", new[] { "SubjectId" });
            DropIndex("dbo.PaidDocumentStudents", new[] { "StudentId" });
            DropIndex("dbo.PaidDocumentStudents", new[] { "PaidDocumentId" });
            DropIndex("dbo.PaidDocuments", new[] { "SubjectId" });
            DropIndex("dbo.ItemOrders", new[] { "order_id_fk" });
            DropIndex("dbo.FreeVideoStudents", new[] { "SubjectId" });
            DropIndex("dbo.FreeVideoStudents", new[] { "StudentId" });
            DropIndex("dbo.FreeVideoStudents", new[] { "FreeVideoId" });
            DropIndex("dbo.FreeVideos", new[] { "SubjectId" });
            DropIndex("dbo.FreeDocumentStudents", new[] { "SubjectId" });
            DropIndex("dbo.FreeDocumentStudents", new[] { "StudentId" });
            DropIndex("dbo.FreeDocumentStudents", new[] { "FreeDocumentId" });
            DropIndex("dbo.FreeDocuments", new[] { "SubjectId" });
            DropIndex("dbo.Subjects", new[] { "CourseId" });
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            DropIndex("dbo.Classrooms", new[] { "SubjectId" });
            DropIndex("dbo.Classrooms", new[] { "CalendarId" });
            DropIndex("dbo.Classrooms", new[] { "TeacherId" });
            DropIndex("dbo.CalendarBookings", new[] { "BookingTimeId" });
            DropIndex("dbo.AssignmentSubmissions", new[] { "StudentId" });
            DropIndex("dbo.AssignmentSubmissions", new[] { "AssignmentId" });
            DropIndex("dbo.Students", new[] { "CourseId" });
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            DropTable("dbo.StudentTypes");
            DropTable("dbo.StudentResources");
            DropTable("dbo.SHOP_PRODS");
            DropTable("dbo.QAHelpRequests");
            DropTable("dbo.PaidVideoStudents");
            DropTable("dbo.PaidVideos");
            DropTable("dbo.PaidDocumentStudents");
            DropTable("dbo.PaidDocuments");
            DropTable("dbo.Orders");
            DropTable("dbo.ItemOrders");
            DropTable("dbo.FreeVideoStudents");
            DropTable("dbo.FreeVideos");
            DropTable("dbo.FreeDocumentStudents");
            DropTable("dbo.FreeDocuments");
            DropTable("dbo.Teachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.Classrooms");
            DropTable("dbo.CalendarBookings");
            DropTable("dbo.BookingTimes");
            DropTable("dbo.AssignmentSubmissions");
            DropTable("dbo.Courses");
            DropTable("dbo.Students");
            DropTable("dbo.Assignments");
        }
    }
}
