using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeacherAssistant.Models;
using TeachersAssistant.Domain.Model.Models;
using TeacherAssistant.Models;

namespace TeachersAssistant.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                var mt = cfg.CreateMap<TeacherViewModel, Teacher>();
                mt.ReverseMap();
                var mc = cfg.CreateMap<ClassroomViewModel, Classroom>();
                mc.ForMember(dest => dest.CalendarId, opt=> opt.MapFrom( src => src.CalendarBookingId));
                mc.ReverseMap();
                var st = cfg.CreateMap<StudentViewModel, Student>();
                st.ReverseMap();
                var sub = cfg.CreateMap<SubjectViewModel, Subject>();
                sub.ReverseMap();
                var docsFdoc = cfg.CreateMap<PaidDocument, FreeDocument>();
                docsFdoc.ReverseMap();
                var docsFvid = cfg.CreateMap<PaidDocument, FreeVideo>();
                docsFvid.ReverseMap();
                var docsPvid = cfg.CreateMap<PaidDocument, PaidVideo>();
                docsPvid.ReverseMap();
                var mp = cfg.CreateMap<ProductViewModel, SHOP_PRODS>();
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