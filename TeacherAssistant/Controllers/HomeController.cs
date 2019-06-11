using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using System.Web.Security;
using EmailServices.EmailDomain;
using EmailServices.Interfaces;
using TeacherAssistant.Infrastructure;
using TeacherAssistant.Models;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;
using TeachersAssistant.Services.Concretes;
using System.Globalization;
using CuttingEdge.Conditions;

namespace  TeacherAssistant.Controllers
{
    public class HomeController : Controller
    {
        private TeachersAssistantRepositoryServices _teacherRepository;
       /*public HomeController()
        {

        }*/
        public HomeController(ICalendarBookingRepositoryMarker calendarRepositoryMarker,
            IClassroomRepositoryMarker classroomRepositoryMarker,
            IFreeDocumentRepositoryMarker freeDocumentRepositoryMarker,
            IFreeDocumentStudentRepositoryMarker freeDocumentStudentRepositoryMarker,
            IFreeVideoRepositoryMarker freeVideoRepositoryMarker,
            IFreeVideoStudentRepositoryMarker freeVideoStudentRepositoryMarker,
            IPaidDocuemtStudentRepositoryMarker paidDocuemtStudentRepositoryMarker,
            IPaidDocumentRepositoryMarker paidDocumentRepositoryMarker,
            IPaidVideoRepositoryMarker paidVideoRepositoryMarker,
            IPaidVideoStudentRepositoryMarker paidVideoStudentRepositoryMarker,
            IStudentRepositoryMarker studentRepositoryMarker,
            IStudentTypeRepositoryMarker studentTypeRepositoryMarker,
            ISubjectRepositoryMarker subjectRepositoryMarker,
            ITeacherRepositoryMarker teacherRepositoryMarker,
            IBookingTimeRepositoryMarker bookingTimeRepositoryMarker,
            IStudentResourceRepositoryMarker studentResourceRepositoryMarker,
            IQAHelpRequestRepositoryMarker qAHelpRequestRepositoryMarker,
            IAssignmentRepositoryMarker assignmentRepositoryMarker,
            IAssignmentSubmissionRepositoryMarker assignmentSubmissionRepositoryMarker,
            ICourseRepositoryMarker courseRepositoryMarker)
        {

            Condition.Requires<ICalendarBookingRepositoryMarker>(calendarRepositoryMarker, "calendarRepositoryMarker").IsNotNull();
            Condition.Requires<IClassroomRepositoryMarker>(classroomRepositoryMarker, "classroomRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeDocumentRepositoryMarker>(freeDocumentRepositoryMarker, "freeDocumentRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeDocumentStudentRepositoryMarker>(freeDocumentStudentRepositoryMarker, "freeDocumentStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeVideoRepositoryMarker>(freeVideoRepositoryMarker, "freeVideoRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeVideoStudentRepositoryMarker>(freeVideoStudentRepositoryMarker, "freeVideoStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidDocuemtStudentRepositoryMarker>(paidDocuemtStudentRepositoryMarker, "paidDocuemtStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidDocumentRepositoryMarker>(paidDocumentRepositoryMarker, "paidDocumentRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidVideoRepositoryMarker>(paidVideoRepositoryMarker, "paidVideoRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidVideoStudentRepositoryMarker>(paidVideoStudentRepositoryMarker, "paidVideoStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentRepositoryMarker>(studentRepositoryMarker, "studentRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentTypeRepositoryMarker>(studentTypeRepositoryMarker, "studentTypeRepositoryMarker").IsNotNull();
            Condition.Requires<ISubjectRepositoryMarker>(subjectRepositoryMarker, "subjectRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentTypeRepositoryMarker>(studentTypeRepositoryMarker, "teacherRepositoryMarker").IsNotNull();
            Condition.Requires<IBookingTimeRepositoryMarker>(bookingTimeRepositoryMarker, "bookingTimeRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentResourceRepositoryMarker>(studentResourceRepositoryMarker, "studentResourceRepositoryMarker").IsNotNull();
            Condition.Requires<IQAHelpRequestRepositoryMarker>(qAHelpRequestRepositoryMarker, "qaHelpRequestRepositoryMarker").IsNotNull();
            Condition.Requires<IAssignmentRepositoryMarker>(assignmentRepositoryMarker, "assignmentSubmissionRepositoryMarker").IsNotNull();
            Condition.Requires<IAssignmentSubmissionRepositoryMarker>(assignmentSubmissionRepositoryMarker, "assignmentSubmissionRepositoryMarker").IsNotNull();
            Condition.Requires<ICourseRepositoryMarker>(courseRepositoryMarker, "courseRepositoryMarker").IsNotNull();
            var unitOfWork = new TeachersAssistantUnitOfWork(calendarRepositoryMarker,
             classroomRepositoryMarker,
             freeDocumentRepositoryMarker,
             freeDocumentStudentRepositoryMarker,
             freeVideoRepositoryMarker,
             freeVideoStudentRepositoryMarker,
             paidDocuemtStudentRepositoryMarker,
             paidDocumentRepositoryMarker,
             paidVideoRepositoryMarker,
             paidVideoStudentRepositoryMarker,
             studentRepositoryMarker,
             studentTypeRepositoryMarker,
             subjectRepositoryMarker,
             teacherRepositoryMarker,
             bookingTimeRepositoryMarker,
             studentResourceRepositoryMarker,
             qAHelpRequestRepositoryMarker,
             assignmentRepositoryMarker,
             assignmentSubmissionRepositoryMarker,
             courseRepositoryMarker);
            _teacherRepository = new TeachersAssistantRepositoryServices(unitOfWork);
        }
        public enum MediaType { Document, Video }
        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
                return Redirect("~/Administration/TeachersCalendar");
            if (User.IsInRole("StateJunior"))
                return Redirect("~/StateJunior/Home/Index");
            if (User.IsInRole("StatePrimary"))
                return Redirect("~/StatePrimary/Home/Index");
            if (User.IsInRole("Grammar11Plus"))
                return Redirect("~/Grammar11Plus/Home/Index");
            if (User.IsInRole("SecondarySchool"))
                return Redirect("~/SecondarySchool/Home/Index");
            if (User.IsInRole("CollegeAndPostGraduate"))
                return Redirect("~/CollegeAndPostGraduate/Home/Index");
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "About Us.";

            return View();
        }
        
        public ActionResult ContactUs()
        {
            ViewBag.Message = "Contact Us.";

            return View();
        }

        [HttpGet]
        [Authorize(Roles="Administrator,Grammar11Plus,StatePrimary,StateJunior")]
        public ViewResult PublicClass()
        {
            return View("PublicClass");
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Grammar11Plus,StatePrimary,StateJunior")]
        public ViewResult PrivateClass()
        {
            return View("PrivateClass");
        }

        [HttpGet]
        public ActionResult BookTeacherHelpTime()
        {
            ViewBag.Message = "Book Teacher Time.";

            GetUIDropdownLists();

            return View("BookTeacherHelpTime");
        }
        [HttpPost]
        public ActionResult BookTeacherHelpTime(TeacherCalendarViewModel bookingTimeViewModel)
        {
            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                if (bookingTimeViewModel.Delete != null)
                {
                    var teacherCalendar = _teacherRepository.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);
                    _teacherRepository.DeleteTeacherCalendarByBooking(teacherCalendar);
                    return View("_SuccessfullCreation");
                }
                Teacher teacher = _teacherRepository.GetTeacherByName(User.Identity.Name);
                Student student = _teacherRepository.GetStudentByName(bookingTimeViewModel.StudentFullName);
                Subject subject = _teacherRepository.GetSubjectById(bookingTimeViewModel.SubjectId);
                foreach (var bookingTime in bookingTimeViewModel.BookingTimes)
                {
                    _teacherRepository.SaveOrUpdateBooking(teacher, student, subject, new BookingTime { BookingTimeId = bookingTime.BookingTimeId, StartTime = DateTime.Parse(bookingTime.StartTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }), EndTime = DateTime.Parse(bookingTime.EndTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }) }, bookingTimeViewModel.Description);
                }
                var emailService = new EmailServices.EmailService(ConfigurationManager.AppSettings["smtpServer"]);
                
                var emailMessage = new System.Net.Mail.MailMessage();

                var fileInfo = new FileInfo(Server.MapPath("~/Infrastructure/EmailTemplates/TeacherBookingTime.html"));
                var html = fileInfo.OpenText().ReadToEnd();
                html.Replace("{TeacherName}", teacher.EmailAddress);
                html.Replace("{StudentName}", student.EmailAddress);
                html.Replace("{SubjectName}", subject.SubjectName);
                html.Replace("{StartTime}", bookingTimeViewModel.BookingTimes[0].StartTime);
                html.Replace("{EndTime}", bookingTimeViewModel.BookingTimes[0].EndTime);
                emailService.EmailType = EmailType.Html;
                //emailService.SendEmail(new TicketMasterEmailMessage {EmailFrom= student.EmailAddress, EmailMessage = html,EmailTo = new List<string> {student.EmailAddress}, Subject = "Teacher Assistant's Booking Time Schedule"});
                return View("_SuccessfullCreation");
            }
            return View("BookTeacherHelpTime",bookingTimeViewModel);
        }
        [HttpGet]
        public ActionResult TeachersCalendar(string teacherEmail)
        {
            ViewBag.Message = "Teachers Calendar.";
            var teacher = _teacherRepository.GetTeacherByName(teacherEmail);
            var calendar = _teacherRepository.GetTeacherCalendar(teacher.TeacherId ?? 1);
            var teacherCalendarViewModelList = new List<TeacherCalendarViewModel>();

            var bookingTimes = new List<BookingTimeString>();

            if (calendar != null)
            {
                foreach (var cal in calendar)
                {
                    var teacherCalendarViewModel = new TeacherCalendarViewModel();
                    teacherCalendarViewModel.CalendarBookingId = (int)calendar.FirstOrDefault().CalendarBookingId;
                    teacherCalendarViewModel.BookingTimes = bookingTimes.ToArray();
                    teacherCalendarViewModel.ClassId = (int)calendar.FirstOrDefault().ClassId;
                    teacherCalendarViewModel.Description = calendar.FirstOrDefault().Description;
                    teacherCalendarViewModel.TeacherId = calendar.FirstOrDefault().TeacherId;
                    teacherCalendarViewModel.StudentId = calendar.FirstOrDefault().StudentId;
                    teacherCalendarViewModel.SubjectId = calendar.FirstOrDefault().SubjectId;
                    teacherCalendarViewModel.TeacherFullName = calendar.FirstOrDefault().TeacherFullName;
                    teacherCalendarViewModel.StudentFullName = calendar.FirstOrDefault().StudentFullName;
                    teacherCalendarViewModel.StudentTypeId = Int32.Parse(calendar.FirstOrDefault().StudentTypeId);
                    ViewBag.CalendarUiBookingList = calendar.Select(p => p.BookingTime).ToArray();
                    teacherCalendarViewModelList.Add(teacherCalendarViewModel);
                }

            }
            ViewBag.CalendarUiList = teacherCalendarViewModelList.ToArray();
            return View("TeachersCalendar", teacherCalendarViewModelList.Count() > 0 ? teacherCalendarViewModelList.ToArray()[teacherCalendarViewModelList.Count() - 1] : null);

        }
        [HttpGet]
        [Authorize(Roles= "Administrator,Grammar11Plus,StatePrimary,StateJunior,CollegeAndPostGraduate,SecondarySchool")]
        public ActionResult DownloadFreeDocuments()
        {
            ViewBag.Message = "Download Free Documents.";
            var role = Roles.GetRolesForUser().FirstOrDefault();
            if (role.ToLower().Equals("administrator"))
            {
                role = Session["Role"] as string;
                if (role == null) role = string.Empty;
            }
            var freeDocsUrl = string.Empty;

            if (!string.IsNullOrEmpty(role))
            {
                switch (role)
                {
                    case "CollegeAndPostGraduate":
                        freeDocsUrl = Url.Content("~/CollegeAndPostGraduate/Home/DownloadFreeDocuments");
                        var routeVals = freeDocsUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        return Redirect(freeDocsUrl);
                    case "SecondarySchool":
                        freeDocsUrl = Url.Content("~/SecondarySchool/Home/DownloadFreeDocuments");
                        routeVals = freeDocsUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        return Redirect(freeDocsUrl);
                    case "Grammar11Plus":
                        freeDocsUrl = Url.Content("~/Grammar11Plus/Home/DownloadFreeDocuments");
                        routeVals = freeDocsUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        return Redirect(freeDocsUrl);
                    case "StatePrimary":
                        freeDocsUrl = Url.Content("~/StatePrimary/Home/DownloadFreeDocuments");
                        routeVals = freeDocsUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        return Redirect(freeDocsUrl);
                    case "StateJunior":
                        freeDocsUrl = Url.Content("~/StateJunior/Home/DownloadFreeDocuments");
                        routeVals = freeDocsUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        return Redirect(freeDocsUrl);
                    default:
                        return Redirect("~/Home/Index");
                }
            }
            return Redirect("~/Home/Index");
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Grammar11Plus,StatePrimary,StateJunior")]
        public ActionResult DownloadPaidDocuments()
        {
            ViewBag.Message = "Download Paid Documents.";
            
            var paidDocsUrl = string.Empty;

            var boughtItems = _teacherRepository.GetUserBoughtItems(User.Identity.Name);

            ViewBag.PaidDocumentsList =  boughtItems;
            return View("DownloadPaidDocuments");
        }

        [HttpPost]
        public  FileResult HandlePaidDocumentDownload(string virtualPath)
        {
            var username = ConfigurationManager.AppSettings["ftpServerUser"];
            var password = ConfigurationManager.AppSettings["ftpServerPassword"];
            var ftpServer = ConfigurationManager.AppSettings["ftpServer"];

            var ftpUtility = new FTPDownloadUtility(username, password, ftpServer);
            try
            {
                Stream stream = null;
                ftpUtility.DownloadFile(ref virtualPath,out stream);

                var bytes = new byte[2048];
                var bytesRead = 0;
                Response.Headers.Add("Content-Type", "application/octate-stream");
                Response.Headers.Add("Content-Disposition", string.Format("attachment;filename={0}", virtualPath.Substring(virtualPath.LastIndexOf("/")+1)));
                
                var fileResult = File(Response.OutputStream, "application/zip");
                while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    Response.OutputStream.Write(bytes, 0, bytesRead);
                }
                Response.OutputStream.Seek(0, SeekOrigin.Begin);
                
                return fileResult;
            }
            catch (Exception e)
            {
                return File(System.Text.ASCIIEncoding.UTF8.GetBytes(virtualPath+",error: "+e.Message + "\r\n" + e.StackTrace),"application/text");
            }
        }
        private List<SelectListItem> GetStudentsList()
        {
            var students = _teacherRepository.GetStudentList();
            var studentList = new List<SelectListItem>();

            foreach (var student in students)
            {
                studentList.Add(new SelectListItem { Text = student.EmailAddress, Value = student.StudentId.ToString() });
            }

            return studentList;
        }

        private List<SelectListItem> GetSubjectList()
        {
            var subjects = _teacherRepository.GetSubjectList();
            var subjectList = new List<SelectListItem>();

            foreach (var subject in subjects)
            {
                subjectList.Add(new SelectListItem { Text = subject.SubjectName, Value = subject.SubjectId.ToString() });
            }

            return subjectList;
        }
        private List<SelectListItem> GetTeacherList()
        {
            var teachers = _teacherRepository.GetTeacherList();
            var teacherList = new List<SelectListItem>();

            foreach (var teacher in teachers)
            {
                teacherList.Add(new SelectListItem { Text = teacher.EmailAddress, Value = teacher.TeacherId.ToString() });
            }

            return teacherList;
        }
                private List<SelectListItem> GetRolesSelectList()
        {
            var rolesList = new List<SelectListItem>();

            foreach (var role in Roles.GetAllRoles())
            {
                rolesList.Add(new SelectListItem { Text = role, Value = role });
            }

            return rolesList;
        }
        public IList<SelectListItem> GetQARequestList()
        {
            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "Pick Product Item", Value = 0.ToString() });

            productList.AddRange(_teacherRepository.GetQARequestList().Select(p => new SelectListItem { Text = p.Description.Substring(0, 20), Value = p.QAHelpRequestId.ToString() }).ToList());
            return productList;
        }
        private void GetUIDropdownLists()
        {
            ViewBag.QAHelpRequestList = GetQARequestList();
            ViewBag.TeacherList = GetTeacherList();
            ViewBag.RoleList = GetRolesSelectList();
            ViewBag.StudentList = GetStudentsList();
            ViewBag.SubjectList = GetSubjectList();
        }
    }

}