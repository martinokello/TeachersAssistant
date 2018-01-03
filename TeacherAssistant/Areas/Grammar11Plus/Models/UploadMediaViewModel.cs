using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Areas.Grammar11Plus.TeachersAssistant.Models
{
    public class UploadMediaViewModel
    {
        public int MediaId { get; set; }
        public string MediaType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public HttpPostedFileBase MediaContent { get; set; }
        [Required]
        public string RoleName { get; set; }
        public int SubjectId { get; set; }
        
    }
}