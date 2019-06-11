using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public class AssignmentSubmissionViewModel
    {
        public int AssignmentSubmissionId { get; set; }
        public int CourseId { get; set; }
        public string AssignmentName { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public  int SubjectId { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string FilePath { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string StudentRole { get; set; }
        public bool IsSubmitted { get; set; }
        public string Graded { get; set; }
        public string UnGraded { get; set; }
        public string BothGradedAndUnGraded { get; set; }
        public string Notes { get; set; }
        public string Grade { get; set; }
        public decimal GradeNumeric { get; set; }
        public string Select { get; set; }
        public string Update { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }
    }
}