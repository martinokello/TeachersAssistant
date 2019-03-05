using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeacherAssistant.Infrastructure.FluentValidation;

namespace TeacherAssistant.Models
{
    [Validator(typeof(UploadMediaViewModelValidator))]
    public class UploadMediaViewModel
    {
        public int? MediaId { get; set; }
        //[Required]
        public string MediaType { get; set; }
        //[Required]
        public string Name { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
       // [Required]
        public string RoleName { get; set; }
        //[Required]
        public int SubjectId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
        
    }
}