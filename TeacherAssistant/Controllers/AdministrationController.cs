﻿using AutoMapper;
using CuttingEdge.Conditions;
using EmailServices.EmailDomain;
using EmailServices.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TeacherAssistant.Infrastructure.FluentValidation;
using TeacherAssistant.Models;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;
using TeachersAssistant.Services.Concretes;

namespace TeacherAssistant.Controllers
{
    [Authorize(Roles = "Admin,Administrator")]
    public class AdministrationController : Controller
    {
        private ApplicationUserManager _userManager;
        private IEmailService _emailService;

        TeachersAssistantRepositoryServices _repositoryServices;
        /*public AdministrationController()
        {

        }*/

        public AdministrationController(ICalendarBookingRepositoryMarker calendarRepositoryMarker,
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
            IBookingTimeRepositoryMarker bookingRepositoryMarker,
            IStudentResourceRepositoryMarker studentResourceRepositoryMarker,
            IQAHelpRequestRepositoryMarker qAHelpRequestRepositoryMarker,
            IAssignmentRepositoryMarker assignmentRepositoryMarker,
            IAssignmentSubmissionRepositoryMarker assignmentSubmissionRepositoryMarker,
            ICourseRepositoryMarker courseRepositoryMarker,
            IEmailService mailService)
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
            Condition.Requires<IBookingTimeRepositoryMarker>(bookingRepositoryMarker, "bookingRepositoryMarker").IsNotNull();
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
             bookingRepositoryMarker,
             studentResourceRepositoryMarker,
             qAHelpRequestRepositoryMarker,
             assignmentRepositoryMarker,
             assignmentSubmissionRepositoryMarker,
             courseRepositoryMarker);

