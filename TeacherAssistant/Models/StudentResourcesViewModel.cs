using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public class StudentResourcesViewModel
    {
        public int StudentResourceId { get; set; }
        [Required(ErrorMessage = "Resource Name Required")]
        public string StudentResourceName { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string FilePath { get; set; }
        public int SubjectId { get; set; }
        public string RoleName { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}