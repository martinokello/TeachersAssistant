﻿using System;
using System.Collections.Generic;
using System.Linq;
using TeachersAssistant.Domain.Model.Models;
using TeachersAssistant.Services.Interfaces;
using TeachersAssistant.DataAccess;

namespace TeachersAssistant.Services.Concretes
{
    public class TeachersAssistantRepositoryServices : IRepositoryServicesMarker
    {
        TeachersAssistantUnitOfWork _unitOfWork;

        public TeachersAssistantRepositoryServices() { }
        public TeachersAssistantRepositoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as TeachersAssistantUnitOfWork;
        }
        public void SaveOrUpdateCalendar(CalendarBooking calendarBk)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var calendarBooking = _unitOfWork._calendarBookingRepository.GetById((int)calendarBk.CalendarBookingId);
                calendarBooking.SubjectId = calendarBooking.SubjectId;
                if (calendarBk.ClassId > 0)
                    calendarBooking.ClassId = calendarBk.ClassId;
                _unitOfWork._calendarBookingRepository.Update(calendarBooking);
                _unitOfWork.SaveChanges();
            }
        }
        public CalendarBooking GetTeacherCalendarByBookingId(int teacherId, int bookingId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                CalendarBooking calendarBooking = null;
                calendarBooking = _unitOfWork._calendarBookingRepository.GetAll().SingleOrDefault(p => p.TeacherId == teacherId && p.BookingTimeId == bookingId);
                return calendarBooking;
            }
        }
        public CalendarBooking[] GetTeacherCalendar(int teacherId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var teacherCalendar = _unitOfWork._calendarBookingRepository.GetAll().Where(p => p.TeacherId == teacherId);
                return teacherCalendar.ToArray();
            }
        }
        public CalendarBooking[] GetTeacherCalendar()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var teacherCalendar = _unitOfWork._calendarBookingRepository.GetAll();
                return teacherCalendar.ToArray();
            }
        }
        public void DeleteClassroom(Classroom classRoomModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (classRoomModel.ClassroomId != null && classRoomModel.ClassroomId > 0)
                {
                    var classroom = _unitOfWork._classroomRepository.GetById((int)classRoomModel.ClassroomId);
                    _unitOfWork._classroomRepository.Delete(classroom);
                    _unitOfWork.SaveChanges();
                }
            }
        }
        public SHOP_PRODS GetProductById(int productId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork.TeachersAssistantDbContext.ShopProducts.SingleOrDefault(p => p.prodId == productId);
            }
        }

        public IList<SHOP_PRODS> GetProductsList()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork.TeachersAssistantDbContext.ShopProducts.ToList();
            }
        }
        public IList<QAHelpRequest> GetQARequestList()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._qAHelpRequestRepository.GetAll();
            }
        }
        public QAHelpRequest GetQARequestId(int qaHelpRequestId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._qAHelpRequestRepository.GetById(qaHelpRequestId);
            }
        }
        public void RequestQATime(QAHelpRequest qaHelpRequest)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var request = _unitOfWork._qAHelpRequestRepository.GetById(qaHelpRequest.QAHelpRequestId);
                if (request == null)
                {
                    _unitOfWork._qAHelpRequestRepository.Add(qaHelpRequest);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    _unitOfWork._qAHelpRequestRepository.Update(qaHelpRequest);
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public void ManageClassRoom(Classroom classRoomModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var classroom = _unitOfWork._classroomRepository.GetById((int)classRoomModel.ClassroomId);

                if (classroom == null)
                {
                    _unitOfWork._classroomRepository.Add(classRoomModel);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    if (classroom != null)
                    {
                        classroom.SubjectId = classRoomModel.SubjectId;
                        classroom.TeacherId = classRoomModel.TeacherId;
                        classroom.StudentType = classRoomModel.StudentType;
                        classroom.CalendarId = classRoomModel.CalendarId;
                        _unitOfWork.SaveChanges();
                    }
                }
            }
        }
        public void SaveOrUpdateOrders(Order order)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var ord = _unitOfWork._calendarBookingRepository.DbContextTeachersAssistant.Orders.SingleOrDefault(p => p.orderId == order.orderId);

                if (ord == null)
                {
                    _unitOfWork._calendarBookingRepository.DbContextTeachersAssistant.Orders.Add(order);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    ord.paid_for = order.paid_for;
                    ord.order_date = order.order_date;
                    ord.order_gross = order.order_gross;
                    order.password = order.password;
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public string[] GetNewRegistrants()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);

                var allUsers = dbContext.Database.SqlQuery<string>("select [Email] from [dbo].[AspNetUsers]").ToArray();

                var students = _unitOfWork._studentRepository.GetAll();

                var teachers = _unitOfWork._teacherRepository.GetAll();

                var unionStTeachers = students.Select(p => p.EmailAddress).Union(teachers.Select(q => q.EmailAddress)).ToArray();

                return allUsers.Where(p => !unionStTeachers.Contains(p)).ToArray();
            }
        }

        public void SaveOrUpdateAssignment(Assignment assignment)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var assign = _unitOfWork._assignmentRepository.GetById(assignment.AssignmentId);

                if(assign == null)
                {
                    _unitOfWork._assignmentRepository.Add(assignment);
                }
                else
                {
                    assign.AssignmentName = assignment.AssignmentName;
                    assign.DateAssigned = assignment.DateAssigned;
                    assign.DateDue = assignment.DateDue;
                    assign.Description = assignment.Description;
                    assign.FilePath = assignment.FilePath;
                    assign.SubjectId = assignment.SubjectId;
                    assign.StudentId = assignment.StudentId;
                    assign.StudentRole = assignment.StudentRole;
                }
                _unitOfWork.SaveChanges();
            }
        }

        public Assignment GetAssignmentById(int assignmentId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._assignmentRepository.GetById(assignmentId);
            }
        }

        public void SaveOrUpdateItemOrders(ItemOrder orderItem)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var ordIt = _unitOfWork._calendarBookingRepository.DbContextTeachersAssistant.ItemOrders.SingleOrDefault(p => p.ItemId == orderItem.ItemId);
                if (ordIt == null)
                {
                    _unitOfWork._calendarBookingRepository.DbContextTeachersAssistant.ItemOrders.Add(orderItem);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    ordIt.numberOrdered = orderItem.numberOrdered;
                    ordIt.Order = orderItem.Order;
                    ordIt.order_id_fk = orderItem.order_id_fk;
                    ordIt.username = orderItem.username;
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public void DeleteAssignment(int assignmentId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var ass = _unitOfWork._assignmentRepository.GetById(assignmentId);

                if (ass != null)
                {
                    _unitOfWork._assignmentRepository.Delete(ass);
                    _unitOfWork.SaveChanges();
                }

            }
        }

        public void DeleteStudent(Student studentModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (studentModel.StudentId != null && studentModel.StudentId > 0)
                {
                    var student = _unitOfWork._subjectRepository.GetById((int)studentModel.StudentId);
                    _unitOfWork._subjectRepository.Delete(student);
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public List<SHOP_PRODS> GetShoppingProdsList()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._freeVideoRepository.DbContextTeachersAssistant.ShopProducts.ToList();
            }
        }

        public void ManageStudent(Student studentModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (studentModel.StudentId == null || studentModel.StudentId < 1)
                {
                    _unitOfWork._studentRepository.Add(studentModel);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    var student = _unitOfWork._studentRepository.GetById((int)studentModel.StudentId);
                    if (student != null)
                    {
                        student.StudentType = studentModel.StudentType;
                        student.CourseId = studentModel.CourseId;
                        _unitOfWork.SaveChanges();
                    }
                }
            }
        }

        public void DeleteSubject(Subject subjectModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (subjectModel.SubjectId != null && subjectModel.SubjectId > 0)
                {
                    var subject = _unitOfWork._subjectRepository.GetById((int)subjectModel.SubjectId);
                    _unitOfWork._subjectRepository.Delete(subject);
                    _unitOfWork.SaveChanges();

                }
            }
        }

        public Classroom GetClassroomById(int? classroomId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._classroomRepository.GetById((int)classroomId);
            }
        }

        public void ManageSubject(Subject subjectModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var subject = _unitOfWork._subjectRepository.GetById((int)subjectModel.SubjectId);

                if (subject == null)
                {
                    _unitOfWork._subjectRepository.Add(subjectModel);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    subject.SubjectName = subjectModel.SubjectName;
                    subject.TeacherId = subjectModel.TeacherId;
                    subject.CourseId = subjectModel.CourseId;
                    _unitOfWork.SaveChanges();
                }
            }
        }
        public void DeleteTeacher(Teacher teacherModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (teacherModel.TeacherId != null && teacherModel.TeacherId > 0)
                {
                    var teacher = _unitOfWork._teacherRepository.GetById((int)(int)teacherModel.TeacherId);
                    _unitOfWork._teacherRepository.Delete(teacher);
                    _unitOfWork.SaveChanges();

                }
            }
        }

        public Student GetStudentById(int? studentId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._studentRepository.GetById((int)studentId);
            }
        }
        public Subject GetSubjectById(int? subjectId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._subjectRepository.GetById((int)subjectId);
            }
        }
        public Classroom[] GetClassrooms()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._classroomRepository.GetAll();
            }
        }
        public Teacher GetTeacherById(int? teacherId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._teacherRepository.GetById((int)teacherId);
            }
        }
        public void ManageTeachers(Teacher teacherModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (teacherModel.TeacherId == null || teacherModel.TeacherId < 1)
                {
                    _unitOfWork._teacherRepository.Add(teacherModel);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    var teacher = _unitOfWork._teacherRepository.GetById((int)(int)teacherModel.TeacherId);
                    if (teacher != null)
                    {
                        teacher.EmailAddress = teacherModel.EmailAddress;
                        teacher.FirsName = teacherModel.FirsName;
                        teacher.LastName = teacherModel.LastName;
                        _unitOfWork.SaveChanges();
                    }
                }
            }
        }

        public BookingTime GetBookingById(int bookingTimeId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._bookingTimeRepository.GetById(bookingTimeId);
            }
        }

        public Teacher GetTeacherByName(string name)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._teacherRepository.GetAll().FirstOrDefault(p => p.EmailAddress == name);
            }
        }

        public Student GetStudentByName(string emailAddress)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._studentRepository.GetAll().FirstOrDefault(p => p.EmailAddress == emailAddress);
            }
        }
        public Subject GetSubjectByName(string subjectName)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._subjectRepository.GetAll().FirstOrDefault(p => p.SubjectName.ToLower().Equals(subjectName.ToLower()));
            }
        }

        public List<GroupedResourcesViewModel> GetGroupedResourcesByRoleThenSubject(string roleName)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                IEnumerable<Dictionary<string, IGrouping<string, StudentResource>>> result = null;
                var results = new List<GroupedResourcesViewModel>();
                if (_unitOfWork._studentResourcesRepository.GetAll() != null) { 
                    result = _unitOfWork._studentResourcesRepository.GetAll().GroupBy(gr => gr.RoleName).Select(p =>
                    {
                        var dict = new Dictionary<string, IGrouping<string, StudentResource>>();
                        dict.Add(p.Key, p);
                        return dict;
                    }).ToList();
                }
                if(result!=null)
                foreach (var dict in result)
                {
                    results.Add(new GroupedResourcesViewModel
                    {
                        GroupedByRole = dict.FirstOrDefault().Key,
                        GroupedBySubject = dict.FirstOrDefault().Value.GroupBy(gs => gs.SubjectId).Select(p =>
                        {
                            var dic = new Dictionary<string, IGrouping<int, StudentResource>>();
                            dic.Add(_unitOfWork._subjectRepository.GetById(p.Key).SubjectName, p);
                            return dic;
                        }).ToList(),
                        IndividualResources = dict.FirstOrDefault().Value.GroupBy(gs => gs.StudentResourceId)
                    });
                }
                return results;
            }
        }

        public StudentResource GetStudentResourceById(int studentResourceId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._studentResourcesRepository.GetById(studentResourceId);
            }
        }

        public Subject GetSubjectById(int subjectId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var subject = _unitOfWork._subjectRepository.GetById(subjectId);
                return new Subject { SubjectId = subject.SubjectId, SubjectName = subject.SubjectName, TeacherId = subject.TeacherId, Teacher = subject.Teacher };
            }
        }

        public void SaveOrUpdateBooking(Teacher teacher, Student student, Subject subject, BookingTime bookingTime, string description)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var calendar = _unitOfWork._calendarBookingRepository.GetAll().SingleOrDefault(p => p.BookingTimeId == bookingTime.BookingTimeId);

                if (calendar == null)
                {
                    var bookingTimes = new BookingTime { StartTime = bookingTime.StartTime, EndTime = bookingTime.EndTime };

                    _unitOfWork._bookingTimeRepository.Add(bookingTimes);
                    _unitOfWork.SaveChanges();
                    calendar = new CalendarBooking
                    {
                        StudentId = (int)student.StudentId,
                        SubjectId = (int)subject.SubjectId,
                        TeacherId = (int)teacher.TeacherId,
                        BookingTimeId = (int)bookingTimes.BookingTimeId,
                        Description = description
                    };

                    _unitOfWork._calendarBookingRepository.Add(calendar);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    var bkTime=_unitOfWork._bookingTimeRepository.GetById((int)bookingTime.BookingTimeId);
                    bkTime.StartTime = bookingTime.StartTime;
                    bkTime.EndTime = bookingTime.EndTime;
                    calendar.BookingTime = bkTime;
                    calendar.Description = description;
                    _unitOfWork.SaveChanges();
                }
            }

        }

        public void AddOrUpdateCourse(Course course)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var res = _unitOfWork._courseRepository.GetById(course.CourseId);
                if (res == null)
                {
                    _unitOfWork._courseRepository.Add(course);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    res.CourseName = course.CourseName;
                    res.CourseDescription = course.CourseDescription;
                    _unitOfWork.SaveChanges();
                }
            }
        }
        
        public void DeleteCourse(Course course)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var res = _unitOfWork._courseRepository.GetById(course.CourseId);
                if (res != null)
                {
                    _unitOfWork._courseRepository.Delete(res);
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public Course GetCourseById(int courseId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._courseRepository.GetById(courseId);   
            }
        }

        public void DeleteStudentResource(int studentResourceId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var res = _unitOfWork._studentResourcesRepository.GetById(studentResourceId);
                if(res != null)
                {
                    _unitOfWork._studentResourcesRepository.Delete(res);
                    _unitOfWork.SaveChanges();
                }
            }
        }
        public void SaveOrUpdateFreeVideo(FreeVideo freeVideo)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (freeVideo.FreeVideoId != null && freeVideo.FreeVideoId > 0)
                {
                    //update
                    var result = _unitOfWork._freeVideoRepository.GetById((int)freeVideo.FreeVideoId);
                    if (result != null)
                    {
                        result.RoleName = freeVideo.RoleName;
                        result.FilePath = freeVideo.FilePath;
                        result.SubjectId = freeVideo.SubjectId;
                        _unitOfWork.SaveChanges();
                    }
                }
                else
                {
                    //create
                    _unitOfWork._freeVideoRepository.Add(freeVideo);
                    _unitOfWork.SaveChanges();

                }
            }
        }

        public void SaveOrUpdateStudentResource(StudentResource studentResource)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var stuRes = _unitOfWork._studentResourcesRepository.GetById(studentResource.StudentResourceId);
                if(stuRes == null)
                {
                    _unitOfWork._studentResourcesRepository.Add(studentResource);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    stuRes.SubjectId = studentResource.SubjectId;
                    stuRes.CourseId = studentResource.CourseId;
                    stuRes.FilePath = studentResource.FilePath;
                    stuRes.RoleName = studentResource.RoleName;
                    stuRes.StudentResourceName = studentResource.StudentResourceName;
                    _unitOfWork.SaveChanges(); 
                }
            }
           
        }

        public PaidDocument[] GetPaidDocuments(string role)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._paidDocumentRepository.GetAll().Where(p => p.RoleName.ToLower().Equals(role.ToLower())).ToArray();
            }
        }

        public AssignmentSubmission[] GetCurrentAssignmentsSubmissions(string role, int studentId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._assignmentSubmissionRepository.GetAll().Where(p => p.StudentRole.ToLower().Equals(role.ToLower()) && p.StudentId == studentId).ToArray();
            }
        }

        public FreeDocument[] GetFreeDocuments(string role)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._freeDocumentRepository.GetAll().Where(p => p.RoleName.ToLower().Equals(role.ToLower())).ToArray();
            }
        }
        public PaidVideo[] GetPaidVideos(string role)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._paidVideoRepository.GetAll().Where(p => p.RoleName.ToLower().Equals(role.ToLower())).ToArray();
            }
        }
        public FreeVideo[] GetFreeVideos(string role)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._freeVideoRepository.GetAll().Where(p => p.RoleName.ToLower().Equals(role.ToLower())).ToArray();
            }
        }

        public void SaveOrUpdateAssignmentSubmissions(AssignmentSubmission actualSubmission)
        {
            actualSubmission.IsSubmitted = true;
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);

                var submission = _unitOfWork._assignmentSubmissionRepository.GetById(actualSubmission.AssignmentSubmissionId);
                var assignment= _unitOfWork._assignmentRepository.GetById(actualSubmission.AssignmentId);

                if(submission == null)
                {
                    _unitOfWork._assignmentSubmissionRepository.Add(actualSubmission);
                }
                else
                {
                    submission.AssignmentId = actualSubmission.AssignmentId;
                    submission.DateDue = actualSubmission.DateDue;
                    submission.DateSubmitted = actualSubmission.DateSubmitted;
                    submission.FilePath = actualSubmission.FilePath;
                    submission.StudentId = actualSubmission.StudentId;
                    submission.StudentRole = actualSubmission.StudentRole;
                    submission.AssignmentName = actualSubmission.AssignmentName;
                    submission.TeacherId = actualSubmission.TeacherId;
                    submission.SubjectId = actualSubmission.SubjectId;
                    submission.IsSubmitted = true;
                    submission.Grade = actualSubmission.Grade;
                    submission.GradeNumeric = actualSubmission.GradeNumeric;
                    submission.Notes = actualSubmission.Notes;
                }
                assignment.IsSubmitted = true;
                _unitOfWork.SaveChanges();
            }
        }
        public void SaveOrUpdatePaidVideo(PaidVideo paidVideo)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (paidVideo.PaidVideoId != null && paidVideo.PaidVideoId > 0)
                {
                    //update
                    var result = _unitOfWork._paidVideoRepository.GetById((int)paidVideo.PaidVideoId);
                    if (result != null)
                    {
                        result.RoleName = paidVideo.RoleName;
                        result.FilePath = paidVideo.FilePath;
                        result.SubjectId = paidVideo.SubjectId;
                        _unitOfWork.SaveChanges();
                    }
                }
                else
                {
                    //create
                    _unitOfWork._paidVideoRepository.Add(paidVideo);
                    _unitOfWork.SaveChanges();

                }
            }
        }

        public PaidDocument GetPaidDocumentById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._paidDocumentRepository.GetById((int)mediaId);
            }
        }

        public FreeDocument GetFreeDocumentById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._freeDocumentRepository.GetById((int)mediaId);
            }
        }

        public PaidVideo GetPaidVideoById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._paidVideoRepository.GetById((int)mediaId);
            }
        }

        public FreeVideo GetFreeVideoById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._freeVideoRepository.GetById((int)mediaId);
            }
        }

        public void SaveOrUpdateFreeDocument(FreeDocument document)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (document.FreeDocumentId != null && document.FreeDocumentId > 0)
                {
                    //update
                    var result = _unitOfWork._freeDocumentRepository.GetById((int)document.FreeDocumentId);
                    if (result != null)
                    {
                        result.RoleName = document.RoleName;
                        result.FilePath = document.FilePath;
                        result.SubjectId = document.SubjectId;
                        _unitOfWork.SaveChanges();
                    }
                }
                else
                {
                    //create
                    _unitOfWork._freeDocumentRepository.Add(document);
                    _unitOfWork.SaveChanges();

                }
            }
        }

        public void SaveOrUpdatePaidDocument(PaidDocument paidDocument)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (paidDocument.PaidDocumentId != null && paidDocument.PaidDocumentId > 0)
                {
                    //update
                    var result = _unitOfWork._paidDocumentRepository.GetById((int)paidDocument.PaidDocumentId);
                    if (result != null)
                    {
                        result.RoleName = paidDocument.RoleName;
                        result.FilePath = paidDocument.FilePath;
                        result.SubjectId = paidDocument.SubjectId;
                        _unitOfWork.SaveChanges();
                    }
                }
                else
                {
                    //create
                    _unitOfWork._paidDocumentRepository.Add(paidDocument);
                    _unitOfWork.SaveChanges();

                }
            }
        }

        public void DeleteFreeDocumentById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var pDoc = _unitOfWork._freeDocumentRepository.GetAll().FirstOrDefault(p => p.FreeDocumentId == mediaId);
                _unitOfWork._freeDocumentRepository.Delete(pDoc);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeletePaidVideoById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                var pDoc = _unitOfWork._paidVideoRepository.GetAll().FirstOrDefault(p => p.PaidVideoId == mediaId);
                _unitOfWork.InitializeDbContext(dbContext); _unitOfWork._paidVideoRepository.Delete(pDoc);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeletePaidDocumentById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var pDoc = _unitOfWork._paidDocumentRepository.GetAll().FirstOrDefault(p => p.PaidDocumentId == mediaId);
                _unitOfWork._paidDocumentRepository.Delete(pDoc);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeleteFreeVideoById(int? mediaId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var pDoc = _unitOfWork._freeVideoRepository.GetAll().FirstOrDefault(p => p.FreeVideoId == mediaId);
                _unitOfWork._freeVideoRepository.Delete(pDoc);
                _unitOfWork.SaveChanges();
            }
        }

        public Teacher[] GetTeacherList()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._teacherRepository.GetAll();
            }
        }
        public Student[] GetStudentList()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._studentRepository.GetAll();
            }
        }

        public Subject[] GetSubjectList()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._subjectRepository.GetAll();
            }
        }

        public CalendarBooking GetTeacherCalendarByBookingId(int? bookingId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._calendarBookingRepository.GetById(((int)bookingId));
            }
        }

        public void DeleteTeacherCalendarByBooking(CalendarBooking teacherCalendar)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                if (teacherCalendar.CalendarBookingId != null && teacherCalendar.CalendarBookingId > 0)
                {
                    var booking = _unitOfWork._calendarBookingRepository.GetById((int)teacherCalendar.CalendarBookingId);
                    _unitOfWork._calendarBookingRepository.Delete(booking);
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public void DeleteBooking(int bookingTimeId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var bookingToDelete = _unitOfWork._bookingTimeRepository.GetById(bookingTimeId);
                _unitOfWork._bookingTimeRepository.Delete(bookingToDelete);
                _unitOfWork.SaveChanges();
            }
        }

        public void SaveOrUpdate(SHOP_PRODS shopProd)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var product = _unitOfWork._bookingTimeRepository.DbContextTeachersAssistant.ShopProducts.SingleOrDefault(p => p.prodId == shopProd.prodId);
                if (product == null)
                {
                    _unitOfWork._bookingTimeRepository.DbContextTeachersAssistant.ShopProducts.Add(shopProd);
                }
                else
                {
                    product.prodName = shopProd.prodName;
                    product.prodPrice = shopProd.prodPrice;
                    product.prodDesc = shopProd.prodDesc;
                    product.IsPaidVideo = shopProd.IsPaidVideo;
                    product.IsPaidDocument = shopProd.IsPaidDocument;
                    product.DocumentId = shopProd.DocumentId;
                }
                _unitOfWork.SaveChanges();
            }
        }

        public void DeleteProduct(SHOP_PRODS shopProd)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var product = _unitOfWork._bookingTimeRepository.DbContextTeachersAssistant.ShopProducts.SingleOrDefault(p => p.prodId == shopProd.prodId);
                if (product != null)
                {
                    _unitOfWork._bookingTimeRepository.DbContextTeachersAssistant.ShopProducts.Remove(product);
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public PaidDocument[] GetPaidDocuments()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._paidDocumentRepository.GetAll();
            }
        }

        public PaidVideo[] GetPaidVideos()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._paidVideoRepository.GetAll();
            }
        }

        public dynamic GetUserBoughtItems(string name)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var paidDocuments =
                    _unitOfWork._paidDocumentRepository.DbContextTeachersAssistant.ItemOrders.Where(
                        p => p.username.Equals(name) /*&& (bool)p.Order.paid_for*/);

                if (paidDocuments.Any())
                {
                    var paidDocs = from pd in paidDocuments.ToList()
                                   join pr in _unitOfWork._paidDocumentRepository.DbContextTeachersAssistant.ShopProducts.ToList()
                                   on pd.product_name equals pr.prodName into docs
                                   from p in docs.ToList()
                                   join q in _unitOfWork._paidDocumentRepository.GetAll().ToList() on p.DocumentId equals q.PaidDocumentId
                                   select new { ProdId = p.prodId, ProdName = p.prodName, IsDocument = true, p.prodDesc, FilePath = q.FilePath };

                    var paidVids = from pd in paidDocuments.ToList()
                                   join pr in _unitOfWork._paidVideoRepository.DbContextTeachersAssistant.ShopProducts.ToList()
                                    on pd.product_name equals pr.prodName into docs
                                   from p in docs.ToList()
                                   join q in _unitOfWork._paidVideoRepository.GetAll().ToList() on p.DocumentId equals q.PaidVideoId
                                   select new { ProdId = p.prodId, ProdName = p.prodName, IsDocument = false, p.prodDesc, FilePath = q.FilePath };

                    var result = paidDocs.ToList();
                    result.AddRange(paidVids.ToList());
                    return result;
                }
                return null;
            }
        }

        public StudentResource[] GetAllStudentResources()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);

                return _unitOfWork._studentResourcesRepository.GetAll().ToArray();
            }
        }

        public void SaveOrUpdateQAHelpRequests(QAHelpRequest qaHelpRequestViewModel)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var req = _unitOfWork._qAHelpRequestRepository.GetById(qaHelpRequestViewModel.QAHelpRequestId);
                if(req == null)
                {
                    _unitOfWork._qAHelpRequestRepository.Add(qaHelpRequestViewModel);
                }
                else
                {
                    _unitOfWork._qAHelpRequestRepository.Update(qaHelpRequestViewModel);
                }
                _unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<Assignment> GetCurrentAssignments(string roleName)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var results = _unitOfWork._assignmentRepository.GetAll().Where(p => p.StudentRole.ToLower().Equals(roleName.ToLower()) && p.DateDue > DateTime.Now);
                return results;
            }
        }
        public IEnumerable<Assignment> GetCurrentAssignments()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var results = _unitOfWork._assignmentRepository.GetAll().Where(p => p.DateDue >= DateTime.Now);
                return results;
            }
        }
        public IEnumerable<Assignment> GetAllAssignments()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var results = _unitOfWork._assignmentRepository.GetAll();
                return results;
            }
        }
        public IEnumerable<AssignmentSubmission> GetUngradedSubmittedAssignments()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var results = _unitOfWork._assignmentSubmissionRepository.GetAll().Where(p => p.IsSubmitted && string.IsNullOrEmpty(p.Grade));
                return results;
            }
        }
        public IEnumerable<AssignmentSubmission> GetCurrentAssignmentsSubmissions()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var results = _unitOfWork._assignmentSubmissionRepository.GetAll().Where(p => p.IsSubmitted );
                return results;
            }
        }
        public IEnumerable<AssignmentSubmission> GetGradedAssignmentsSubmissions()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var results = _unitOfWork._assignmentSubmissionRepository.GetAll().Where(p => p.IsSubmitted && !string.IsNullOrEmpty(p.Grade));
                return results;
            }
        }
        

        public AssignmentSubmission GetAssignmentSubmissionsById(int assignmentSubmissionId)
        {

            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                return _unitOfWork._assignmentSubmissionRepository.GetById(assignmentSubmissionId);
            }
        }

        public void DeleteAssignmentSubmissiongById(int assignmentSubmissionId)
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var submission = _unitOfWork._assignmentSubmissionRepository.GetById(assignmentSubmissionId);
                _unitOfWork._assignmentSubmissionRepository.Delete(submission);
                _unitOfWork.SaveChanges();
            }
        }

        public Course[] GetAllCourses()
        {
            using (var dbContext = new DataAccess.TeachersAssistantDbContext())
            {
                _unitOfWork.InitializeDbContext(dbContext);
                var courses = _unitOfWork._courseRepository.GetAll();
                return courses;
            }
        }
    }
}
