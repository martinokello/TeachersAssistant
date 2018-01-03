using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Areas.StatePrimary.TeachersAssistant.Models
{
    public class UploadMediaViewModel
    {
        public int MediaId { get; set; }
        public string MediaType { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string RoleName { get; set; }
        public int SubjectId { get; set; }
        
    }
}