using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public class AssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public string StudentRole { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime DateDue { get; set; }
        public string FilePath { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string Select { get; set; }
        public string Update { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }
    }
}