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

                var map = cfg.CreateMap<ProductViewModel, SHOP_PRODS>();
                map.ForAllMembers(opt => opt.Ignore());
                map.ForMember(dest => dest.prodName, opt => opt.MapFrom(src => src.ProductName));
                map.ForMember(dest => dest.prodId, opt => opt.MapFrom(src => src.ProductId));
                map.ForMember(dest => dest.prodPrice, opt => opt.MapFrom(src => src.ProductPrice));
                map.ForMember(dest => dest.prodDesc, opt => opt.MapFrom(src => src.ProductDescription));
                map.ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId));
                map.ReverseMap();
            });
        }
    }
}