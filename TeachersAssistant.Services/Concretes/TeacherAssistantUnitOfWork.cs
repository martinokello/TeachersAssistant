using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Services.Interfaces;
using TeachersAssistant.DataAccess.Concretes;

namespace  TeachersAssistant.Services.Concretes
{
    public class TeachersAssistantUnitOfWork : IUnitOfWork
    {
        public CalendarBookingRepository _calendarBookingRepository;
        public ClassroomRepository _classroomRepository;
        public FreeDocumentRepository _freeDocumentRepository;
        public FreeDocumentStudentRepository _freeDocumentStudentRepository;
        public FreeVideoRepository _freeVideoRepository;
        public FreeVideoStudentRepository _freeVideoStudentRepository;
        public PaidDocuemtStudentRepository _paidDocuemtStudentRepository;
        public PaidDocumentRepository _paidDocumentRepository;
        public PaidVideoRepository _paidVideoRepository;
        public PaidVideoStudentRepository _paidVideoStudentRepository;
        public StudentRepository _studentRepository;
        public StudentTypeRepository _studentTypeRepository;
        public SubjectRepository _subjectRepository;
        public TeacherRepository _teacherRepository;
        public BookingTimeRepository _bookingTimeRepository;
        public StudentResourcesRepository _studentResourcesRepository;
        public QAHelpRequestRepository _qAHelpRequestRepository;


        public DataAccess.TeachersAssistantDbContext TeachersAssistantDbContext { get; set; }
        public TeachersAssistantUnitOfWork() { }

        public TeachersAssistantUnitOfWork(ICalendarBookingRepositoryMarker calendarBookingRepository,
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
            IQAHelpRequestRepositoryMarker qAHelpRequestRepositoryMarker)
        {
            _calendarBookingRepository = calendarBookingRepository as CalendarBookingRepository;
            _classroomRepository = classroomRepositoryMarker as ClassroomRepository;
            _freeDocumentRepository = freeDocumentRepositoryMarker as FreeDocumentRepository;
            _freeDocumentStudentRepository = freeDocumentStudentRepositoryMarker as FreeDocumentStudentRepository;
            _freeVideoRepository = freeVideoRepositoryMarker as FreeVideoRepository;
            _freeVideoStudentRepository = freeVideoStudentRepositoryMarker as FreeVideoStudentRepository;
            _paidDocuemtStudentRepository = paidDocuemtStudentRepositoryMarker as PaidDocuemtStudentRepository;
            _paidDocumentRepository = paidDocumentRepositoryMarker as PaidDocumentRepository;
            _paidVideoRepository = paidVideoRepositoryMarker as PaidVideoRepository;
            _paidVideoStudentRepository = paidVideoStudentRepositoryMarker as PaidVideoStudentRepository;
            _studentRepository = studentRepositoryMarker as StudentRepository;
            _studentTypeRepository = studentTypeRepositoryMarker as StudentTypeRepository;
            _subjectRepository = subjectRepositoryMarker as SubjectRepository;
            _teacherRepository = teacherRepositoryMarker as TeacherRepository;
            _bookingTimeRepository = bookingTimeRepositoryMarker as BookingTimeRepository;
            _studentResourcesRepository = studentResourcesRepositoryMarker as StudentResourcesRepository;
            _qAHelpRequestRepository = qAHelpRequestRepositoryMarker as QAHelpRequestRepository;
        }

        public void InitializeDbContext(DataAccess.TeachersAssistantDbContext teachersAssistantDbContext)
        {
            _calendarBookingRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _classroomRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _freeDocumentRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _freeDocumentStudentRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _freeVideoRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _freeVideoStudentRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _paidDocuemtStudentRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _paidDocumentRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _paidVideoRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _paidVideoStudentRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _studentRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _studentTypeRepository.DbContextTeachersAssistant = TeachersAssistantDbContext;
            _subjectRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _teacherRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _bookingTimeRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _bookingTimeRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _studentResourcesRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            _qAHelpRequestRepository.DbContextTeachersAssistant = teachersAssistantDbContext;
            this.TeachersAssistantDbContext = teachersAssistantDbContext;
        }
        public void SaveChanges()
        {
            TeachersAssistantDbContext.SaveChanges();
        }


    }
}
