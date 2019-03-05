using System.Data.Entity;
using System.Reflection.Emit;
using System.Web.Security;
using TeachersAssistant.Domain.Model.Models;

namespace TeachersAssistant.DataAccess
{

    public class TeachersAssistantDbContext : DbContext
    {
        // Your context has been configured to use a TeacherAssistant connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // TeacherAssistant.DataAccess.TeachersAssistant database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TeachersAssistant; ' 
        // connection string in the application configuration file.
        public TeachersAssistantDbContext()
            : base("name=TeachersAssistant")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TeachersAssistantDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<TeachersAssistant>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
             .HasRequired(p => p.Teacher)
             .WithMany()
             .HasForeignKey(p => p.TeacherId)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Classroom>()
             .HasRequired(p => p.Subject)
             .WithMany()
             .HasForeignKey(p => p.SubjectId)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Classroom>()
             .HasRequired(p => p.Teacher)
             .WithMany()
             .HasForeignKey(p => p.TeacherId)
             .WillCascadeOnDelete(false);
            modelBuilder.Entity<FreeDocument>()
             .HasRequired(p => p.Subject)
             .WithMany()
             .HasForeignKey(p => p.SubjectId)
             .WillCascadeOnDelete(false);
            modelBuilder.Entity<PaidDocument>()
             .HasRequired(p => p.Subject)
             .WithMany()
             .HasForeignKey(p => p.SubjectId)
             .WillCascadeOnDelete(false);
            modelBuilder.Entity<FreeVideo>()
             .HasRequired(p => p.Subject)
             .WithMany()
             .HasForeignKey(p => p.SubjectId)
             .WillCascadeOnDelete(false);
            modelBuilder.Entity<PaidVideo>()
             .HasRequired(p => p.Subject)
             .WithMany()
             .HasForeignKey(p => p.SubjectId)
             .WillCascadeOnDelete(false);
            modelBuilder.Entity<StudentResource>()
             .HasRequired(p => p.Subject)
             .WithMany()
             .HasForeignKey(p => p.SubjectId)
             .WillCascadeOnDelete(false);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Teacher> Teachers { get; set; }
        public  DbSet<CalendarBooking> CalendarBookings { get; set; }
        public  DbSet<Classroom> Classrooms { get; set; }
        public  DbSet<FreeDocument> FreeDocuments { get; set; }
        public  DbSet<PaidDocument> PaidDocuments { get; set; }
        public  DbSet<FreeVideo> FreeVideos { get; set; }
        public  DbSet<PaidVideo> PaidVideos { get; set; }
        public  DbSet<Student> Students { get; set; }
        public  DbSet<FreeDocumentStudent> FreeDocumentStudents { get; set; }
        public  DbSet<PaidDocumentStudent> PaidDocumentStudents { get; set; }
        public  DbSet<FreeVideoStudent> FreeVideoStudents { get; set; }
        public  DbSet<PaidVideoStudent> PaidVideoStudents { get; set; }
        public  DbSet<Subject> Subjects { get; set; }
        public  DbSet<StudentType> StudentTypes { get; set; }
        public DbSet<BookingTime> BookingTimes { get; set; }
        public DbSet<SHOP_PRODS> ShopProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }
        public DbSet<StudentResource> StudentResources { get; set; }
        

    }
    
}