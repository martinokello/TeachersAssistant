using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TeacherAssistant.Infrastructure;
using TeacherAssistant.Models;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;
using TeachersAssistant.Models;
using TeachersAssistant.Services.Concretes;

namespace  TeacherAssistant.Controllers
{
    public class HomeController : Controller
    {
        private TeachersAssistantRepositoryServices _teacherRepository;
        public HomeController()
        {

        }
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
            IBookingTimeRepositoryMarker bookingTimeRepositoryMarker)
        {
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
             bookingTimeRepositoryMarker);
            _teacherRepository = new TeachersAssistantRepositoryServices(unitOfWork);
            _teacherRepository.GetSubjectList();
        }
        public enum MediaType { Document, Video }
        public ActionResult Index()
        {
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
        public ActionResult OnlineClassRoom()
        {
            ViewBag.Message = "Online Classroom.";

            return View();
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
                    return View("SuccssessfullCreation");
                }
                Teacher teacher = _teacherRepository.GetTeacherByName(User.Identity.Name);
                Student student = _teacherRepository.GetStudentByName(bookingTimeViewModel.StudentFullName);
                Subject subject = _teacherRepository.GetSubjectById(bookingTimeViewModel.SubjectId);
                foreach (var bookingTime in bookingTimeViewModel.BookingTimes)
                {
                    _teacherRepository.SaveOrUpdateBooking(teacher, student, subject, bookingTime, bookingTimeViewModel.Description);
                }
                return View("SucssessfullCreation");
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

            var bookingTimes = new List<BookingTime>();

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
        [Authorize(Roles="Administrator,Grammar11Plus,StatePrimary,StateJunior")]
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
                    case "Grammar11Plus":
                        freeDocsUrl = Url.Content("~/Grammar11Plus/Home/DownloadFreeDocuments");
                        var routeVals = freeDocsUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
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

        private void GetUIDropdownLists()
        {
            ViewBag.TeacherList = GetTeacherList();
            ViewBag.RoleList = GetRolesSelectList();
            ViewBag.StudentList = GetStudentsList();
            ViewBag.SubjectList = GetSubjectList();
        }
    }

}