            unitOfWork.InitializeDbContext(new TeachersAssistant.DataAccess.TeachersAssistantDbContext());
            _emailService = mailService;
            _repositoryServices = new TeachersAssistantRepositoryServices(unitOfWork);
        }

        [HttpGet]
        public ActionResult AssignWork()
        {
            GetUIDropdownLists();
            ViewBag.DateAssignedString = DateTime.Now;
            ViewBag.DateDueString = DateTime.Now;
            ViewBag.AssignmentList = GetAllAssignmentsList();
            return View("AssignWork");
        }

        [HttpGet]
        public JsonResult GetNewRegistrants()
        {
            return Json(new { Registrants = _repositoryServices.GetNewRegistrants() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCurrentStudents()
        {
            return Json(new { Registrants = _repositoryServices.GetStudentList().Select(p => p.EmailAddress).ToArray() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AssignWorkSelectOrDelete(AssignmentSelectAndDeleteViewModel assignmentViewModel)
        {
            try
            {
                ViewBag.DateAssignedString = DateTime.Now;
                ViewBag.DateDueString = DateTime.Now;
                GetUIDropdownLists();
                if (ModelState.IsValid)
                {

                    var virtualPath = string.Empty;


                    if (assignmentViewModel.Select != null)
                    {
                        if (assignmentViewModel.AssignmentId < 1)
                        {
                            ModelState.AddModelError("AssignmentId", "Assignment Id Required");
                            return View(assignmentViewModel);
                        }
                        Assignment doc = _repositoryServices.GetAssignmentById(assignmentViewModel.AssignmentId);
                        ModelState.Clear();
                        ViewBag.DateDueString = doc.DateDue;
                        ViewBag.DateAssignedString = doc.DateAssigned;
                        return View(new AssignmentSelectAndDeleteViewModel
                        {
                            AssignmentId = doc.AssignmentId,
                            StudentRole = doc.StudentRole,
                            SubjectId = doc.SubjectId,
                            StudentId = doc.StudentId,
                            FilePath = doc.FilePath,
                            DateAssigned = doc.DateAssigned,
                            DateDue = doc.DateDue,
                            Description = doc.Description,
                            AssignmentName = doc.AssignmentName,
                            CourseId = doc.CourseId,
                            TeacherId = doc.TeacherId
                        });

                    }
                    else if (assignmentViewModel.Delete != null)
                    {
                        if (assignmentViewModel.AssignmentId < 1)
                        {
                            ModelState.AddModelError("AssignmenId", "Assignment Id Required");
                            return View(assignmentViewModel);
                        }


                        var doc = _repositoryServices.GetAssignmentById(assignmentViewModel.AssignmentId);
                        if (doc != null)
                        {
                            var delFileInfo = new FileInfo(Server.MapPath(doc.FilePath));
                            if (delFileInfo.Exists)
                            {
                                delFileInfo.Delete();
                            }

                        }
                        _repositoryServices.DeleteAssignment(doc.AssignmentId);
                        return View("SuccessfullCreation");

                    }
                }

            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("AssignWork", assignmentViewModel);
            }
            return View("AssignWork", assignmentViewModel);
        }
        [HttpPost]
        public ActionResult AssignWorkUpdate(AssignmentUpdateViewModel assignmentViewModel)
        {
            try
            {
                ViewBag.DateAssignedString = DateTime.Now;
                ViewBag.DateDueString = DateTime.Now;
                ViewBag.AssignmentList = GetAllAssignmentsList();
                GetUIDropdownLists();
                if (ModelState.IsValid)
                {

                    var virtualPath = string.Empty;


                    //Save file to relevant fileSystem:
                    switch (assignmentViewModel.StudentRole.ToLower())
                    {
                        case "collegeandpostgraduate":
                            virtualPath = string.Format("~/StudentResources/CollegeAndPostGraduate/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "secondaryschool":
                            virtualPath = string.Format("~/StudentResources/SecondarySchool/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "grammar11plus":
                            virtualPath = string.Format("~/StudentResources/Grammar11Plus/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "stateprimary":
                            virtualPath = string.Format("~/StudentResources/StatePrimary/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "statejunior":
                            virtualPath = string.Format("~/StudentResources/StateJunior/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                    }
                    var dateDue = DateTime.Now;
                    var dataAssigned = DateTime.Now;

                    HttpPostedFileBase file = assignmentViewModel.MediaContent;

                    var fileName = file.FileName;
                    var fileBuffer = new byte[file.ContentLength];

                    var physicalPath = Server.MapPath(virtualPath);
                    var dirInfo = new DirectoryInfo(physicalPath);
                    if (!dirInfo.Exists) dirInfo.Create();
                    FileInfo fileInfo1 = new FileInfo(physicalPath + "\\" + file.FileName);
                    if (fileInfo1.Exists)
                    {
                        fileInfo1.Delete();
                    }
                    FileInfo fileInfo = new FileInfo(physicalPath + "\\" + file.FileName);

                    using (var fileStream = fileInfo.Create())
                    {
                        var sizeRead = 0;
                        while ((sizeRead = file.InputStream.Read(fileBuffer, 0, fileBuffer.Length)) > 0)
                        {
                            fileStream.Write(fileBuffer, 0, sizeRead);
                        }
                        file.InputStream.Flush();
                        file.InputStream.Close();
                        fileStream.Flush();
                        fileStream.Close();
                    }

                    _repositoryServices.SaveOrUpdateAssignment(new Assignment
                    {
                        AssignmentId = assignmentViewModel.AssignmentId,
                        AssignmentName = assignmentViewModel.AssignmentName,
                        DateAssigned = assignmentViewModel.DateAssigned,
                        DateDue = assignmentViewModel.DateDue,
                        Description = assignmentViewModel.Description,
                        FilePath = Url.Content(virtualPath + "/" + file.FileName),
                        StudentId = assignmentViewModel.StudentId,
                        StudentRole = assignmentViewModel.StudentRole,
                        SubjectId = assignmentViewModel.SubjectId,
                        TeacherId = assignmentViewModel.TeacherId,
                        CourseId = assignmentViewModel.CourseId
                    });
                    return View("SuccessfullCreation");
                }

                return View("AssignWork", assignmentViewModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("AssignWork", assignmentViewModel);
            }
        }
        [HttpPost]
        public ActionResult AssignWorkCreate(AssignmentCreateViewModel assignmentViewModel)
        {
            try
            {
                ViewBag.DateAssignedString = DateTime.Now;
                ViewBag.DateDueString = DateTime.Now;
                ViewBag.AssignmentList = GetAllAssignmentsList();
                GetUIDropdownLists();
                if (ModelState.IsValid)
                {

                    var virtualPath = string.Empty;


                    //Save file to relevant fileSystem:
                    switch (assignmentViewModel.StudentRole.ToLower())
                    {
                        case "collegeandpostgraduate":
                            virtualPath = string.Format("~/StudentResources/CollegeAndPostGraduate/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "secondaryschool":
                            virtualPath = string.Format("~/StudentResources/SecondarySchool/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "grammar11plus":
                            virtualPath = string.Format("~/StudentResources/Grammar11Plus/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "stateprimary":
                            virtualPath = string.Format("~/StudentResources/StatePrimary/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                        case "statejunior":
                            virtualPath = string.Format("~/StudentResources/StateJunior/Assignments/{0}", _repositoryServices.GetSubjectById(assignmentViewModel.SubjectId).SubjectName);
                            break;
                    }
                    var dateDue = DateTime.Now;
                    var dataAssigned = DateTime.Now;

                    HttpPostedFileBase file = assignmentViewModel.MediaContent;

                    var fileName = file.FileName;
                    var fileBuffer = new byte[file.ContentLength];

                    var physicalPath = Server.MapPath(virtualPath);
                    var dirInfo = new DirectoryInfo(physicalPath);
                    if (!dirInfo.Exists) dirInfo.Create();
                    FileInfo fileInfo1 = new FileInfo(physicalPath + "\\" + file.FileName);
                    if (fileInfo1.Exists)
                    {
                        fileInfo1.Delete();
                    }
                    FileInfo fileInfo = new FileInfo(physicalPath + "\\" + file.FileName);

                    using (var fileStream = fileInfo.Create())
                    {
                        var sizeRead = 0;
                        while ((sizeRead = file.InputStream.Read(fileBuffer, 0, fileBuffer.Length)) > 0)
                        {
                            fileStream.Write(fileBuffer, 0, sizeRead);
                        }
                        file.InputStream.Flush();
                        file.InputStream.Close();
                        fileStream.Flush();
                        fileStream.Close();
                    }

                    _repositoryServices.SaveOrUpdateAssignment(new Assignment
                    {
                        AssignmentName = assignmentViewModel.AssignmentName,
                        DateAssigned = assignmentViewModel.DateAssigned,
                        DateDue = assignmentViewModel.DateDue,
                        Description = assignmentViewModel.Description,
                        FilePath = Url.Content(virtualPath + "/" + file.FileName),
                        StudentId = assignmentViewModel.StudentId,
                        StudentRole = assignmentViewModel.StudentRole,
                        SubjectId = assignmentViewModel.SubjectId,
                        TeacherId = assignmentViewModel.TeacherId,
                        CourseId = assignmentViewModel.CourseId
                    });
                    return View("SuccessfullCreation");
                }

                return View("AssignWork", assignmentViewModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("AssignWork", assignmentViewModel);
            }
        }
        [HttpGet]
        public ActionResult ManageClassRoom()
        {
            ViewBag.DateSubmittedString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ViewBag.DateDueString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            GetUIDropdownLists();
            return View();
        }
        [HttpPost]
        public ActionResult ManageClassRoomSelectOrDelete(ClassroomSelectOrDeleteViewModel classRoomViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                var classRoomModel = (Classroom)Mapper.Map(classRoomViewModel, typeof(ClassroomSelectOrDeleteViewModel), typeof(Classroom));
                if (classRoomViewModel.Select != null)
                {
                    var calendar = _repositoryServices.GetTeacherCalendarByBookingId(classRoomViewModel.CalendarBookingId);
                    var classroom = _repositoryServices.GetClassroomById(classRoomViewModel.ClassroomId);
                    classRoomViewModel = (ClassroomSelectOrDeleteViewModel)Mapper.Map(classroom, typeof(Classroom), typeof(ClassroomSelectOrDeleteViewModel));
                    classRoomViewModel.CalendarBookingId = (int)calendar.CalendarBookingId;
                    classRoomViewModel.SubjectId = calendar.SubjectId;
                    ViewBag.DateSubmittedString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    ViewBag.DateDueString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    return View("ManageClassRoom", classRoomViewModel);
                }

                if (classRoomViewModel.Delete != null)
                {
                    var classroom = _repositoryServices.GetClassroomById(classRoomViewModel.ClassroomId);
                    _repositoryServices.DeleteClassroom(classroom);
                    return View("SuccessfullCreation");
                }
                else
                {
                    var calendar = _repositoryServices.GetTeacherCalendarByBookingId(classRoomViewModel.CalendarBookingId);
                    _repositoryServices.ManageClassRoom(classRoomModel);
                    calendar.SubjectId = classRoomModel.SubjectId;
                    calendar.ClassId = (int)classRoomModel.ClassroomId;
                    _repositoryServices.SaveOrUpdateCalendar(calendar);
                    return View("SuccessfullCreation");
                }
            }
            ViewBag.DateSubmittedString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ViewBag.DateDueString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return View("ManageClassRoom", classRoomViewModel);
        }
        public ActionResult ManageClassRoomCreate(ClassroomCreateViewModel classRoomViewModel)
        {
            GetUIDropdownLists();
            var classRoomModel = (Classroom)Mapper.Map(classRoomViewModel, typeof(ClassroomCreateViewModel), typeof(Classroom));

            if (ModelState.IsValid)
            {
                var calendar = _repositoryServices.GetTeacherCalendarByBookingId(classRoomViewModel.CalendarBookingId);
                _repositoryServices.ManageClassRoom(classRoomModel);
                calendar.SubjectId = classRoomModel.SubjectId;
                calendar.ClassId = (int)classRoomModel.ClassroomId;
                _repositoryServices.SaveOrUpdateCalendar(calendar);
                return View("SuccessfullCreation");
            }
            ViewBag.DateSubmittedString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ViewBag.DateDueString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return View("ManageClassRoom", classRoomViewModel);
        }
        public ActionResult ManageClassRoomUpdate(ClassroomUpdateViewModel classRoomViewModel)
        {
            GetUIDropdownLists();
            var classRoomModel = (Classroom)Mapper.Map(classRoomViewModel, typeof(ClassroomUpdateViewModel), typeof(Classroom));

            if (ModelState.IsValid)
            {
                if (classRoomModel.SubjectId < 0 || classRoomModel.TeacherId < 0 || classRoomViewModel.CalendarBookingId < 0)
                {
                    ModelState.AddModelError("ClassroomId", "CalendarBookingId Teacher and Subject are required");
                    return View("ManageClassRoom", classRoomViewModel);
                }
                var calendar = _repositoryServices.GetTeacherCalendarByBookingId(classRoomViewModel.CalendarBookingId);
                _repositoryServices.ManageClassRoom(classRoomModel);
                calendar.SubjectId = classRoomModel.SubjectId;
                calendar.ClassId = (int)classRoomModel.ClassroomId;
                _repositoryServices.SaveOrUpdateCalendar(calendar);
                return View("SuccessfullCreation");
            }
            ViewBag.DateSubmittedString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ViewBag.DateDueString = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return View("ManageClassRoom", classRoomViewModel);
        }
        [HttpGet]
        public ActionResult ManageStudent()
        {
            GetUIDropdownLists();
            return View("ManageStudent");
        }

        [HttpPost]
        public ActionResult ManageStudentSelectOrDelete(StudentSelectOrDeleteViewModel studentViewModel)
        {
            GetUIDropdownLists();

            if (ModelState.IsValid)
            {
                if (studentViewModel.Select != null)
                {
                    if (studentViewModel.StudentId < 1)
                    {
                        ModelState.AddModelError("StudentId", "StudentId is required");
                        return View("ManageStudent", studentViewModel);
                    }
                    var student = _repositoryServices.GetStudentById(studentViewModel.StudentId);
                    studentViewModel = (StudentSelectOrDeleteViewModel)Mapper.Map(student, typeof(Student), typeof(StudentSelectOrDeleteViewModel));

                    ModelState.Clear();
                    return View("ManageStudent", studentViewModel);
                }
                if (studentViewModel.Delete != null)
                {
                    var student = _repositoryServices.GetStudentById(studentViewModel.StudentId);
                    _repositoryServices.DeleteStudent(student);
                    return View("SuccessfullCreation");
                }
            }

            return View("ManageStudent", studentViewModel);
        }

        [HttpPost]
        public ActionResult ManageStudentUpdate(StudentUpdateViewModel studentViewModel)
        {
            GetUIDropdownLists();

            if (ModelState.IsValid)
            {
                if (studentViewModel.Select != null)
                {
                    if (studentViewModel.StudentId < 1)
                    {
                        ModelState.AddModelError("StudentId", "StudentId is required");
                        return View("ManageStudent", studentViewModel);
                    }
                    var student = _repositoryServices.GetStudentById(studentViewModel.StudentId);
                    studentViewModel = (StudentUpdateViewModel)Mapper.Map(student, typeof(Student), typeof(StudentUpdateViewModel));

                    ModelState.Clear();
                    return View("ManageStudent", studentViewModel);
                }
                if (studentViewModel.Delete != null)
                {
                    var student = _repositoryServices.GetStudentById(studentViewModel.StudentId);
                    _repositoryServices.DeleteStudent(student);
                    return View("SuccessfullCreation");
                }
                else
                {
                    var studentModel =
                        (Student)Mapper.Map(studentViewModel, typeof(StudentUpdateViewModel), typeof(Student));
                    _repositoryServices.ManageStudent(studentModel);
                    return View("SuccessfullCreation");
                }
            }

            return View("ManageStudent", studentViewModel);
        }
        [HttpPost]
        public ActionResult ManageStudentCreate(StudentCreateViewModel studentViewModel)
        {
            GetUIDropdownLists();

            if (ModelState.IsValid)
            {
                var studentModel =
                    (Student)Mapper.Map(studentViewModel, typeof(StudentCreateViewModel), typeof(Student));
                _repositoryServices.ManageStudent(studentModel);
                return View("SuccessfullCreation");
            }

            return View("ManageStudent", studentViewModel);
        }
        [HttpGet]
        public ActionResult ManageCourse()
        {
            GetUIDropdownLists();
            return View("ManageCourses");
        }

        [HttpPost]
        public ActionResult ManageCourseSelectOrDelete(CourseSelectOrDeleteViewModel courseViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                if (courseViewModel.Select != null)
                {
                    var course = _repositoryServices.GetCourseById(courseViewModel.CourseId);
                    courseViewModel = (CourseSelectOrDeleteViewModel)Mapper.Map(course, typeof(Course), typeof(CourseSelectOrDeleteViewModel));

                    ModelState.Clear();
                    return View("ManageCourses", courseViewModel);
                }
                if (courseViewModel.Delete != null)
                {

                    var course = _repositoryServices.GetCourseById(courseViewModel.CourseId);
                    _repositoryServices.DeleteCourse(course);
                    return View("SuccessfullCreation");
                }
            }
            return View("ManageCourses", courseViewModel);
        }
        [HttpPost]
        public ActionResult ManageCourseCreate(CourseCreateViewModel courseViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(courseViewModel.CourseName) && !string.IsNullOrEmpty(courseViewModel.CourseDescription))
                {
                    var course = (Course)Mapper.Map(courseViewModel, typeof(CourseCreateViewModel), typeof(Course));
                    _repositoryServices.AddOrUpdateCourse(course);
                    return View("SuccessfullCreation");

                }
            }
            return View("ManageCourses", courseViewModel);
        }
        [HttpPost]
        public ActionResult ManageCourseUpdate(CourseUpdateViewModel courseViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(courseViewModel.CourseName) && !string.IsNullOrEmpty(courseViewModel.CourseDescription))
                {
                    var course = (Course)Mapper.Map(courseViewModel, typeof(CourseUpdateViewModel), typeof(Course));
                    _repositoryServices.AddOrUpdateCourse(course);
                    return View("SuccessfullCreation");

                }
            }
            return View("ManageCourses", courseViewModel);
        }
        [HttpGet]
        public ActionResult ManageSubject()
        {
            GetUIDropdownLists();
            return View("ManageSubject");
        }

        [HttpPost]
        public ActionResult ManageSubjectSelectOrDelete(SubjectSelectOrDeleteViewModel subjectViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {

                if (subjectViewModel.Select != null)
                {
                    var subject = _repositoryServices.GetSubjectById(subjectViewModel.SubjectId);
                    subjectViewModel = (SubjectSelectOrDeleteViewModel)Mapper.Map(subject, typeof(Subject), typeof(SubjectSelectOrDeleteViewModel));
                    ModelState.Clear();
                    return View("ManageSubject", subjectViewModel);
                }
                if (subjectViewModel.Delete != null)
                {
                    if (ModelState.IsValid)
                    {
                        var subject = _repositoryServices.GetSubjectById(subjectViewModel.SubjectId);
                        _repositoryServices.DeleteSubject(subject);
                        return View("SuccessfullCreation");
                    }
                    return View("ManageSubject", subjectViewModel);
                }
            }
            return View("ManageSubject", subjectViewModel);
        }
        [HttpPost]
        public ActionResult ManageSubjectCreate(SubjectCreateViewModel subjectViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                var subjectModel = (Subject)Mapper.Map(subjectViewModel, typeof(SubjectCreateViewModel), typeof(Subject));
                if (subjectViewModel.SubjectName != null)
                {
                    _repositoryServices.ManageSubject(subjectModel);
                    return View("SuccessfullCreation");

                }
            }
            return View("ManageSubject", subjectViewModel);
        }
        [HttpPost]
        public ActionResult ManageSubjectUpdate(SubjectUpdateViewModel subjectViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                var subjectModel = (Subject)Mapper.Map(subjectViewModel, typeof(SubjectUpdateViewModel), typeof(Subject));
                if (subjectViewModel.SubjectName != null)
                {
                    _repositoryServices.ManageSubject(subjectModel);
                    return View("SuccessfullCreation");

                }
            }
            return View("ManageSubject", subjectViewModel);
        }
        [HttpGet]
        public ActionResult ManageTeacher()
        {
            GetUIDropdownLists();
            return View("ManageTeacher");
        }

        [HttpPost]
        public ActionResult ManageTeacherSelectOrDelete(TeacherSelectOrDeleteViewModel teacherViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid && teacherViewModel.Select != null)
            {
                var teacher = _repositoryServices.GetTeacherById(teacherViewModel.TeacherId);
                teacherViewModel = (TeacherSelectOrDeleteViewModel)Mapper.Map(teacher, typeof(Teacher), typeof(TeacherSelectOrDeleteViewModel));
                ModelState.Clear();
                return View("ManageTeacher", teacherViewModel);
            }
            if (ModelState.IsValid && teacherViewModel.Delete != null)
            {
                var teacher = _repositoryServices.GetTeacherByName(teacherViewModel.EmailAddress);
                _repositoryServices.DeleteTeacher(teacher);
                return View("SuccessfullCreation");
            }
            return View("ManageTeacher", teacherViewModel);
        }
        [HttpPost]
        public ActionResult ManageTeacherCreate(TeacherCreateViewModel teacherViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                var teacherModel = (Teacher)Mapper.Map(teacherViewModel, typeof(TeacherCreateViewModel), typeof(Teacher));
                _repositoryServices.ManageTeachers(teacherModel);
                return View("SuccessfullCreation");
            }
            return View("ManageTeacher", teacherViewModel);
        }
        [HttpPost]
        public ActionResult ManageTeacherUpdate(TeacherUpdateViewModel teacherViewModel)
        {
            GetUIDropdownLists();
            if (ModelState.IsValid)
            {
                var teacherModel = (Teacher)Mapper.Map(teacherViewModel, typeof(TeacherUpdateViewModel), typeof(Teacher));
                _repositoryServices.ManageTeachers(teacherModel);
                return View("SuccessfullCreation");
            }
            return View("ManageTeacher", teacherViewModel);
        }
        [HttpGet]
        public ActionResult ManageStudentResources()
        {
            GetUIDropdownLists();
            return View("ManageResources");
        }
        [HttpPost]
        public ActionResult ManageStudentResourcesSelectOrDelete(StudentResourcesSelectOrDeleteViewModel resourceModel)
        {
            try
            {
                GetUIDropdownLists();

                ViewBag.StudentResourcesList = GetStudentResourcesList();
                ViewBag.RoleList = GetRolesSelectList();
                ViewBag.SubjectList = GetSubjectList();

                var virtualPath = string.Empty;

                //Save file to relevant fileSystem:
                if (ModelState.IsValid)
                {

                    if (resourceModel.Select != null)
                    {
                        StudentResource doc = _repositoryServices.GetStudentResourceById(resourceModel.StudentResourceId);
                        ModelState.Clear();
                        return View("ManageResources", new StudentResourcesSelectOrDeleteViewModel
                        {
                            StudentResourceId = doc.StudentResourceId,
                            RoleName = doc.RoleName,
                            SubjectId = doc.SubjectId,
                            FilePath = doc.FilePath,
                            StudentResourceName = doc.StudentResourceName
                        });
                    }
                    else if (resourceModel.Delete != null)
                    {
                        var doc = _repositoryServices.GetStudentResourceById(resourceModel.StudentResourceId);
                        if (doc != null)
                        {
                            var delFileInfo = new FileInfo(Server.MapPath(doc.FilePath));
                            if (delFileInfo.Exists)
                            {
                                delFileInfo.Delete();
                            }

                        }
                        _repositoryServices.DeleteStudentResource(resourceModel.StudentResourceId);
                        return View("SuccessfullCreation");
                    }
                }
                return View("ManageResources", resourceModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("ManageResources", resourceModel);
            }
        }
        [HttpPost]
        public ActionResult ManageStudentResourcesUpdate(StudentResourcesUpdateViewModel resourceModel)
        {
            try
            {
                GetUIDropdownLists();

                ViewBag.StudentResourcesList = GetStudentResourcesList();
                ViewBag.RoleList = GetRolesSelectList();
                ViewBag.SubjectList = GetSubjectList();

                var virtualPath = string.Empty;

                //Save file to relevant fileSystem:

                if (ModelState.IsValid)
                {
                    var subj = _repositoryServices.GetSubjectById(resourceModel.SubjectId);
                    virtualPath = GetResourcesFilePath(resourceModel.RoleName, subj);
                    HttpPostedFileBase file = resourceModel.MediaContent;

                    var fileName = file.FileName;
                    var fileBuffer = new byte[file.ContentLength];

                    var physicalPath = Server.MapPath(virtualPath);
                    var dirInfo = new DirectoryInfo(physicalPath);
                    if (!dirInfo.Exists) dirInfo.Create();

                    FileInfo fileInfo1 = new FileInfo(physicalPath + "\\" + file.FileName);
                    if (fileInfo1.Exists)
                    {
                        fileInfo1.Delete();
                    }
                    FileInfo fileInfo = new FileInfo(physicalPath + "\\" + file.FileName);

                    using (var fileStream = fileInfo.Create())
                    {
                        var sizeRead = 0;
                        while ((sizeRead = file.InputStream.Read(fileBuffer, 0, fileBuffer.Length)) > 0)
                        {
                            fileStream.Write(fileBuffer, 0, sizeRead);
                        }
                        file.InputStream.Flush();
                        file.InputStream.Close();
                        fileStream.Flush();
                        fileStream.Close();
                    }

                    var studentResource = new StudentResource()
                    {
                        StudentResourceId = resourceModel.StudentResourceId,
                        FilePath = Url.Content(virtualPath + "/" + file.FileName),
                        SubjectId = resourceModel.SubjectId,
                        RoleName = resourceModel.RoleName,
                        StudentResourceName = resourceModel.StudentResourceName,
                        CourseId = resourceModel.CourseId
                    };
                    //Save file Path To DB: 
                    _repositoryServices.SaveOrUpdateStudentResource(studentResource);
                    return View("SuccessfullCreation");
                }

                return View("ManageResources", resourceModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("ManageResources", resourceModel);
            }
        }
        [HttpPost]
        public ActionResult ManageStudentResourcesCreate(StudentResourcesCreateViewModel resourceModel)
        {
            try
            {
                GetUIDropdownLists();

                ViewBag.StudentResourcesList = GetStudentResourcesList();
                ViewBag.RoleList = GetRolesSelectList();
                ViewBag.SubjectList = GetSubjectList();

                var virtualPath = string.Empty;

                //Save file to relevant fileSystem:

                if (ModelState.IsValid)
                {
                    var subj = _repositoryServices.GetSubjectById(resourceModel.SubjectId);
                    virtualPath = GetResourcesFilePath(resourceModel.RoleName, subj);
                    HttpPostedFileBase file = resourceModel.MediaContent;

                    var fileName = file.FileName;
                    var fileBuffer = new byte[file.ContentLength];

                    var physicalPath = Server.MapPath(virtualPath);
                    var dirInfo = new DirectoryInfo(physicalPath);
                    if (!dirInfo.Exists) dirInfo.Create();

                    FileInfo fileInfo1 = new FileInfo(physicalPath + "\\" + file.FileName);
                    if (fileInfo1.Exists)
                    {
                        fileInfo1.Delete();
                    }
                    FileInfo fileInfo = new FileInfo(physicalPath + "\\" + file.FileName);

                    using (var fileStream = fileInfo.Create())
                    {
                        var sizeRead = 0;
                        while ((sizeRead = file.InputStream.Read(fileBuffer, 0, fileBuffer.Length)) > 0)
                        {
                            fileStream.Write(fileBuffer, 0, sizeRead);
                        }
                        file.InputStream.Flush();
                        file.InputStream.Close();
                        fileStream.Flush();
                        fileStream.Close();
                    }

                    var studentResource = new StudentResource()
                    {
                        StudentResourceId = resourceModel.StudentResourceId,
                        FilePath = Url.Content(virtualPath + "/" + file.FileName),
                        SubjectId = resourceModel.SubjectId,
                        RoleName = resourceModel.RoleName,
                        StudentResourceName = resourceModel.StudentResourceName,
                        CourseId = resourceModel.CourseId
                    };
                    //Save file Path To DB: 
                    _repositoryServices.SaveOrUpdateStudentResource(studentResource);
                    return View("SuccessfullCreation");
                }

                return View("ManageResources", resourceModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("ManageResources", resourceModel);
            }
        }

        private string GetResourcesFilePath(string roleName, Subject subject)
        {
            var virtualPath = string.Empty;

            switch (roleName.ToLower())
            {
                case "collegeandpostgraduate":
                    virtualPath = string.Format("~/StudentResources/CollegeAndPostGraduate/{0}", subject.SubjectName);
                    break;
                case "secondaryschool":
                    virtualPath = string.Format("~/StudentResources/SecondarySchool/{0}", subject.SubjectName);
                    break;
                case "grammar11plus":
                    virtualPath = string.Format("~/StudentResources/Grammar11Plus/{0}", subject.SubjectName);
                    break;
                case "stateprimary":
                    virtualPath = string.Format("~/StudentResources/StatePrimary/{0}", subject.SubjectName);
                    break;
                case "statejunior":
                    virtualPath = string.Format("~/StudentResources/StateJunior/{0}", subject.SubjectName);
                    break;
            }
            return virtualPath;
        }

        [HttpGet]
        public ActionResult UploadMedia()
        {
            GetUIDropdownLists();
            return View("UploadMedia");
        }
        [HttpPost]
        public ActionResult UploadMediaSelectOrDelete(UploadMediaSelectOrDeleteViewModel mediaModel)
        {
            try
            {
                GetUIDropdownLists();

                ViewBag.RoleList = GetRolesSelectList();


                var mediaType = mediaModel.MediaType;
                var virtualPath = string.Empty;
                if (ModelState.IsValid)
                {
                    switch (mediaModel.RoleName.ToLower())
                    {
                        case "collegeandpostgraduate":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/CollegeAndPostGraduate/PaidDocuments";
                            else virtualPath = "~/Documents/CollegeAndPostGraduate/FreeDocuments";
                            break;
                        case "secondaryschool":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/SecondarySchool/PaidDocuments";
                            else virtualPath = "~/Documents/SecondarySchool/FreeDocuments";
                            break;
                        case "grammar11plus":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/Grammar11Plus/PaidDocuments";
                            else virtualPath = "~/Documents/Grammar11Plus/FreeDocuments";
                            break;
                        case "stateprimary":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/StatePrimary/PaidDocuments";
                            else virtualPath = "~/Documents/StatePrimary/FreeDocuments";
                            break;
                        case "statejunior":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/StateJunior/PaidDocuments";
                            else virtualPath = "~/Documents/StateJunior/FreeDocuments";
                            break;
                        default:
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/Administration/PaidDouments";
                            else virtualPath = "~/Documents/Administration/FreeDouments";
                            break;
                    }

                    if (mediaModel.Select != null)
                    {
                        if (mediaModel.MediaId == null || string.IsNullOrEmpty(mediaType) || mediaModel.MediaId < 1)
                        {
                            ModelState.AddModelError("MediaId", "Media Id Required");
                            return View("UploadMedia", mediaModel);
                        }
                        switch (mediaType.ToLower())
                        {
                            case "paiddocument":
                                var paidDoc = _repositoryServices.GetPaidDocumentById(mediaModel.MediaId);
                                ModelState.Clear();
                                return View(new UploadMediaSelectOrDeleteViewModel { MediaId = paidDoc.PaidDocumentId, RoleName = paidDoc.RoleName, Name = paidDoc.FilePath });
                            case "freedocument":
                                var freeDoc = _repositoryServices.GetFreeDocumentById(mediaModel.MediaId);
                                ModelState.Clear();
                                return View(new UploadMediaSelectOrDeleteViewModel { MediaId = freeDoc.FreeDocumentId, RoleName = freeDoc.RoleName, Name = freeDoc.FilePath });
                            case "paidvideo":
                                var paidVideo = _repositoryServices.GetPaidVideoById(mediaModel.MediaId);
                                ModelState.Clear();
                                return View(new UploadMediaSelectOrDeleteViewModel { MediaId = paidVideo.PaidVideoId, RoleName = paidVideo.RoleName, Name = paidVideo.FilePath });
                            case "freevideo":
                                var freeVideo = _repositoryServices.GetFreeVideoById(mediaModel.MediaId);
                                ModelState.Clear();
                                return View(new UploadMediaSelectOrDeleteViewModel { MediaId = freeVideo.FreeVideoId, RoleName = freeVideo.RoleName, Name = freeVideo.FilePath });
                        }
                        ModelState.Clear();
                        return View("UploadMedia", mediaModel);
                    }
                    else if (mediaModel.Delete != null)
                    {
                        if (mediaModel.MediaId == null || string.IsNullOrEmpty(mediaType) || mediaModel.MediaId < 1)
                        {
                            ModelState.AddModelError("MediaId", "Media Id Required");
                            return View("UploadMedia", mediaModel);
                        }


                        switch (mediaType.ToLower())
                        {
                            case "paiddocument":
                                dynamic doc = _repositoryServices.GetPaidDocumentById(mediaModel.MediaId);
                                if (doc != null)
                                {
                                    var delFileInfo = new FileInfo(Server.MapPath(doc.FilePath));
                                    if (delFileInfo.Exists)
                                    {
                                        delFileInfo.Delete();
                                    }

                                }
                                _repositoryServices.DeletePaidDocumentById(mediaModel.MediaId);
                                return View("SuccessfullCreation");
                            case "freedocument":
                                doc = _repositoryServices.GetFreeDocumentById(mediaModel.MediaId);
                                if (doc != null)
                                {
                                    var delFileInfo = new FileInfo(Server.MapPath(doc.FilePath));
                                    if (delFileInfo.Exists)
                                    {
                                        delFileInfo.Delete();
                                    }

                                }
                                _repositoryServices.DeleteFreeDocumentById(mediaModel.MediaId);
                                return View("SuccessfullCreation");
                            case "paidvideo":
                                doc = _repositoryServices.GetPaidVideoById(mediaModel.MediaId);
                                if (doc != null)
                                {
                                    var delFileInfo = new FileInfo(Server.MapPath(doc.FilePath));
                                    if (delFileInfo.Exists)
                                    {
                                        delFileInfo.Delete();
                                    }

                                }
                                _repositoryServices.DeletePaidVideoById(mediaModel.MediaId);
                                return View("SuccessfullCreation");
                            case "freevideo":
                                doc = _repositoryServices.GetFreeVideoById(mediaModel.MediaId);
                                if (doc != null)
                                {
                                    var delFileInfo = new FileInfo(Server.MapPath(doc.FilePath));
                                    if (delFileInfo.Exists)
                                    {
                                        delFileInfo.Delete();
                                    }

                                }
                                _repositoryServices.DeleteFreeVideoById(mediaModel.MediaId);
                                return View("SuccessfullCreation");
                        }
                        return View("UploadMedia", mediaModel);
                    }

                }
                return View("UploadMedia", mediaModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("UploadMedia", mediaModel);
            }
        }

        [HttpPost]
        public ActionResult UploadMediaCreate(UploadMediaCreateViewModel mediaModel)
        {
            try
            {
                GetUIDropdownLists();

                ViewBag.RoleList = GetRolesSelectList();


                var mediaType = mediaModel.MediaType;
                var virtualPath = string.Empty;
                if (ModelState.IsValid)
                {
                    //Save file to relevant fileSystem:
                    switch (mediaModel.RoleName.ToLower())
                    {
                        case "collegeandpostgraduate":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/CollegeAndPostGraduate/PaidDocuments";
                            else virtualPath = "~/Documents/CollegeAndPostGraduate/FreeDocuments";
                            break;
                        case "secondaryschool":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/SecondarySchool/PaidDocuments";
                            else virtualPath = "~/Documents/SecondarySchool/FreeDocuments";
                            break;
                        case "grammar11plus":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/Grammar11Plus/PaidDocuments";
                            else virtualPath = "~/Documents/Grammar11Plus/FreeDocuments";
                            break;
                        case "stateprimary":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/StatePrimary/PaidDocuments";
                            else virtualPath = "~/Documents/StatePrimary/FreeDocuments";
                            break;
                        case "statejunior":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/StateJunior/PaidDocuments";
                            else virtualPath = "~/Documents/StateJunior/FreeDocuments";
                            break;
                        default:
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/Administration/PaidDouments";
                            else virtualPath = "~/Documents/Administration/FreeDouments";
                            break;
                    }


                    HttpPostedFileBase file = mediaModel.MediaContent;
                    var fileName = file.FileName;
                    var fileBuffer = new byte[file.ContentLength];
                    var fileContent = file.InputStream.Read(fileBuffer, 0, fileBuffer.Length);
                    file.InputStream.Flush();
                    file.InputStream.Close();

                    var physicalPath = Server.MapPath(virtualPath);
                    var dirInfo = new DirectoryInfo(physicalPath);
                    if (!dirInfo.Exists) dirInfo.Create();

                    FileInfo fileInfo1 = new FileInfo(physicalPath + "\\" + file.FileName);
                    if (fileInfo1.Exists)
                    {
                        fileInfo1.Delete();
                    }
                    FileInfo fileInfo = new FileInfo(physicalPath + "\\" + file.FileName);
                    using (var fileStream = fileInfo.Create())
                    {
                        fileStream.Write(fileBuffer, 0, fileBuffer.Length);
                        fileStream.Flush();
                        fileStream.Close();
                    }

                    var paidDocument = new PaidDocument
                    {
                        FilePath = "~" + Url.Content(virtualPath) + "/" + file.FileName,
                        SubjectId = mediaModel.SubjectId,
                        RoleName = mediaModel.RoleName
                    };
                    //Save file Path To DB: 
                    switch (mediaModel.MediaType.ToLower())
                    {
                        case "paiddocument":
                            _repositoryServices.SaveOrUpdatePaidDocument(paidDocument);
                            break;
                        case "freedocument":
                            var freeDocument = AutoMapper.Mapper.Map(paidDocument, typeof(PaidDocument),
                                typeof(FreeDocument));
                            _repositoryServices.SaveOrUpdateFreeDocument(freeDocument as FreeDocument);
                            break;
                        case "paidvideo":
                            var paidVideo = AutoMapper.Mapper.Map(paidDocument, typeof(PaidDocument),
                                typeof(PaidVideo));
                            _repositoryServices.SaveOrUpdatePaidVideo(paidVideo as PaidVideo);
                            break;
                        case "freevideo":
                            var freeVideo = AutoMapper.Mapper.Map(paidDocument, typeof(PaidDocument),
                                typeof(FreeVideo));
                            _repositoryServices.SaveOrUpdateFreeVideo(freeVideo as FreeVideo);
                            break;
                    }
                    return View("SuccessfullCreation");
                }
                return View("UploadMedia", mediaModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("UploadMedia", mediaModel);
            }
        }

        [HttpPost]
        public ActionResult UploadMediaUpdate(UploadMediaUpdateViewModel mediaModel)
        {
            try
            {
                GetUIDropdownLists();

                ViewBag.RoleList = GetRolesSelectList();


                var mediaType = mediaModel.MediaType;
                var virtualPath = string.Empty;
                if (ModelState.IsValid)
                {
                    //Save file to relevant fileSystem:
                    switch (mediaModel.RoleName.ToLower())
                    {
                        case "collegeandpostgraduate":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/CollegeAndPostGraduate/PaidDocuments";
                            else virtualPath = "~/Documents/CollegeAndPostGraduate/FreeDocuments";
                            break;
                        case "secondaryschool":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/SecondarySchool/PaidDocuments";
                            else virtualPath = "~/Documents/SecondarySchool/FreeDocuments";
                            break;
                        case "grammar11plus":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/Grammar11Plus/PaidDocuments";
                            else virtualPath = "~/Documents/Grammar11Plus/FreeDocuments";
                            break;
                        case "stateprimary":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/StatePrimary/PaidDocuments";
                            else virtualPath = "~/Documents/StatePrimary/FreeDocuments";
                            break;
                        case "statejunior":
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/StateJunior/PaidDocuments";
                            else virtualPath = "~/Documents/StateJunior/FreeDocuments";
                            break;
                        default:
                            if (mediaType.ToLower().StartsWith("paid"))
                                virtualPath = "~/Documents/Administration/PaidDouments";
                            else virtualPath = "~/Documents/Administration/FreeDouments";
                            break;
                    }


                    HttpPostedFileBase file = mediaModel.MediaContent;
                    var fileName = file.FileName;
                    var fileBuffer = new byte[file.ContentLength];
                    var fileContent = file.InputStream.Read(fileBuffer, 0, fileBuffer.Length);
                    file.InputStream.Flush();
                    file.InputStream.Close();

                    var physicalPath = Server.MapPath(virtualPath);
                    var dirInfo = new DirectoryInfo(physicalPath);
                    if (!dirInfo.Exists) dirInfo.Create();

                    FileInfo fileInfo1 = new FileInfo(physicalPath + "\\" + file.FileName);
                    if (fileInfo1.Exists)
                    {
                        fileInfo1.Delete();
                    }
                    FileInfo fileInfo = new FileInfo(physicalPath + "\\" + file.FileName);
                    using (var fileStream = fileInfo.Create())
                    {
                        fileStream.Write(fileBuffer, 0, fileBuffer.Length);
                        fileStream.Flush();
                        fileStream.Close();
                    }

                    var paidDocument = new PaidDocument
                    {
                        FilePath = "~" + Url.Content(virtualPath) + "/" + file.FileName,
                        SubjectId = mediaModel.SubjectId,
                        RoleName = mediaModel.RoleName
                    };
                    //Save file Path To DB: 
                    switch (mediaModel.MediaType.ToLower())
                    {
                        case "paiddocument":
                            _repositoryServices.SaveOrUpdatePaidDocument(paidDocument);
                            break;
                        case "freedocument":
                            var freeDocument = AutoMapper.Mapper.Map(paidDocument, typeof(PaidDocument),
                                typeof(FreeDocument));
                            _repositoryServices.SaveOrUpdateFreeDocument(freeDocument as FreeDocument);
                            break;
                        case "paidvideo":
                            var paidVideo = AutoMapper.Mapper.Map(paidDocument, typeof(PaidDocument),
                                typeof(PaidVideo));
                            _repositoryServices.SaveOrUpdatePaidVideo(paidVideo as PaidVideo);
                            break;
                        case "freevideo":
                            var freeVideo = AutoMapper.Mapper.Map(paidDocument, typeof(PaidDocument),
                                typeof(FreeVideo));
                            _repositoryServices.SaveOrUpdateFreeVideo(freeVideo as FreeVideo);
                            break;
                    }
                    return View("SuccessfullCreation");
                }
                return View("UploadMedia", mediaModel);
            }
            catch (Exception e)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileAccess", string.Format("{0}", e.Message));
                return View("UploadMedia", mediaModel);
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpGet]
        public ActionResult TeachersCalendar()
        {
            GetUIDropdownLists();
            ViewBag.Message = "Teachers Calendar.";
            var calendars = _repositoryServices.GetTeacherCalendar();
            var calendarBookingViewModels = new List<ICalendarBookingViewModel>();
            var classRooms = _repositoryServices.GetClassrooms();
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
                    var student = _repositoryServices.GetStudentById(cal.StudentId);
                    var subject = _repositoryServices.GetSubjectById(cal.SubjectId);
                    var teacher = _repositoryServices.GetTeacherById(cal.TeacherId);
                    var bookingTime = _repositoryServices.GetBookingById(cal.BookingTimeId);

                    if (bookingTime == null) continue;
                    calendarBookingViewModels.Add(new CalendarBookingSelectOrDeleteViewModel { ClassId = cal.ClassroomId, Teacher = teacher, Subject = subject, Student = student, BookingTime = bookingTime });
                }

            }
            ViewBag.CalendarUiList = calendarBookingViewModels.ToArray();

            return View("TeachersCalendar", calendarBookingViewModels.Count() > 0 ? calendarBookingViewModels.ToArray()[calendarBookingViewModels.Count() - 1] : null);
        }
        [HttpGet]
        public ActionResult ManageTeachersCalendar()
        {
            GetUIDropdownLists();
            ViewBag.Message = "Manage Teachers Calendar";
            var calendars = _repositoryServices.GetTeacherCalendar();
            var calendarBookingViewModels = new List<ICalendarBookingViewModel>();
            var classRooms = _repositoryServices.GetClassrooms();
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
                    var student = _repositoryServices.GetStudentById(cal.StudentId);
                    var subject = _repositoryServices.GetSubjectById(cal.SubjectId);
                    var teacher = _repositoryServices.GetTeacherById(cal.TeacherId);
                    var bookingTime = _repositoryServices.GetBookingById(cal.BookingTimeId);

                    if (bookingTime == null) continue;
                    calendarBookingViewModels.Add(new CalendarBookingSelectOrDeleteViewModel { Teacher = teacher, Subject = subject, Student = student, BookingTime = bookingTime, ClassId = cal.ClassroomId });
                }
            }
            ViewBag.CalendarUiList = calendarBookingViewModels.ToArray();
            return View("ManageTeacherCalendar");
        }
        private void GetTeacherBookingTimes(CalendarBookingSelectOrDeleteViewModel bookTeacherTime)
        {
            var teacherCalendar = _repositoryServices.GetTeacherCalendarByBookingId(bookTeacherTime.CalendarBookingId);
            Student student = _repositoryServices.GetStudentById(teacherCalendar.StudentId);
            Subject subject = _repositoryServices.GetSubjectById(teacherCalendar.SubjectId);
            bookTeacherTime.StudentId = (int)student.StudentId;
            bookTeacherTime.SubjectId = (int)subject.SubjectId;
            bookTeacherTime.CalendarBookingId = (int)teacherCalendar.CalendarBookingId;


            var calendarBookingViewModels = new List<CalendarBookingSelectOrDeleteViewModel>();
            student = _repositoryServices.GetStudentById(teacherCalendar.StudentId);
            subject = _repositoryServices.GetSubjectById(teacherCalendar.SubjectId);
            bookTeacherTime.StudentId = (int)student.StudentId;
            bookTeacherTime.SubjectId = (int)subject.SubjectId;
            bookTeacherTime.CalendarBookingId = (int)teacherCalendar.CalendarBookingId;

            var classRooms = _repositoryServices.GetClassrooms();
            var calendars = _repositoryServices.GetTeacherCalendar();
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
                student = _repositoryServices.GetStudentById(cal.StudentId);
                subject = _repositoryServices.GetSubjectById(cal.SubjectId);
                var teacher = _repositoryServices.GetTeacherById(cal.TeacherId);
                var bookingTime = _repositoryServices.GetBookingById(cal.BookingTimeId);

                if (bookingTime == null || teacher.TeacherId != bookTeacherTime.TeacherId) continue;
                calendarBookingViewModels.Add(new CalendarBookingSelectOrDeleteViewModel { Teacher = teacher, Subject = subject, Student = student, BookingTime = bookingTime, ClassId = cal.ClassroomId });
            }
            ViewBag.CalendarUiList = calendarBookingViewModels.ToArray();
            ModelState.Clear();
        }
        [HttpPost]
        public ActionResult ManageTeachersCalendarSelectOrDelete(TeacherCalendarSelectOrDeleteViewModel bookingTimeViewModel)
        {
            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();
            ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);
            var teacherCalendar =
                _repositoryServices.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);

            if (ModelState.IsValid)
            {
                ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);
                if (!string.IsNullOrEmpty(bookingTimeViewModel.Select))
                {
                    ModelState.Clear();
                    return View("ManageTeacherCalendar", new TeacherCalendarSelectOrDeleteViewModel
                    {
                        CalendarBookingId = teacherCalendar.CalendarBookingId,
                        ClassId = teacherCalendar.ClassId,
                        Description = teacherCalendar.Description,
                        StudentFullName = teacherCalendar.StudentFullName,
                        StudentId = teacherCalendar.StudentId,
                        TeacherId = teacherCalendar.TeacherId,
                        TeacherFullName = teacherCalendar.TeacherFullName,
                        SubjectId = teacherCalendar.SubjectId,
                        BookingTimes = new BookingTimeString[] { new BookingTimeString { StartTime = teacherCalendar.BookingTime.StartTime.ToString("yyyy-MM-dd HH:mm"), EndTime = teacherCalendar.BookingTime.EndTime.ToString("yyyy-MM-dd HH:mm") } }

                    });
                }
                if (bookingTimeViewModel.Delete != null)
                {
                    teacherCalendar =
                        _repositoryServices.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);
                    _repositoryServices.DeleteTeacherCalendarByBooking(teacherCalendar);
                    return View("SuccessfullCreation");
                }
            }
            return View("ManageTeacherCalendar", bookingTimeViewModel);
        }
        [HttpPost]
        public ActionResult ManageTeachersCalendarCreate(TeacherCalendarCreateViewModel bookingTimeViewModel)
        {
            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();
            ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);

            if (ModelState.IsValid)
            {
                ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);
                var teacherCalendar =
                    _repositoryServices.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);
                Teacher teacher = _repositoryServices.GetTeacherById(bookingTimeViewModel.TeacherId);
                Student student = _repositoryServices.GetStudentById(bookingTimeViewModel.StudentId);
                Subject subject = _repositoryServices.GetSubjectById(bookingTimeViewModel.SubjectId);
                var bookingTimeId = teacherCalendar.BookingTime.BookingTimeId;
                foreach (var bookingTime in bookingTimeViewModel.BookingTimes)
                {
                    _repositoryServices.SaveOrUpdateBooking(teacher, student, subject, new BookingTime { BookingTimeId = bookingTimeId, StartTime = DateTime.Parse(bookingTime.StartTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }), EndTime = DateTime.Parse(bookingTime.EndTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }) },
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
                ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);
                //emailService.SendEmail(new TicketMasterEmailMessage {EmailFrom= student.EmailAddress, EmailMessage = html,EmailTo = new List<string> {student.EmailAddress}, Subject = "Teacher Assistant's Booking Time Schedule"});

                var message = new TicketMasterEmailMessage { EmailFrom = ConfigurationManager.AppSettings["BusinessEmail"], EmailTo = new List<string> { student.EmailAddress }, Subject = "Teacher Assistant's Booking Time Schedule", EmailMessage = html };
                emailService.SendEmail(message);
                return View("SuccessfullCreation");
            }
            return View("ManageTeacherCalendar", bookingTimeViewModel);
        }
        [HttpPost]
        public ActionResult ManageTeachersCalendarUpdate(TeacherCalendarUpdateViewModel bookingTimeViewModel)
        {

            ViewBag.Message = "Book Teacher Time.";
            GetUIDropdownLists();

            ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);
            if (ModelState.IsValid)
            {
                ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);
                var teacherCalendar =
                    _repositoryServices.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);
                Teacher teacher = _repositoryServices.GetTeacherById(bookingTimeViewModel.TeacherId);
                Student student = _repositoryServices.GetStudentById(bookingTimeViewModel.StudentId);
                Subject subject = _repositoryServices.GetSubjectById(bookingTimeViewModel.SubjectId);
                var bookingTimeId = teacherCalendar.BookingTime.BookingTimeId;
                foreach (var bookingTime in bookingTimeViewModel.BookingTimes)
                {
                    _repositoryServices.SaveOrUpdateBooking(teacher, student, subject, new BookingTime { BookingTimeId = bookingTimeId, StartTime = DateTime.Parse(bookingTime.StartTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }), EndTime = DateTime.Parse(bookingTime.EndTime, new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd HH:mm" }) },
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
                ViewBag.CalendarUiList = GetCalendarListData(bookingTimeViewModel);
                //emailService.SendEmail(new TicketMasterEmailMessage {EmailFrom= student.EmailAddress, EmailMessage = html,EmailTo = new List<string> {student.EmailAddress}, Subject = "Teacher Assistant's Booking Time Schedule"});

                var message = new TicketMasterEmailMessage { EmailFrom = ConfigurationManager.AppSettings["BusinessEmail"], EmailTo = new List<string> { student.EmailAddress }, Subject = "Teacher Assistant's Booking Time Schedule", EmailMessage = html };
                emailService.SendEmail(message);
                return View("SuccessfullCreation");
            }
            return View("ManageTeacherCalendar", bookingTimeViewModel);
        }
        [HttpGet]
        public ActionResult AssignmentAndSubmissions()
        {
            try
            {
                var submissions = _repositoryServices.GetCurrentAssignmentsSubmissions().Select(p =>
                {
                    return new AssignmentSubmissionSelectOrDeleteViewModel
                    {
                        AssignmentId = p.AssignmentId,
                        AssignmentSubmissionId = p.AssignmentSubmissionId,
                        DateSubmitted = p.DateSubmitted,
                        AssignmentName = p.AssignmentName,
                        Notes = p.Notes,
                        DateDue = p.DateDue,
                        Grade = p.Grade,
                        FilePath = p.FilePath,
                        StudentId = p.StudentId,
                        SubjectId = p.SubjectId,
                        TeacherId = p.TeacherId,
                        StudentRole = p.StudentRole,
                        IsSubmitted = p.IsSubmitted,
                        CourseId = p.CourseId
                    };
                });

                return View("AssignmentAndSubmissions", submissions.ToArray());
            }
            catch
            {
                return View("AssignmentAndSubmissions", new AssignmentSubmissionSelectOrDeleteViewModel[] { });
            }
        }
        private CalendarBookingSelectOrDeleteViewModel[] GetCalendarListData(ITeacherCalendarViewModel bookingTimeViewModel)
        {

            var calendarBookingViewModels = new List<CalendarBookingSelectOrDeleteViewModel>();
            var calendar = _repositoryServices.GetTeacherCalendarByBookingId(bookingTimeViewModel.CalendarBookingId);
            Student student = _repositoryServices.GetStudentById(calendar.StudentId);
            Subject subject = _repositoryServices.GetSubjectById(calendar.SubjectId);
            bookingTimeViewModel.StudentId = (int)student.StudentId;
            bookingTimeViewModel.SubjectId = (int)subject.SubjectId;
            bookingTimeViewModel.CalendarBookingId = calendar.CalendarBookingId;
            ModelState.Clear();


            var calendars = _repositoryServices.GetTeacherCalendar();
            var classRooms = _repositoryServices.GetClassrooms();
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
                student = _repositoryServices.GetStudentById(cal.StudentId);
                subject = _repositoryServices.GetSubjectById(cal.SubjectId);
                var teacher = _repositoryServices.GetTeacherByName(User.Identity.Name);
                var bookingTime = _repositoryServices.GetBookingById(cal.BookingTimeId);

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
            return calendarBookingViewModels.ToArray();
        }
        private List<SelectListItem> GetAllQAHelpRequestsForTeacher(string email)
        {
            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "Pick Product Item", Value = 0.ToString() });
            var teacher = _repositoryServices.GetTeacherByName(email)?? _repositoryServices.GetTeacherList().First();
            productList.AddRange(_repositoryServices.GetQARequestList().Where(p=> p.TeacherId == teacher.TeacherId).Select(p => new SelectListItem { Text = p.Description, Value = p.QAHelpRequestId.ToString() }).ToList());
           
            return productList;
        }
        private List<SelectListItem> GetFilteredQASelectList(IEnumerable<QAHelpRequest> qaItems)
        {

            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "Pick Product Item", Value = 0.ToString() });
            if (qaItems!=null && qaItems.Any())
            {
                productList.AddRange(qaItems.Select(p => new SelectListItem { Text = p.Description, Value = p.QAHelpRequestId.ToString() }).ToList());
            }
            return productList;
        }

        [HttpGet]
        public ActionResult ManageQAHelpRequest()
        {
            GetUIDropdownLists();
            ViewBag.QAHelpRequestList = GetAllQAHelpRequestsForTeacher(User.Identity.Name);
            return View();
        }


        [HttpPost]
        public ActionResult ManageQAHelpRequest(QAHelpRequestViewModel qaHelpRequestViewModel)
        {
            GetUIDropdownLists();
            ViewBag.QAHelpRequestList = GetAllQAHelpRequestsForTeacher(User.Identity.Name);

            if (ModelState.IsValid)
            {
                _repositoryServices.SaveOrUpdateQAHelpRequests(new QAHelpRequest
                {
                    QAHelpRequestId = qaHelpRequestViewModel.QAHelpRequestId,
                    Description = qaHelpRequestViewModel.Description,
                    EndTime = qaHelpRequestViewModel.EndTime,
                    StartTime = qaHelpRequestViewModel.StartTime,
                    IsScheduled = qaHelpRequestViewModel.IsScheduled,
                    StudentId = qaHelpRequestViewModel.StudentId,
                    StudentRole = qaHelpRequestViewModel.StudentRole,
                    SubjectId = qaHelpRequestViewModel.SubjectId,
                    TeacherId = qaHelpRequestViewModel.TeacherId
                });

                //Add to Calendar

                _repositoryServices.SaveOrUpdateBooking(_repositoryServices.GetTeacherById(qaHelpRequestViewModel.TeacherId),
                    _repositoryServices.GetStudentById(qaHelpRequestViewModel.StudentId),
                    _repositoryServices.GetSubjectById(qaHelpRequestViewModel.SubjectId),
                    new BookingTime { StartTime = qaHelpRequestViewModel.StartTime, EndTime = qaHelpRequestViewModel.EndTime }, qaHelpRequestViewModel.Description);
                var student = _repositoryServices.GetStudentById(qaHelpRequestViewModel.StudentId);
                var teacher = _repositoryServices.GetTeacherById(qaHelpRequestViewModel.TeacherId);
                var subject = _repositoryServices.GetSubjectById(qaHelpRequestViewModel.SubjectId);
                _emailService.SendEmail(new TicketMasterEmailMessage { EmailFrom = ConfigurationManager.AppSettings["BusinessEmail"], Subject = $"Teacher Confirmation of Help Time ({qaHelpRequestViewModel.StartTime.ToString("dd-MM-yyyy HH:mm")}), from teacher {teacher.FirsName} {teacher.LastName} in subject: { subject.SubjectName } and Role {qaHelpRequestViewModel.StudentRole}", EmailTo = new List<string> { teacher.EmailAddress, student.EmailAddress }, EmailMessage = $"Teacher confirming Help from {teacher.FirsName } in subject: { subject.SubjectName } for student: {student.StudentFirsName} {student.StudentLastName} in Role {qaHelpRequestViewModel.StudentRole} with the confirmed Help time here; Start Time at: {qaHelpRequestViewModel.StartTime.ToString("dd-MM-yyyy HH:mm")} and End Time {qaHelpRequestViewModel.EndTime.ToString("dd-MM-yyyy HH:mm")}. Please note this time has been booked in my calendar which you can view from your portal. \r\nKindest Regards\r\nMartinLayooInc Team." });
                return View("_SuccessfullCreation", qaHelpRequestViewModel);
            }
            return View(qaHelpRequestViewModel);
        }

        [HttpGet]
        public JsonResult QAHelpRequest(int qAHelpRequestId)
        {
            var payLoad = _repositoryServices.GetQARequestId(qAHelpRequestId);
            return Json(payLoad, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ManageProducts()
        {
            var products = _repositoryServices.GetProductsList();
            ViewBag.ProductIdsList = GetProductList();
            return View("ManageProducts");
        }
        [HttpPost]
        public ViewResult ManageProductsSelectOrDelete(ProductSelectOrDeleteViewModel productModel)
        {
            var products = _repositoryServices.GetProductsList();
            ViewBag.ProductIdsList = GetProductList();
            if (ModelState.IsValid)
            {
                var shopProd = new SHOP_PRODS
                {
                    prodName = productModel.ProductName,
                    prodDesc = productModel.ProductDescription,
                    prodPrice = productModel.ProductPrice,
                    prodId = productModel.ProductId,
                    DocumentId = productModel.DocumentId
                };

                if (productModel.Select != null)
                {
                    var prod = _repositoryServices.GetProductById(productModel.ProductId);
                    var actProd = Mapper.Map(prod, typeof(SHOP_PRODS), typeof(ProductSelectOrDeleteViewModel)) as ProductSelectOrDeleteViewModel;
                    actProd.DocumentType = prod.IsPaidDocument ? 0 : prod.IsPaidVideo ? 1 : -1;
                    ModelState.Clear();
                    return View("ManageProducts", actProd);
                }
                if (productModel.Delete != null)
                {
                    _repositoryServices.DeleteProduct(shopProd);
                    return View("SuccessfullCreation");
                }
            }
            return View("ManageProducts", productModel);
        }
        [HttpPost]
        public ViewResult ManageProductsCreate(ProductCreateViewModel productModel)
        {
            var products = _repositoryServices.GetProductsList();
            ViewBag.ProductIdsList = GetProductList();
            if (ModelState.IsValid)
            {
                var shopProd = new SHOP_PRODS
                {
                    prodName = productModel.ProductName,
                    prodDesc = productModel.ProductDescription,
                    prodPrice = productModel.ProductPrice,
                    prodId = productModel.ProductId,
                    DocumentId = productModel.DocumentId
                };

                if (shopProd != null && productModel.DocumentType != -1)
                {
                    shopProd.IsPaidDocument = productModel.DocumentType == 0;
                    shopProd.IsPaidVideo = productModel.DocumentType == 1;
                }
                _repositoryServices.SaveOrUpdate(shopProd);
                return View("SuccessfullCreation");
            }
            return View("ManageProducts", productModel);
        }
        [HttpPost]
        public ViewResult ManageProductsUpdate(ProductUpdateViewModel productModel)
        {

            var products = _repositoryServices.GetProductsList();
            ViewBag.ProductIdsList = GetProductList();
            if (ModelState.IsValid)
            {
                var shopProd = new SHOP_PRODS
                {
                    prodName = productModel.ProductName,
                    prodDesc = productModel.ProductDescription,
                    prodPrice = productModel.ProductPrice,
                    prodId = productModel.ProductId,
                    DocumentId = productModel.DocumentId
                };

                if (shopProd != null && productModel.DocumentType != -1)
                {
                    shopProd.IsPaidDocument = productModel.DocumentType == 0;
                    shopProd.IsPaidVideo = productModel.DocumentType == 1;
                }
                _repositoryServices.SaveOrUpdate(shopProd);
                return View("SuccessfullCreation");
            }
            return View("ManageProducts", productModel);
        }
        [HttpGet]
        public JsonResult GetPaidDocuments(int paidDocument)
        {
            switch (paidDocument)
            {
                case 0:
                    //PaidDocuemnts
                    var paidDocs = _repositoryServices.GetPaidDocuments();

                    return Json(paidDocs.Select<PaidDocument, dynamic>(p => new { DocumentId = p.PaidDocumentId, DocumentName = p.FilePath.Substring(p.FilePath.LastIndexOf("/")) }).ToArray(), JsonRequestBehavior.AllowGet);
                case 1:
                    //PaidVideos
                    var paidVids = _repositoryServices.GetPaidVideos();
                    return Json(paidVids.Select<PaidVideo, dynamic>(p => new { DocumentId = p.PaidVideoId, DocumentName = p.FilePath.Substring(p.FilePath.LastIndexOf("/")) }).ToArray(), JsonRequestBehavior.AllowGet);
                default:
                    return null;
            }
        }
        [HttpGet]
        public ActionResult CreateRoles()
        {
            return View("CreateRoles");
        }

        [HttpPost]
        public ActionResult CreateRoles(UserRoleViewModel role)
        {
            try
            {
                if (string.IsNullOrEmpty(role.RoleName))
                {
                    ModelState.AddModelError("RoleName", "Role name is required");
                    return View(role);
                }
                if (ModelState.IsValid && !Roles.RoleExists(role.RoleName))
                {
                    Roles.CreateRole(role.RoleName);
                    return View("SuccessfullCreation");
                }
                else
                {
                    return View("CreateRoles");
                }
            }
            catch (Exception ex)
            {
                return View("CreateRoles");
            }
        }

        private bool AssignRoles(string user, string role)
        {
            try
            {
                if (!Roles.IsUserInRole(user, role))
                    Roles.AddUserToRole(user, role);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool RemoveRole(string role)
        {
            try
            {
                Roles.DeleteRole(role);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult AssignUserRole()
        {
            ViewBag.RoleName = GetRolesSelectList();
            ViewBag.CourseList = GetAllCourses();
            return View("AssignUserRole");
        }



        [HttpPost]
        public ActionResult AssignUserRole(Models.UserRoleViewModel userInRole)
        {

            ViewBag.CourseList = GetAllCourses();
            ViewBag.RoleName = GetRolesSelectList();
            if (string.IsNullOrEmpty(userInRole.RoleName) || string.IsNullOrEmpty(userInRole.Username))
            {
                ModelState.AddModelError("RoleName", "Role name and Username are required");
                return View(userInRole);
            }
            if (AssignRoles(userInRole.Username, userInRole.RoleName))
            {
                return View("SuccessfullCreation");
            }
            return View("AssignUserRole", userInRole);
        }
        [HttpGet]

        public ActionResult RemoveUserFromRole()
        {
            ViewBag.RoleName = GetRolesSelectList();
            return View("RemoveUserFromRole");
        }

        [HttpPost]
        public ActionResult RemoveUserFromRole(Models.UserRoleViewModel userInRole)
        {
            if (string.IsNullOrEmpty(userInRole.RoleName) || string.IsNullOrEmpty(userInRole.Username))
            {
                ModelState.AddModelError("RoleName", "Role name and Username are required");
                return View(userInRole);
            }
            var rolesList = new List<string>();

            foreach (var role in Roles.GetAllRoles())
            {
                rolesList.Add(role);
            }
            ViewBag.RoleName = new SelectList(rolesList);

            if (RemoveUserFromRole(userInRole.Username, userInRole.RoleName))
            {
                return View("SuccessfullCreation");
            }
            return View("RemoveUserFromRole", userInRole);
        }
        [HttpGet]
        public ActionResult AddGradesToSubmissions()
        {
            GetUIDropdownLists();
            ViewBag.DateSubmittedString = DateTime.Now;
            ViewBag.DateDueString = DateTime.Now;
            ViewBag.AssignmentList = GetCurrentAssignmentList();
            ViewBag.UngragedAssignmentSubmissionList = GetAllAssignmentSubmissionsList();
            return View();
        }

        [HttpPost]
        public ActionResult AddGradesToSubmissionsSelectOrDelete(AssignmentSubmissionSelectOrDeleteViewModel assignmentSubmissions)
        {
            ViewBag.DateSubmittedString = DateTime.Now;
            ViewBag.DateDueString = DateTime.Now;
            GetUIDropdownLists();
            ViewBag.AssignmentList = GetCurrentAssignmentList();
            ViewBag.UngragedAssignmentSubmissionList = GetAllAssignmentSubmissionsList();
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(assignmentSubmissions.Graded))
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetGradedAssignmentSubmissionsList();
                }
                else if (!string.IsNullOrEmpty(assignmentSubmissions.UnGraded))
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetSubmittedUngradedAssignmentSubmissionsList();
                }
                else
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetAllAssignmentSubmissionsList();
                }

                Assignment assignment = _repositoryServices.GetAssignmentById(assignmentSubmissions.AssignmentId);
                AssignmentSubmission submission = _repositoryServices.GetAssignmentSubmissionsById(assignmentSubmissions.AssignmentSubmissionId);
                if (assignmentSubmissions.Select != null)
                {
                    var assSub = new AssignmentSubmissionSelectOrDeleteViewModel
                    {
                        AssignmentSubmissionId = submission.AssignmentSubmissionId,
                        AssignmentId = submission.AssignmentId,
                        AssignmentName = assignment.AssignmentName,
                        DateDue = submission.DateDue,
                        DateSubmitted = submission.DateSubmitted,
                        FilePath = submission.FilePath,
                        Grade = submission.Grade,
                        GradeNumeric = submission.GradeNumeric,
                        StudentId = submission.StudentId,
                        TeacherId = submission.TeacherId,
                        SubjectId = assignment.SubjectId,
                        IsSubmitted = submission.IsSubmitted,
                        StudentRole = submission.StudentRole,
                        Notes = submission.Notes
                    };
                    ViewBag.DateSubmittedString = assSub.DateSubmitted;
                    ViewBag.DateDueString = assSub.DateDue;
                    ModelState.Clear();
                    return View("AddGradesToSubmissions", assSub);
                }
                else if (assignmentSubmissions.Delete != null)
                {
                    _repositoryServices.DeleteAssignmentSubmissiongById(assignmentSubmissions.AssignmentSubmissionId);

                    return View("SuccessfullCreation");
                }
            }
            return View("AddGradesToSubmissions", assignmentSubmissions);
        }
        [HttpPost]
        public ActionResult AddGradesToSubmissionsCreate(AssignmentSubmissionCreateViewModel assignmentSubmissions)
        {
            ViewBag.DateSubmittedString = DateTime.Now;
            ViewBag.DateDueString = DateTime.Now;
            GetUIDropdownLists();
            ViewBag.AssignmentList = GetCurrentAssignmentList();
            ViewBag.UngragedAssignmentSubmissionList = GetAllAssignmentSubmissionsList();
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(assignmentSubmissions.Graded))
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetGradedAssignmentSubmissionsList();
                }
                else if (!string.IsNullOrEmpty(assignmentSubmissions.UnGraded))
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetSubmittedUngradedAssignmentSubmissionsList();
                }
                else
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetAllAssignmentSubmissionsList();
                }

                Assignment assignment = _repositoryServices.GetAssignmentById(assignmentSubmissions.AssignmentId);


                _repositoryServices.SaveOrUpdateAssignmentSubmissions(new AssignmentSubmission
                {
                    AssignmentId = assignmentSubmissions.AssignmentId,
                    DateDue = assignmentSubmissions.DateDue,
                    DateSubmitted = assignmentSubmissions.DateSubmitted,
                    FilePath = assignmentSubmissions.FilePath,
                    Grade = assignmentSubmissions.Grade,
                    GradeNumeric = assignmentSubmissions.GradeNumeric,
                    AssignmentName = assignment.AssignmentName,
                    IsSubmitted = assignmentSubmissions.IsSubmitted,
                    StudentId = assignmentSubmissions.StudentId,
                    TeacherId = assignmentSubmissions.TeacherId,
                    SubjectId = assignmentSubmissions.SubjectId,
                    StudentRole = assignment.StudentRole,
                    Notes = assignmentSubmissions.Notes
                });
                return View("SuccessfullCreation");
            }
            return View("AddGradesToSubmissions", assignmentSubmissions);
        }
        [HttpPost]
        public ActionResult AddGradesToSubmissionsUpdate(AssignmentSubmissionUpdateViewModel assignmentSubmissions)
        {
            ViewBag.DateSubmittedString = DateTime.Now;
            ViewBag.DateDueString = DateTime.Now;
            GetUIDropdownLists();
            ViewBag.AssignmentList = GetCurrentAssignmentList();
            ViewBag.UngragedAssignmentSubmissionList = GetAllAssignmentSubmissionsList();
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(assignmentSubmissions.Graded))
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetGradedAssignmentSubmissionsList();
                }
                else if (!string.IsNullOrEmpty(assignmentSubmissions.UnGraded))
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetSubmittedUngradedAssignmentSubmissionsList();
                }
                else
                {
                    ViewBag.UngragedAssignmentSubmissionList = GetAllAssignmentSubmissionsList();
                }

                Assignment assignment = _repositoryServices.GetAssignmentById(assignmentSubmissions.AssignmentId);


                _repositoryServices.SaveOrUpdateAssignmentSubmissions(new AssignmentSubmission
                {
                    AssignmentId = assignmentSubmissions.AssignmentId,
                    DateDue = assignmentSubmissions.DateDue,
                    DateSubmitted = assignmentSubmissions.DateSubmitted,
                    FilePath = assignmentSubmissions.FilePath,
                    Grade = assignmentSubmissions.Grade,
                    GradeNumeric = assignmentSubmissions.GradeNumeric,
                    AssignmentName = assignment.AssignmentName,
                    IsSubmitted = assignmentSubmissions.IsSubmitted,
                    StudentId = assignmentSubmissions.StudentId,
                    TeacherId = assignmentSubmissions.TeacherId,
                    SubjectId = assignmentSubmissions.SubjectId,
                    StudentRole = assignment.StudentRole,
                    Notes = assignmentSubmissions.Notes
                });
                return View("SuccessfullCreation");
            }
            return View("AddGradesToSubmissions", assignmentSubmissions);
        }
        [HttpGet]
        public ViewResult ViewGraphsAndReports()
        {
            GetUIDropdownLists();
            return View("ReportingAndPerformance");
        }
        private bool RemoveUserFromRole(string username, string roleName)
        {
            try
            {
                if (Roles.IsUserInRole(roleName))
                    Roles.RemoveUserFromRole(username, roleName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private List<SelectListItem> GetAssignmentSubmissionsListItems(IEnumerable<AssignmentSubmission> submissions)
        {
            var listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "Pick an Assignment Submission", Value = 0.ToString() });
            listItems.AddRange(submissions.Select(p => new SelectListItem { Text = p.AssignmentName, Value = p.AssignmentSubmissionId.ToString() }).ToList());
            return listItems;
        }

        private List<SelectListItem> GetClassroomList()
        {
            var classRoomList = new List<SelectListItem>();
            var classrooms = _repositoryServices.GetClassrooms();

            classRoomList.Add(new SelectListItem { Text = "Pick a Class", Value = 0.ToString() });
            classRoomList.AddRange(classrooms.Select(p => new SelectListItem { Text = string.Format("[{0}] Class", p.ClassroomId), Value = p.ClassroomId.ToString() }).ToList());
            return classRoomList;
        }
        private List<SelectListItem> GetStudentsList()
        {
            var students = _repositoryServices.GetStudentList();
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
            var subjects = _repositoryServices.GetSubjectList();
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
            var teachers = _repositoryServices.GetTeacherList();
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
            var teacherCalendars = _repositoryServices.GetTeacherCalendar();
            calendarList.Add(new SelectListItem { Text = "Pick a Canlendar By Id and Teacher", Value = 0.ToString() });

            foreach (var cal in teacherCalendars)
            {
                calendarList.Add(new SelectListItem { Text = string.Format("[CalendarID:{0}] By ", cal.CalendarBookingId.ToString()) + GetTeacherList().Single(p => Convert.ToInt32(p.Value) == cal.TeacherId).Text, Value = cal.CalendarBookingId.ToString() });
            }
            return calendarList;
        }
        private List<SelectListItem> GetRolesSelectList()
        {
            var rolesList = new List<SelectListItem>();

            rolesList.Add(new SelectListItem { Text = "Pick a Role", Value = "" });
            foreach (var role in Roles.GetAllRoles())
            {
                rolesList.Add(new SelectListItem { Text = role, Value = role });
            }

            return rolesList;
        }
        private List<SelectListItem> GetStudentResourcesList()
        {
            var resourceList = new List<SelectListItem>();

            resourceList.Add(new SelectListItem { Text = "Pick a Resource", Value = "" });
            foreach (StudentResource resource in _repositoryServices.GetAllStudentResources())
            {
                resourceList.Add(new SelectListItem { Text = resource.StudentResourceName, Value = resource.StudentResourceId.ToString() });
            }

            return resourceList;
        }

        public IList<SelectListItem> GetQARequestList()
        {
            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "Pick Product Item", Value = 0.ToString() });
            var results = _repositoryServices.GetQARequestList();
            if (results.Any())
            {
                productList.AddRange(results.Select(p => new SelectListItem { Text = p.Description.Substring(0, 20), Value = p.QAHelpRequestId.ToString() }).ToList());
            }
            return productList;

        }
        private List<SelectListItem> GetProductList()
        {
            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "Pick Product Item", Value = 0.ToString() });

            productList.AddRange(_repositoryServices.GetProductsList().Select(p => new SelectListItem { Text = p.prodName, Value = p.prodId.ToString() }).ToList());
            return productList;
        }
        private void GetUIDropdownLists()
        {
            ViewBag.StudentList = GetStudentsList();
            ViewBag.StudentResourcesList = GetStudentResourcesList();
            ViewBag.TeacherList = GetTeacherList();
            ViewBag.RoleList = GetRolesSelectList();
            ViewBag.SubjectList = GetSubjectList();
            ViewBag.CalendarBookingList = GetCalendarList();
            ViewBag.ClassroomList = GetClassroomList();
            ViewBag.GetAllAssignmentsList = GetAllAssignmentsList();
            ViewBag.CourseList = GetAllCourses();
        }

        private List<SelectListItem> GetAllAssignmentsList()
        {
            var assingnmentList = new List<SelectListItem>();
            assingnmentList.Add(new SelectListItem { Text = "Pick Assignment", Value = 0.ToString() });

            assingnmentList.AddRange(_repositoryServices.GetAllAssignments().Select(p => new SelectListItem { Text = p.AssignmentName, Value = p.AssignmentId.ToString() }).ToList());
            return assingnmentList;
        }
        private List<SelectListItem> GetAllCourses()
        {
            var courseList = new List<SelectListItem>();
            courseList.Add(new SelectListItem { Text = "Pick Course", Value = 0.ToString() });

            courseList.AddRange(_repositoryServices.GetAllCourses().Select(p => new SelectListItem { Text = p.CourseName, Value = p.CourseId.ToString() }).ToList());
            return courseList;
        }

        private List<SelectListItem> GetCurrentAssignmentList()
        {
            var assingnmentList = new List<SelectListItem>();
            assingnmentList.Add(new SelectListItem { Text = "Pick Assignment", Value = 0.ToString() });

            assingnmentList.AddRange(_repositoryServices.GetCurrentAssignments().Select(p => new SelectListItem { Text = p.AssignmentName, Value = p.AssignmentId.ToString() }).ToList());
            return assingnmentList;
        }
        private List<SelectListItem> GetSubmittedUngradedAssignmentSubmissionsList()
        {
            var assingnmentList = new List<SelectListItem>();
            assingnmentList.Add(new SelectListItem { Text = "Pick Assignment Submissions", Value = 0.ToString() });

            assingnmentList.AddRange(_repositoryServices.GetUngradedSubmittedAssignments().Select(p => new SelectListItem { Text = p.AssignmentName, Value = p.AssignmentSubmissionId.ToString() }).ToList());
            return assingnmentList;
        }
        private List<SelectListItem> GetAllAssignmentSubmissionsList()
        {
            var assingnmentList = new List<SelectListItem>();
            assingnmentList.Add(new SelectListItem { Text = "Pick Assignment Submissions", Value = 0.ToString() });

            assingnmentList.AddRange(_repositoryServices.GetCurrentAssignmentsSubmissions().Select(p => new SelectListItem { Text = p.AssignmentName, Value = p.AssignmentSubmissionId.ToString() }).ToList());
            return assingnmentList;
        }

        private List<SelectListItem> GetGradedAssignmentSubmissionsList()
        {
            var assingnmentList = new List<SelectListItem>();
            assingnmentList.Add(new SelectListItem { Text = "Pick Assignment Submissions", Value = 0.ToString() });

            assingnmentList.AddRange(_repositoryServices.GetGradedAssignmentsSubmissions().Select(p => new SelectListItem { Text = p.AssignmentName, Value = p.AssignmentSubmissionId.ToString() }).ToList());
            return assingnmentList;
        }
        [HttpGet]
        public JsonResult GetMediaDocumentIdsFor(string mediaType, string role)
        {
            switch (mediaType.ToLower())
            {
                case "paiddocument":
                    var list = _repositoryServices.GetPaidDocuments(role)
                   .Select(p => new SelectListItem { Text = p.PaidDocumentId.ToString() + " " + p.FilePath.Substring(p.FilePath.LastIndexOf("/") + 1), Value = p.PaidDocumentId.ToString() }).ToList();
                    list.Add(new SelectListItem { Text = "Pick DocumentId", Value = "-1" });
                    return Json(list.OrderBy(p => p.Value), JsonRequestBehavior.AllowGet);

                case "freedocument":
                    list = _repositoryServices.GetFreeDocuments(role)
                         .Select(p => new SelectListItem { Text = p.FreeDocumentId.ToString() + " " + p.FilePath.Substring(p.FilePath.LastIndexOf("/") + 1), Value = p.FreeDocumentId.ToString() }).ToList();
                    list.Add(new SelectListItem { Text = "Pick DocumentId", Value = "-1" });
                    return Json(list.OrderBy(p => p.Value), JsonRequestBehavior.AllowGet);
                case "paidvideo":
                    list = _repositoryServices.GetPaidVideos(role)
                          .Select(p => new SelectListItem { Text = p.PaidVideoId.ToString() + " " + p.FilePath.Substring(p.FilePath.LastIndexOf("/") + 1), Value = p.PaidVideoId.ToString() }).ToList();
                    list.Add(new SelectListItem { Text = "Pick DocumentId", Value = "-1" });
                    return Json(list.OrderBy(p => p.Value), JsonRequestBehavior.AllowGet);
                case "freevideo":
                    list = _repositoryServices.GetFreeVideos(role)
                          .Select(p => new SelectListItem { Text = p.FreeVideoId.ToString() + " " + p.FilePath.Substring(p.FilePath.LastIndexOf("/") + 1), Value = p.FreeVideoId.ToString() }).ToList();
                    list.Add(new SelectListItem { Text = "Pick DocumentId", Value = "-1" });
                    return Json(list.OrderBy(p => p.Value), JsonRequestBehavior.AllowGet);
                default:
                    return null;
            }
        }
    }
}