using TeacherAssistant.Models;
using TeachersAssistant.Domain.Model.Models;

namespace TeachersAssistant.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                var assd = cfg.CreateMap<AssignmentSelectAndDeleteViewModel, Assignment>();
                assd.ReverseMap();
                var assu = cfg.CreateMap<AssignmentUpdateViewModel, Assignment>();
                assu.ReverseMap();
                var assc = cfg.CreateMap<AssignmentCreateViewModel, Assignment>();
                assc.ReverseMap();
                var asssub = cfg.CreateMap<AssignmentSubmissionSelectOrDeleteViewModel, Assignment>();
                asssub.ReverseMap();
                var asssubc = cfg.CreateMap<AssignmentSubmissionCreateViewModel, Assignment>();
                asssubc.ReverseMap();
                var asssu = cfg.CreateMap<AssignmentSubmissionUpdateViewModel, Assignment>();
                asssu.ReverseMap();
                var mtu = cfg.CreateMap<TeacherUpdateViewModel, Teacher>();
                mtu.ReverseMap();
                var mtsd = cfg.CreateMap < TeacherSelectOrDeleteViewModel, Teacher>();
                mtsd.ReverseMap();
                var mtcr = cfg.CreateMap<TeacherCreateViewModel, Teacher>();
                mtcr.ReverseMap();


                var mtcu = cfg.CreateMap<TeacherCalendarUpdateViewModel, CalendarBooking>();
                mtcu.ReverseMap();
                var mtcsd = cfg.CreateMap<TeacherCalendarSelectOrDeleteViewModel, CalendarBooking>();
                mtcsd.ReverseMap();
                var mtccr = cfg.CreateMap<TeacherCalendarCreateViewModel, CalendarBooking>();
                mtccr.ReverseMap(); 

                var mtc = cfg.CreateMap<ClassroomCreateViewModel, Classroom>();
                mtc.ForMember(dest => dest.CalendarId, opt=> opt.MapFrom( src => src.CalendarBookingId));
                mtc.ReverseMap();
                var mfc = cfg.CreateMap<ClassroomUpdateViewModel, Classroom>();
                mfc.ForMember(dest => dest.CalendarId, opt => opt.MapFrom(src => src.CalendarBookingId));
                mfc.ReverseMap();
                var msdc = cfg.CreateMap<ClassroomSelectOrDeleteViewModel, Classroom>();
                msdc.ForMember(dest => dest.CalendarId, opt => opt.MapFrom(src => src.CalendarBookingId));
                msdc.ReverseMap();
                var st = cfg.CreateMap<StudentCreateViewModel, Student>();
                st.ReverseMap();
                var sut = cfg.CreateMap<StudentUpdateViewModel, Student>();
                sut.ReverseMap();
                var ssodt = cfg.CreateMap<StudentSelectOrDeleteViewModel, Student>();
                ssodt.ReverseMap();

                var strc = cfg.CreateMap<StudentResourcesCreateViewModel, StudentResource>();
                strc.ReverseMap();
                var sutu = cfg.CreateMap<StudentResourcesUpdateViewModel, StudentResource>();
                sutu.ReverseMap();
                var srsdt = cfg.CreateMap<StudentResourcesSelectOrDeleteViewModel, StudentResource>();
                srsdt.ReverseMap();
                var scb = cfg.CreateMap<SubjectCreateViewModel, Subject>();
                scb.ReverseMap();
                var sub = cfg.CreateMap<SubjectUpdateViewModel, Subject>();
                sub.ReverseMap();
                var subsd= cfg.CreateMap<SubjectSelectOrDeleteViewModel, Subject>();
                subsd.ReverseMap();
                var docsFdoc = cfg.CreateMap<PaidDocument, FreeDocument>();
                docsFdoc.ReverseMap();
                var docsFvid = cfg.CreateMap<PaidDocument, FreeVideo>();
                docsFvid.ReverseMap();
                var docsPvid = cfg.CreateMap<PaidDocument, PaidVideo>();
                docsPvid.ReverseMap();
                var courseMap = cfg.CreateMap<CourseSelectOrDeleteViewModel, Course>();
                courseMap.ReverseMap();
                var courseMapc = cfg.CreateMap<CourseCreateViewModel, Course>();
                courseMapc.ReverseMap();
                var courseMapu = cfg.CreateMap<CourseUpdateViewModel, Course>();
                courseMapu.ReverseMap();
                var mp = cfg.CreateMap<ProductSelectOrDeleteViewModel, SHOP_PRODS>();
                var mpc = cfg.CreateMap<ProductCreateViewModel, SHOP_PRODS>();
                var mpu = cfg.CreateMap<ProductUpdateViewModel, SHOP_PRODS>();
                //map.ForAllMembers(opt => opt.Ignore());
                mp.ForMember(dest => dest.prodName, opt => opt.MapFrom(src => src.ProductName));
                mp.ForMember(dest => dest.prodId, opt => opt.MapFrom(src => src.ProductId));
                mp.ForMember(dest => dest.prodPrice, opt => opt.MapFrom(src => src.ProductPrice));
                mp.ForMember(dest => dest.prodDesc, opt => opt.MapFrom(src => src.ProductDescription));
                mp.ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId));
                mp.ForMember(dest => dest.IsPaidDocument, opt => opt.MapFrom(src => src.IsPaidDocument));
                mp.ForMember(dest => dest.IsPaidVideo, opt => opt.MapFrom(src => src.IsPaidVideo));
                mp.ReverseMap();

                mpc.ForMember(dest => dest.prodName, opt => opt.MapFrom(src => src.ProductName));
                mpc.ForMember(dest => dest.prodId, opt => opt.MapFrom(src => src.ProductId));
                mpc.ForMember(dest => dest.prodPrice, opt => opt.MapFrom(src => src.ProductPrice));
                mpc.ForMember(dest => dest.prodDesc, opt => opt.MapFrom(src => src.ProductDescription));
                mpc.ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId));
                mpc.ForMember(dest => dest.IsPaidDocument, opt => opt.MapFrom(src => src.IsPaidDocument));
                mpc.ForMember(dest => dest.IsPaidVideo, opt => opt.MapFrom(src => src.IsPaidVideo));
                mpc.ReverseMap();

                mpu.ForMember(dest => dest.prodName, opt => opt.MapFrom(src => src.ProductName));
                mpu.ForMember(dest => dest.prodId, opt => opt.MapFrom(src => src.ProductId));
                mpu.ForMember(dest => dest.prodPrice, opt => opt.MapFrom(src => src.ProductPrice));
                mpu.ForMember(dest => dest.prodDesc, opt => opt.MapFrom(src => src.ProductDescription));
                mpu.ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId));
                mpu.ForMember(dest => dest.IsPaidDocument, opt => opt.MapFrom(src => src.IsPaidDocument));
                mpu.ForMember(dest => dest.IsPaidVideo, opt => opt.MapFrom(src => src.IsPaidVideo));
                mpu.ReverseMap();
            });
        }
    }
}