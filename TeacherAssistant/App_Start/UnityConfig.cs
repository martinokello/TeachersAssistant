using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.DataAccess.Concretes;
using TeachersAssistant.Services.Interfaces;
using TeachersAssistant.Services.Concretes;
using System.Reflection;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using TeacherAssistant;
using Microsoft.AspNet.Identity.EntityFramework;
using TeacherAssistant.Models;
using System.Data.Entity;
using System.Web;
using EmailServices.Interfaces;
using Microsoft.Owin.Security;

namespace TeachersAssistant
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IEmailService, EmailServices.EmailService>();
            unityContainer.RegisterType<ICalendarBookingRepositoryMarker, CalendarBookingRepository>();
            unityContainer.RegisterType<IClassroomRepositoryMarker, ClassroomRepository>();
            unityContainer.RegisterType<IFreeDocumentRepositoryMarker, FreeDocumentRepository>();
            unityContainer.RegisterType<IFreeDocumentStudentRepositoryMarker, FreeDocumentStudentRepository>();
            unityContainer.RegisterType<IFreeVideoRepositoryMarker, FreeVideoRepository>();
            unityContainer.RegisterType<IFreeVideoStudentRepositoryMarker, FreeVideoStudentRepository>();
            unityContainer.RegisterType<IPaidDocuemtStudentRepositoryMarker, PaidDocuemtStudentRepository>();
            unityContainer.RegisterType<IPaidDocumentRepositoryMarker, PaidDocumentRepository>();
            unityContainer.RegisterType<IPaidVideoRepositoryMarker, PaidVideoRepository>();
            unityContainer.RegisterType<IPaidVideoStudentRepositoryMarker, PaidVideoStudentRepository>();
            unityContainer.RegisterType<IStudentRepositoryMarker, StudentRepository>();
            unityContainer.RegisterType<IStudentTypeRepositoryMarker, StudentTypeRepository>();
            unityContainer.RegisterType<ISubjectRepositoryMarker, SubjectRepository>();
            unityContainer.RegisterType<ITeacherRepositoryMarker, TeacherRepository>();
            unityContainer.RegisterType<IBookingTimeRepositoryMarker, BookingTimeRepository>();
            unityContainer.RegisterType<IStudentResourceRepositoryMarker, StudentResourcesRepository>();
            unityContainer.RegisterType<IQAHelpRequestRepositoryMarker, QAHelpRequestRepository>();
            unityContainer.RegisterType<IAssignmentRepositoryMarker, AssignmentRepository>();
            unityContainer.RegisterType<IAssignmentSubmissionRepositoryMarker, AssignmentSubmissionRepository>();
            unityContainer.RegisterType<IAdhocPatchAndReportingMarker, AdhocPatchAndReportingRepository>(); 
            unityContainer.RegisterType<ICourseRepositoryMarker, CourseRepository>();

            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(); 
            unityContainer.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>();
            unityContainer.RegisterType<ApplicationUserManager>();
            unityContainer.RegisterType<ApplicationUser>();
            unityContainer.RegisterType<ApplicationSignInManager>();
            unityContainer.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IAuthenticationManager>(
                new InjectionFactory(
                    o => System.Web.HttpContext.Current.GetOwinContext().Authentication
                )
);
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                         where typeof(IHttpController).IsAssignableFrom(t) || typeof(IController).IsAssignableFrom(t)
                        select t;

             foreach (var t in types)
             {
                 unityContainer.RegisterInstance(t);
             }

            DependencyResolver.SetResolver(new UnityDependencyResolver(unityContainer));
        }
    }
}