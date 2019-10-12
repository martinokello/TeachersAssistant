using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeacherAssistant.Infrastructure.FluentValidation;

namespace TeacherAssistant.Models
{
    public interface IUploadMediaViewModel
    {
        int? MediaId { get; set; }
        string MediaType { get; set; }
        string Name { get; set; }
        HttpPostedFileBase MediaContent { get; set; }
        string RoleName { get; set; }
        int SubjectId { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }

    }
    public class UploadMediaSelectOrDeleteViewModel:IUploadMediaViewModel
    {
        [Required(ErrorMessage = "Media Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Media Id Required!")]
        public int? MediaId { get; set; }
        public string MediaType { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string RoleName { get; set; }
        public int SubjectId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }

    }
    [Validator(typeof(UploadMediaViewModelValidator))]
    public class UploadMediaCreateViewModel : IUploadMediaViewModel
    {
        public int? MediaId { get; set; }
        public string MediaType { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string RoleName { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }

    }
    [Validator(typeof(UploadMediaViewModelValidator))]
    public class UploadMediaUpdateViewModel : IUploadMediaViewModel
    {
        [Required(ErrorMessage = "Media Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Media Id Required!")]
        public int? MediaId { get; set; }
        public string MediaType { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string RoleName { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }

    }
}