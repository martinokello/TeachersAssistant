﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EmailServices.Interfaces;
using TeacherAssistant.Models;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;
using TeachersAssistant.Services.Concretes;
using System.Globalization;
using TeachersAssistant.DataAccess;
using System.Text;
using EmailServices.EmailDomain;

namespace TeacherAssistant.Areas.SecondarySchool.Controllers
{

    [Authorize(Roles = "Administrator,SecondarySchool")]
    public class HomeController : Controller
    {
        private TeachersAssistantRepositoryServices _teacherRepository;
        private IEmailService _emailService;
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
            IStudentResourceRepositoryMarker studentResourcesRepositoryMarker,
            IQAHelpRequestRepositoryMarker qAHelpRequestRepositoryMarker,
            IAssignmentRepositoryMarker assignmentRepositoryMarker,
            IAssignmentSubmissionRepositoryMarker assignmentSubmissionRepositoryMarker,
            ICourseRepositoryMarker courseRepositoryMarker,
            IEmailService mailService)
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
             bookingTimeRepositoryMarker,
             studentResourcesRepositoryMarker,
             qAHelpRequestRepositoryMarker,
             assignmentRepositoryMarker,
             assignmentSubmissionRepositoryMarker,
             courseRepositoryMarker);
            unitOfWork.InitializeDbContext(new TeachersAssistantDbContext());
            _emailService = mailService;
            _teacherRepository = new TeachersAssistantRepositoryServices(unitOfWork);
        }
        private List<SelectListItem> GetCurrentAssignmentList(string roleName)
        {
            var assingnmentList = new List<SelectListItem>();
            assingnmentList.Add(new SelectListItem { Text = "Pick Assignment", Value = 0.ToString() });

            assingnmentList.AddRange(_teacherRepository.GetCurrentAssignments(roleName).Select(p => new SelectListItem { Text = p.AssignmentName, Value = p.AssignmentId.ToString() }).ToList());
            return assingnmentList;
        }

        public enum MediaType { Document, Video }
        [HttpGet]
        public ViewResult Index()
        {
            return View("Index");
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
        public ActionResult RequestQAHelp()
        {
            ViewBag.Message = "Request QA Help From Tutor";
            GetUIDropdownLists();
            return View(new QAHelpRequestViewModel());
        }
        [HttpPost]
        public ActionResult RequestQAHelp(QAHelpRequestViewModel qaHelpRequest)
        {
            ViewBag.Message = "Request QA Help From Tutor";
            GetUIDropdownLists();

            if (ModelState.IsValid)
            {
                _teacherRepository.RequestQATime(new QAHelpRequest
                {
                    QAHelpRequestId = qaHelpRequest.QAHelpRequestId,
                    TeacherId = qaHelpRequest.TeacherId,
                    Description = qaHelpRequest.Description,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    StudentId = qaHelpRequest.StudentId,
                    StudentRole = qaHelpRequest.StudentRole,
                    SubjectId = qaHelpRequest.SubjectId,
                    DateCreated = DateTime.Now
                }); var student = _teacherRepository.GetStudentById(qaHelpRequest.StudentId);
                var teacher = _teacherRepository.GetTeacherById(qaHelpRequest.TeacherId);
                var subject = _teacherRepository.GetSubjectById(qaHelpRequest.SubjectId);
                _emailService.SendEmail(new TicketMasterEmailMessage { EmailFrom = ConfigurationManager.AppSettings["BusinessEmail"], Subject = $"Request Help from {student.StudentLastName } in subject: { subject.SubjectName } and Role {qaHelpRequest.StudentRole}", EmailTo = new List<string> { teacher.EmailAddress, student.EmailAddress }, EmailMessage = $"Request Help from {student.StudentLastName } in subject: { subject.SubjectName } and Role {qaHelpRequest.StudentRole} with the suggested Student request Help times with Start Time at: {qaHelpRequest.StartTime.ToString("dd-MM-yyyy HH:mm")} and End Time {qaHelpRequest.EndTime.ToString("dd-MM-yyyy HH:mm")}. Please await the teacher to confirm by email whether this will be a good time for them. \r\nKindest Regards\r\nMartinLayooInc Team." });
                return View("_SuccessfullCreation", qaHelpRequest);
            }
            return View();
        }
        [HttpGet]
        public ActionResult OnlineClassRoom()
        {
            ViewBag.Message = "Online Classroom.";

            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Grammar11Plus,StatePrimary,StateJunior,SecondarySchool,CollegeAndPostGraduate")]
        public ViewResult PrivateClass()
        {
            return View("PrivateClass");
        }
        [HttpGet]
        public ActionResult BookTeacherHelpTime()
        {
            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();

            var calendars = _teacherRepository.GetTeacherCalendar();
            var calendarBookingViewModels = new List<ICalendarBookingViewModel>();
            var classRooms = _teacherRepository.GetClassrooms();
            if (calendars != null)
            {
                var calendarsLeftJoin = from cal in calendars
                                        join cls in classRooms on
                                        cal.CalendarBookingId equals cls.CalendarId into res
                                        from q in res.DefaultIfEmpty()
                                        select new
                                        {
                                            ClassroomId = q == null ? null : q.ClassroomId,
                                            SubjectId = cal.SubjectId,
                                            TeacherId = cal.TeacherId,
                                            StudentId = cal.StudentId,
                                            BookingTimeId = cal.BookingTimeId
                                        };


                foreach (var cal in calendarsLeftJoin)
                {
                    var student = _teacherRepository.GetStudentById(cal.StudentId);
                    var subject = _teacherRepository.GetSubjectById(cal.SubjectId);
                    var teacher = _teacherRepository.GetTeacherById(cal.TeacherId);
                    var bookingTime = _teacherRepository.GetBookingById(cal.BookingTimeId);

                    if (bookingTime == null) continue;
                    calendarBookingViewModels.Add(new CalendarBookingSelectOrDeleteViewModel { ClassId = cal.ClassroomId, Teacher = teacher, Subject = subject, Student = student, BookingTime = bookingTime });
                }

            }
            ViewBag.CalendarUiList = calendarBookingViewModels.ToArray();


            return View("BookTeacherHelpTime");
        }
        [HttpPost]
        public ActionResult BookTeacherHelpTimeCreate(TeacherCalendarCreateViewModel bookingTimeViewModel)
        {
            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();

            var viewResult = SetCalendarValues(bookingTimeViewModel);
            if (ModelState.IsValid)
            {
                Teacher teacher = _teacherRepository.GetTeacherById(bookingTimeViewModel.TeacherId);
                Student student = _teacherRepository.GetStudentByName(User.Identity.Name);
                Subject subject = _teacherRepository.GetSubjectById(bookingTimeViewModel.SubjectId);
                foreach (var bookingTime in bookingTimeViewModel.BookingTimes)
                {
                    _teacherRepository.SaveOrUpdateBooking(teacher, student, subject, new BookingTime { BookingTimeId = bookingTime.BookingTimeId, StartTime = DateTime.Parse(bookingTime.StartTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }), EndTime = DateTime.Parse(bookingTime.EndTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }) },
                        bookingTimeViewModel.Description);
                }
                var emailService = new EmailServices.EmailService(ConfigurationManager.AppSettings["smtpServer"], ConfigurationManager.AppSettings["smtpServerUser"], ConfigurationManager.AppSettings["smtpServerPassword"]);

                var emailMessage = new System.Net.Mail.MailMessage();

                var fileInfo = new FileInfo(Server.MapPath("~/Infrastructure/MailTemplates/TeacherBookingTime.html"));
                var html = fileInfo.OpenText().ReadToEnd();
                html.Replace("{TeacherName}", teacher.EmailAddress);
                html.Replace("{StudentName}", student.EmailAddress);
                html.Replace("{SubjectName}", subject.SubjectName);
                html.Replace("{StartTime}", bookingTimeViewModel.BookingTimes[0].StartTime);
                html.Replace("{EndTime}", bookingTimeViewModel.BookingTimes[0].EndTime);
                emailService.EmailType = EmailType.Html;
                //emailService.SendEmail(new TicketMasterEmailMessage {EmailFrom= student.EmailAddress, EmailMessage = html,EmailTo = , Subject = "Teacher Assistant's Booking Time Schedule"});
                var message = new TicketMasterEmailMessage { EmailFrom = ConfigurationManager.AppSettings["BusinessEmail"], EmailTo = new List<string> { student.EmailAddress }, Subject = "Teacher Assistant's Booking Time Schedule", EmailMessage = html };
                emailService.SendEmail(message);
                return View("SuccessfullCreation");
            }
            return viewResult;
        }
        [HttpPost]
        public ActionResult BookTeacherHelpTimeUpdate(TeacherCalendarUpdateViewModel bookingTimeViewModel)
        {
            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();

            var viewResult = SetCalendarValues(bookingTimeViewModel);
            if (ModelState.IsValid)
            {
                Teacher teacher = _teacherRepository.GetTeacherById(bookingTimeViewModel.TeacherId);
                Student student = _teacherRepository.GetStudentByName(User.Identity.Name);
                Subject subject = _teacherRepository.GetSubjectById(bookingTimeViewModel.SubjectId);
                foreach (var bookingTime in bookingTimeViewModel.BookingTimes)
                {
                    _teacherRepository.SaveOrUpdateBooking(teacher, student, subject, new BookingTime { BookingTimeId = bookingTime.BookingTimeId, StartTime = DateTime.Parse(bookingTime.StartTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }), EndTime = DateTime.Parse(bookingTime.EndTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }) },
                        bookingTimeViewModel.Description);
                }
                var emailService = new EmailServices.EmailService(ConfigurationManager.AppSettings["smtpServer"], ConfigurationManager.AppSettings["smtpServerUser"], ConfigurationManager.AppSettings["smtpServerPassword"]);

                var emailMessage = new System.Net.Mail.MailMessage();

                var fileInfo = new FileInfo(Server.MapPath("~/Infrastructure/MailTemplates/TeacherBookingTime.html"));
                var html = fileInfo.OpenText().ReadToEnd();
                html.Replace("{TeacherName}", teacher.EmailAddress);
                html.Replace("{StudentName}", student.EmailAddress);
                html.Replace("{SubjectName}", subject.SubjectName);
                html.Replace("{StartTime}", bookingTimeViewModel.BookingTimes[0].StartTime);
                html.Replace("{EndTime}", bookingTimeViewModel.BookingTimes[0].EndTime);
                emailService.EmailType = EmailType.Html;
                //emailService.SendEmail(new TicketMasterEmailMessage {EmailFrom= student.EmailAddress, EmailMessage = html,EmailTo = , Subject = "Teacher Assistant's Booking Time Schedule"});
                var message = new TicketMasterEmailMessage { EmailFrom = ConfigurationManager.AppSettings["BusinessEmail"], EmailTo = new List<string> { student.EmailAddress }, Subject = "Teacher Assistant's Booking Time Schedule", EmailMessage = html };
                emailService.SendEmail(message);
                return View("SuccessfullCreation");
            }
            return viewResult;
        }
        [HttpPost]
        public ActionResult BookTeacherHelpTimeSelectOrDelete(TeacherCalendarSelectOrDeleteViewModel bookingTimeViewModel)
        {
            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();

            var viewResult = SetCalendarValues(bookingTimeViewModel);

            if (ModelState.IsValid)
            {
                var setModel = bookingTimeViewModel as TeacherCalendarSelectOrDeleteViewModel;
                if (!string.IsNullOrEmpty(bookingTimeViewModel.Select))
                {
                    return SetCalendarValues(setModel);
                }
                else if (!string.IsNullOrEmpty(bookingTimeViewModel.Delete))
                {
                    var viewResult1 = SetCalendarValues(setModel);

                    if (ModelState.IsValid)
                    {
                        var teacherCalendar =
                            _teacherRepository.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);
                        _teacherRepository.DeleteTeacherCalendarByBooking(teacherCalendar);
                        return View("SuccessfullCreation");
                    }
                    return viewResult1;
                }
            }
            return viewResult;
        }

        private ViewResult SetCalendarValues(ITeacherCalendarViewModel bookingTimeViewModel)
        {
            var calendarBookingViewModels = new List<ICalendarBookingViewModel>();
            var calendar =
                _teacherRepository.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);
            if (calendar != null)
            {
                Student student = _teacherRepository.GetStudentById(calendar.StudentId);
                Subject subject = _teacherRepository.GetSubjectById(calendar.SubjectId);
                bookingTimeViewModel.StudentId = (int)student.StudentId;
                bookingTimeViewModel.SubjectId = (int)subject.SubjectId;
                bookingTimeViewModel.CalendarBookingId = calendar.CalendarBookingId;
                ModelState.Clear();

                var calendars = _teacherRepository.GetTeacherCalendar();
                var classRooms = _teacherRepository.GetClassrooms();

                var calendarsLeftJoin = from cal in calendars
                                        join cls in classRooms on
                                        cal.CalendarBookingId equals cls.CalendarId into res
                                        from q in res.DefaultIfEmpty()
                                        select new
                                        {
                                            ClassroomId = q == null ? null : q.ClassroomId,
                                            SubjectId = cal.SubjectId,
                                            TeacherId = cal.TeacherId,
                                            StudentId = cal.StudentId,
                                            BookingTimeId = cal.BookingTimeId
                                        };

                if (calendarsLeftJoin != null)
                    foreach (var cal in calendarsLeftJoin)
                    {
                        student = _teacherRepository.GetStudentById(cal.StudentId);
                        subject = _teacherRepository.GetSubjectById(cal.SubjectId);
                        var teacher = _teacherRepository.GetTeacherById(cal.TeacherId);
                        var bookingTime = _teacherRepository.GetBookingById(cal.BookingTimeId);

                        if (bookingTime == null) continue;
                        calendarBookingViewModels.Add(new CalendarBookingSelectOrDeleteViewModel
                        {
                            Teacher = teacher,
                            Subject = subject,
                            Student = student,
                            BookingTime = bookingTime,
                            ClassId = cal.ClassroomId
                        });
                    }
                ViewBag.CalendarUiList = calendarBookingViewModels.ToArray();
                if (bookingTimeViewModel.GetType().IsAssignableFrom(typeof(TeacherCalendarSelectOrDeleteViewModel)))
                {
                    ModelState.Clear();
                    return View("BookTeacherHelpTime", new TeacherCalendarSelectOrDeleteViewModel
                    {
                        TeacherId = calendar.TeacherId,
                        SubjectId = calendar.SubjectId,
                        CalendarBookingId = calendar.CalendarBookingId,
                        Description = calendar.Description,
                        ClassId = calendar.ClassId,
                        StudentId = calendar.StudentId,
                        StudentFullName = calendar.StudentFullName,
                        TeacherFullName = calendar.TeacherFullName,
                        BookingTimes = new BookingTimeString[] { new BookingTimeString { StartTime = calendar.BookingTime.StartTime.ToString("yyyy-MM-dd HH:mm"), EndTime = calendar.BookingTime.EndTime.ToString("yyyy-MM-dd HH:mm") } }
                    });
                }
                else if (bookingTimeViewModel.GetType().IsAssignableFrom(typeof(TeacherCalendarUpdateViewModel)))
                    return View("BookTeacherHelpTime", new TeacherCalendarUpdateViewModel
                    {
                        TeacherId = calendar.TeacherId,
                        SubjectId = calendar.SubjectId,
                        CalendarBookingId = calendar.CalendarBookingId,
                        Description = calendar.Description,
                        ClassId = calendar.ClassId,
                        StudentId = calendar.StudentId,
                        StudentFullName = calendar.StudentFullName,
                        TeacherFullName = calendar.TeacherFullName,
                        BookingTimes = new BookingTimeString[] { new BookingTimeString { StartTime = calendar.BookingTime.StartTime.ToString("yyyy-MM-dd HH:mm"), EndTime = calendar.BookingTime.EndTime.ToString("yyyy-MM-dd HH:mm") } }
                    });
                else if (bookingTimeViewModel.GetType().IsAssignableFrom(typeof(TeacherCalendarCreateViewModel)))
                    return View("BookTeacherHelpTime", new TeacherCalendarCreateViewModel
                    {
                        TeacherId = calendar.TeacherId,
                        SubjectId = calendar.SubjectId,
                        CalendarBookingId = calendar.CalendarBookingId,
                        Description = calendar.Description,
                        ClassId = calendar.ClassId,
                        StudentId = calendar.StudentId,
                        StudentFullName = calendar.StudentFullName,
                        TeacherFullName = calendar.TeacherFullName,
                        BookingTimes = new BookingTimeString[] { new BookingTimeString { StartTime = calendar.BookingTime.StartTime.ToString("yyyy-MM-dd HH:mm"), EndTime = calendar.BookingTime.EndTime.ToString("yyyy-MM-dd HH:mm") } }
                    });
                else return View("BookTeacherHelpTime", bookingTimeViewModel);
            }
            else
            {
                return View("BookTeacherHelpTime", bookingTimeViewModel);
            }
        }
        [HttpGet]
        public ActionResult TeachersCalendar()
        {
            GetUIDropdownLists();
            ViewBag.CalendarUiList = null;
            ViewBag.Message = "Teachers Calendar.";
            var calendars = _teacherRepository.GetTeacherCalendar();
            var calendarBookingViewModels = new List<CalendarBookingSelectOrDeleteViewModel>();
            var classRooms = _teacherRepository.GetClassrooms();

            var calendarsLeftJoin = from cal in calendars
                                    join cls in classRooms on
                                    cal.CalendarBookingId equals cls.CalendarId into res
                                    from q in res.DefaultIfEmpty()
                                    select new
                                    {
                                        ClassroomId = q == null ? null : q.ClassroomId,
                                        SubjectId = cal.SubjectId,
                                        TeacherId = cal.TeacherId,
                                        StudentId = cal.StudentId,
                                        BookingTimeId = cal.BookingTimeId
                                    };

            if (calendarsLeftJoin != null)
            {
                foreach (var cal in calendarsLeftJoin)
                {
                    var student = _teacherRepository.GetStudentById(cal.StudentId);
                    var subject = _teacherRepository.GetSubjectById(cal.SubjectId);
                    var teacher = _teacherRepository.GetTeacherById(cal.TeacherId);
                    var bookingTime = _teacherRepository.GetBookingById(cal.BookingTimeId);

                    if (bookingTime == null) continue;
                    calendarBookingViewModels.Add(new CalendarBookingSelectOrDeleteViewModel { Teacher = teacher, Subject = subject, Student = student, BookingTime = bookingTime, ClassId = cal.ClassroomId });
                }
            }
            ViewBag.CalendarUiList = calendarBookingViewModels.ToArray();
            return View("TeachersCalendar");

        }

        [HttpPost]
        public ActionResult TeachersCalendar(int teacherId)
        {
            GetUIDropdownLists();
            if (teacherId < 1)
            {
                ModelState.AddModelError("NoTeacher", "You need to pick a teacher");
            }
            ViewBag.Message = "Teachers Calendar.";
            var calendars = _teacherRepository.GetTeacherCalendar(teacherId);
            if (calendars == null)
            {
                calendars = _teacherRepository.GetTeacherCalendar();

                ModelState.AddModelError("NoTeacherCalendar", "Teacher hasn't a calendar booking");
            }
            var calendarBookingViewModels = new List<CalendarBookingSelectOrDeleteViewModel>();
            var classRooms = _teacherRepository.GetClassrooms();

            var calendarsLeftJoin = from cal in calendars
                                    join cls in classRooms on
                                    cal.CalendarBookingId equals cls.CalendarId into res
                                    from q in res.DefaultIfEmpty()
                                    select new
                                    {
                                        ClassroomId = q == null ? null : q.ClassroomId,
                                        SubjectId = cal.SubjectId,
                                        TeacherId = cal.TeacherId,
                                        StudentId = cal.StudentId,
                                        BookingTimeId = cal.BookingTimeId
                                    };
            if (calendarsLeftJoin != null)
            {
                foreach (var cal in calendarsLeftJoin)
                {
                    var student = _teacherRepository.GetStudentByName(User.Identity.Name);
                    if (student == null || student.StudentId != cal.StudentId) continue;
                    var subject = _teacherRepository.GetSubjectById(cal.SubjectId);
                    var teacher = _teacherRepository.GetTeacherById(teacherId);
                    var bookingTime = _teacherRepository.GetBookingById(cal.BookingTimeId);

                    if (bookingTime == null) continue;
                    calendarBookingViewModels.Add(new CalendarBookingSelectOrDeleteViewModel
                    {
                        Teacher = teacher,
                        Subject = subject,
                        Student = student,
                        BookingTime = bookingTime,
                        ClassId = cal.ClassroomId
                    });
                }

            }
            ViewBag.CalendarUiList = calendarBookingViewModels.ToArray();

            return View("TeachersCalendar",
                calendarBookingViewModels.Count() > 0
                    ? calendarBookingViewModels.ToArray()[calendarBookingViewModels.Count() - 1]
                    : null);
        }

        [HttpGet]
        public ActionResult GetStudentResources()
        {
            var StudentResources = _teacherRepository.GetGroupedResourcesByRoleThenSubject("SecondarySchool");
            return View("StudentResources", StudentResources);
        }
        public ActionResult DownloadFreeDocuments()
        {
            ViewBag.Message = "Download Free Documents.";
            var documents = _teacherRepository.GetFreeDocuments("SecondarySchool");
            var videos = _teacherRepository.GetFreeVideos("SecondarySchool");

            ViewBag.FreeDocumentsList = documents;
            ViewBag.FreeVideosList = videos;
            return View("DownloadFreeDocuments");
        }

        public ActionResult DownloadPaidDocuments()
        {
            ViewBag.Message = "Download Paid Documents."; 
            var documents = _teacherRepository.GetPaidDocuments("SecondarySchool");
            var videos = _teacherRepository.GetPaidVideos("SecondarySchool");

            ViewBag.PaidDocumentsList = documents;
            ViewBag.PaidVideosList = videos;

            return RedirectToRoute(new { controller = "Home", action = "DownloadPaidDocuments", namespaces = "TeacherAssistant.Controllers", area = "" });
        }
        public IList<SelectListItem> GetQARequestList()
        {
            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "Pick Product Item", Value = 0.ToString() });

            productList.AddRange(_teacherRepository.GetQARequestList().Select(p => new SelectListItem { Text = p.Description, Value = p.QAHelpRequestId.ToString() }).ToList());
            return productList;
        }
        private List<SelectListItem> GetClassroomList()
        {
            var classRoomList = new List<SelectListItem>();
            var classrooms = _teacherRepository.GetClassrooms();

            classRoomList.Add(new SelectListItem { Text = "Pick a Class", Value = 0.ToString() });
            classRoomList.AddRange(classrooms.Select(p => new SelectListItem { Text = string.Format("[{0}] Class", p.ClassroomId), Value = p.ClassroomId.ToString() }).ToList());
            return classRoomList;
        }
        private List<SelectListItem> GetStudentsList()
        {
            var students = _teacherRepository.GetStudentList();
            var studentList = new List<SelectListItem>();
            studentList.Add(new SelectListItem { Text = "Pick a Student", Value = 0.ToString() });

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
            subjectList.Add(new SelectListItem { Text = "Pick a Subject", Value = 0.ToString() });

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
            teacherList.Add(new SelectListItem { Text = "Pick a Teacher", Value = 0.ToString() });

            foreach (var teacher in teachers)
            {
                teacherList.Add(new SelectListItem { Text = teacher.EmailAddress, Value = teacher.TeacherId.ToString() });
            }

            return teacherList;
        }
        private List<SelectListItem> GetCalendarList()
        {
            var calendarList = new List<SelectListItem>();
            var teacherCalendars = _teacherRepository.GetTeacherCalendar();
            calendarList.Add(new SelectListItem { Text = "Pick a Calendar By Id and Teacher", Value = 0.ToString() });

            foreach (var cal in teacherCalendars)
            {
                calendarList.Add(new SelectListItem { Text = string.Format("[CalendarID:{0}] By ", cal.CalendarBookingId.ToString()) + GetTeacherList().Single(p => Convert.ToInt32(p.Value) == cal.TeacherId).Text, Value = cal.CalendarBookingId.ToString() });
            }
            return calendarList;
        }
        private List<SelectListItem> GetProductList()
        {
            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "Pick Product Item", Value = 0.ToString() });

            productList.AddRange(_teacherRepository.GetProductsList().Select(p => new SelectListItem { Text = p.prodName, Value = p.prodId.ToString() }).ToList());
            return productList;
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

        [HttpGet]
        public ActionResult ViewAssignmentGrades()
        {
            try
            {
                var studentId = _teacherRepository.GetStudentByName(User.Identity.Name).StudentId;
                var assignments = _teacherRepository.GetCurrentAssignmentsSubmissions("SecondarySchool", (int)studentId).Select(p =>
                new AssignmentSubmissionSelectOrDeleteViewModel
                {
                    AssignmentSubmissionId = p.AssignmentSubmissionId,
                    AssignmentId = p.AssignmentId,
                    AssignmentName = p.AssignmentName,
                    StudentId = p.StudentId,
                    DateDue = p.DateDue,
                    DateSubmitted = p.DateSubmitted,
                    Grade = p.Grade,
                    GradeNumeric = p.GradeNumeric,
                    FilePath = p.FilePath,
                    IsSubmitted = p.IsSubmitted,
                    StudentRole = p.StudentRole,
                    TeacherId = p.TeacherId,
                    SubjectId = p.SubjectId,
                    Notes = p.Notes,
                    CourseId = p.CourseId
                });
                return View("ViewAssignmentGrades", assignments.ToArray());
            }
            catch
            {

                return View("ViewAssignmentGrades", new AssignmentSubmissionSelectOrDeleteViewModel[] {});
            }

        }
        [HttpGet]
        public ActionResult AssignmentAndSubmissions()
        {
            try
            {
                var assignments = _teacherRepository.GetCurrentAssignments("SecondarySchool");

                var student = _teacherRepository.GetStudentByName(User.Identity.Name);
                var previousSubmissions = _teacherRepository.GetCurrentAssignmentsSubmissions();
                var listSubmissions = assignments.Select(p =>
                {
                    var hasPreviouslySubmitted = false;
                    AssignmentSubmission assignmentSubmission = null;
                    if (p.StudentId > 0)
                        assignmentSubmission = previousSubmissions.FirstOrDefault(q => q.AssignmentId == p.AssignmentId && q.StudentId == p.StudentId && p.StudentId == student.StudentId);
                    var assignmentSubmissionId = 0;
                    if (assignmentSubmission != null)
                    {
                        assignmentSubmissionId = assignmentSubmission.AssignmentSubmissionId;
                        hasPreviouslySubmitted = true;

                        return new AssignmentSubmissionSelectOrDeleteViewModel
                        {
                            AssignmentSubmissionId = assignmentSubmissionId,
                            AssignmentName = p.AssignmentName,
                            AssignmentId = p.AssignmentId,
                            DateDue = p.DateDue,
                            FilePath = p.FilePath,
                            StudentId = p.StudentId,
                            StudentRole = p.StudentRole,
                            IsSubmitted = hasPreviouslySubmitted,
                            TeacherId = p.TeacherId,
                            SubjectId = p.SubjectId,
                            Notes = hasPreviouslySubmitted ? assignmentSubmission.Notes : "",
                            CourseId = p.CourseId
                        };
                    }
					else if (p.StudentId > 0 && assignmentSubmission == null)return null;
                    else
                    {
                        return new AssignmentSubmissionSelectOrDeleteViewModel
                        {
                            AssignmentSubmissionId = assignmentSubmissionId,
                            AssignmentName = p.AssignmentName,
                            AssignmentId = p.AssignmentId,
                            DateDue = p.DateDue,
                            FilePath = p.FilePath,
                            StudentId = (int)student.StudentId,
                            StudentRole = p.StudentRole,
                            IsSubmitted = hasPreviouslySubmitted,
                            TeacherId = p.TeacherId,
                            SubjectId = p.SubjectId,
                            Notes = hasPreviouslySubmitted ? assignmentSubmission.Notes : "",
                            CourseId = p.CourseId
                        };
                    }
                });

                return View("AssignmentAndSubmissions", listSubmissions.Where(c=> c != null).ToArray());
            }
            catch
            {
                return View("AssignmentAndSubmissions", new AssignmentSubmissionSelectOrDeleteViewModel[] { });
            }
        }
        [HttpPost]
        public ActionResult SubmitAssignment(AssignmentSubmissionCreateViewModel submissions)
        {
            var assignment = _teacherRepository.GetAssignmentById(submissions.AssignmentId);
            Subject subject = _teacherRepository.GetSubjectById(submissions.SubjectId);
            Student student = _teacherRepository.GetStudentByName(User.Identity.Name);

            var virtualPath = string.Format("~/StudentResources/SecondarySchool/Assignments/Submissions/{0}/{1}", subject.SubjectName, CleanseAssignmentName(assignment.AssignmentName));

            //Save File to FileSystem:
            var fileBuffer = new byte[submissions.MediaContent.ContentLength];

            var physicalPath = Server.MapPath(virtualPath);
            var dirInfo = new DirectoryInfo(physicalPath);
            if (!dirInfo.Exists) dirInfo.Create();
            FileInfo fileInfo1 = new FileInfo(physicalPath + "\\" + student.StudentFirsName + student.StudentLastName + submissions.MediaContent.FileName);
            if (fileInfo1.Exists)
            {
                fileInfo1.Delete();
            }
            FileInfo fileInfo = new FileInfo(physicalPath + "\\" + student.StudentFirsName + student.StudentLastName + submissions.MediaContent.FileName);

            using (var fileStream = fileInfo.Create())
            {
                var sizeRead = 0;
                while ((sizeRead = submissions.MediaContent.InputStream.Read(fileBuffer, 0, fileBuffer.Length)) > 0)
                {
                    fileStream.Write(fileBuffer, 0, sizeRead);
                }
                submissions.MediaContent.InputStream.Flush();
                submissions.MediaContent.InputStream.Close();
                fileStream.Flush();
                fileStream.Close();
            }
            var actualSubmission = new AssignmentSubmission
            {
                AssignmentSubmissionId = submissions.AssignmentSubmissionId,
                AssignmentId = submissions.AssignmentId,
                DateDue = assignment.DateDue,
                DateSubmitted = DateTime.Now,
                StudentId = (int)student.StudentId,
                StudentRole = "SecondarySchool",
                FilePath = Url.Content(virtualPath + "/" + student.StudentFirsName + student.StudentLastName + submissions.MediaContent.FileName),
                IsSubmitted = true,
                SubjectId = submissions.SubjectId,
                TeacherId = submissions.TeacherId,
                AssignmentName = assignment.AssignmentName,
                Notes = submissions.Notes,
                CourseId = assignment.CourseId
            };
            _teacherRepository.SaveOrUpdateAssignmentSubmissions(actualSubmission);
            return View("SuccessfullCreation");

        }
        private void GetUIDropdownLists()
        {
            ViewBag.AssignmentList = GetCurrentAssignmentList("SecondarySchool");
            ViewBag.QAHelpRequestList = GetQARequestList();
            ViewBag.TeacherList = GetTeacherList();
            ViewBag.RoleList = GetRolesSelectList();
            ViewBag.StudentList = GetStudentsList();
            ViewBag.SubjectList = GetSubjectList();
            ViewBag.CalendarBookingList = GetCalendarList();
            ViewBag.ClassroomList = GetClassroomList();
        }
        private string CleanseAssignmentName(string assignmentName)
        {
            var results = assignmentName.Split(new char[] { ' ', ':', '!', ',', '?', ';' });
            var result = string.Empty;
            var buffer = new StringBuilder();
            foreach (var ch in results) buffer.Append(ch);
            return buffer.ToString();
        }
    }
}