using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                var mtu = cfg.CreateMap<TeacherUpdateViewModel, Teacher>();
                mtu.ReverseMap();
                var mtsd = cfg.CreateMap < TeacherSelectOrDeleteViewModel, Teacher>();
                mtsd.ReverseMap();
                var mtcr = cfg.CreateMap<TeacherCreateViewModel, Teacher>();
                mtcr.ReverseMap();
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
                var scb = cfg.CreateMap<SubjectCreateViewModel, Subject>();
                scb.ReverseMap();
                var sub = cfg.CreateMap<SubjectUpdateViewModel, Subject>();
                sub.ReverseMap();
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
                //map.ForAllMembers(opt => opt.Ignore());
                mp.ForMember(dest => dest.prodName, opt => opt.MapFrom(src => src.ProductName));
                mp.ForMember(dest => dest.prodId, opt => opt.MapFrom(src => src.ProductId));
                mp.ForMember(dest => dest.prodPrice, opt => opt.MapFrom(src => src.ProductPrice));
                mp.ForMember(dest => dest.prodDesc, opt => opt.MapFrom(src => src.ProductDescription));
                mp.ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId));
                mp.ForMember(dest => dest.IsPaidDocument, opt => opt.MapFrom(src => src.IsPaidDocument));
                mp.ForMember(dest => dest.IsPaidVideo, opt => opt.MapFrom(src => src.IsPaidVideo));
                mp.ReverseMap();
            });
        }
    }
}