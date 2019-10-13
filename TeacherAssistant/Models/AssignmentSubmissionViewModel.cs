using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeacherAssistant.Infrastructure.Validation;

namespace TeacherAssistant.Models
{
    public interface IAssignmentSubmissionViewModel
    {
        int AssignmentSubmissionId { get; set; }
        int CourseId { get; set; }
        string AssignmentName { get; set; }
        int AssignmentId { get; set; }
        int StudentId { get; set; }
        int TeacherId { get; set; }
        int SubjectId { get; set; }
        DateTime DateDue { get; set; }
        DateTime DateSubmitted { get; set; }
        string FilePath { get; set; }
        HttpPostedFileBase MediaContent { get; set; }
        string StudentRole { get; set; }
        bool IsSubmitted { get; set; }
        string Graded { get; set; }
        string BothGradedAndUnGraded { get; set; }
        string Notes { get; set; }
        string Grade { get; set; }
        decimal GradeNumeric { get; set; }
        string Select { get; set; }
        string Update { get; set; }
        string Create { get; set; }
        string Delete { get; set; }
    }

    public class AssignmentSubmissionSelectOrDeleteViewModel:IAssignmentSubmissionViewModel
    {
        [Required(ErrorMessage= "AssignmentSubmission Id Required!")]
        [Range(1,Int32.MaxValue,ErrorMessage = "AssignmentSubmission Id Required!")]
        public int AssignmentSubmissionId { get; set; }
        public int CourseId { get; set; }
        public string AssignmentName { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
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

    public class AssignmentSubmissionCreateViewModel : IAssignmentSubmissionViewModel
    {
        public int AssignmentSubmissionId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Course Id  Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Assignment Name Required!")]
        public string AssignmentName { get; set; }
        [Required(ErrorMessage = "Assignment Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Assignment Id  Required!")]
        public int AssignmentId { get; set; }
        [Required(ErrorMessage = "Student Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Id  Required!")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Teacher Id  Required!")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id  Required!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Due Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "DateDue format wrong and Time should be in 24 Hr Clock")]
        public DateTime DateDue { get; set; }
        [Required(ErrorMessage = "Submission Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "DateSubmitted format wrong and Time should be in 24 Hr Clock")]
        public DateTime DateSubmitted { get; set; }
        public string FilePath { get; set; }
        [Required(ErrorMessage = "Media Content Attachment Required!")]
        public HttpPostedFileBase MediaContent { get; set; }
        [Required(ErrorMessage = "Student Role Required!")]
        public string StudentRole { get; set; }
        public bool IsSubmitted { get; set; }
        public string Graded { get; set; }
        public string UnGraded { get; set; }
        public string BothGradedAndUnGraded { get; set; }
        [Required(ErrorMessage = "Notes Required!")]
        public string Notes { get; set; }
        [Required(ErrorMessage = "Grade Required!")]
        public string Grade { get; set; }
        [Required(ErrorMessage = "Numeric Grade Required!")]
        public decimal GradeNumeric { get; set; }
        public string Select { get; set; }
        public string Update { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }
    }

    public class AssignmentSubmissionUpdateViewModel : IAssignmentSubmissionViewModel
    {
        [Required(ErrorMessage = "AssignmentSubmission Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "AssignmentSubmission Id Required!")]
        public int AssignmentSubmissionId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Course Id  Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Assignment Name Required!")]
        public string AssignmentName { get; set; }
        [Required(ErrorMessage = "Assignment Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Assignment Id  Required!")]
        public int AssignmentId { get; set; }
        [Required(ErrorMessage = "Student Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Id  Required!")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Teacher Id  Required!")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id  Required!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Due Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "DateDue format wrong and Time should be in 24 Hr Clock")]
        public DateTime DateDue { get; set; }
        [Required(ErrorMessage = "Submission Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "DateSubmitted format wrong and Time should be in 24 Hr Clock")]
        public DateTime DateSubmitted { get; set; }
        public string FilePath { get; set; }
        [Required(ErrorMessage = "Media Content Attachment Required!")]
        public HttpPostedFileBase MediaContent { get; set; }
        [Required(ErrorMessage = "Student Role Required!")]
        public string StudentRole { get; set; }
        public bool IsSubmitted { get; set; }
        public string Graded { get; set; }
        public string UnGraded { get; set; }
        public string BothGradedAndUnGraded { get; set; }
        [Required(ErrorMessage = "Notes Required!")]
        public string Notes { get; set; }
        [Required(ErrorMessage = "Grade Required!")]
        public string Grade { get; set; }
        [Required(ErrorMessage = "Numeric Grade Required!")]
        public decimal GradeNumeric { get; set; }
        public string Select { get; set; }
        public string Update { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }
    }
}