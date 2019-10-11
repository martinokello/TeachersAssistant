using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeacherAssistant.Infrastructure.Validation;

namespace TeacherAssistant.Models
{
    public interface IAssignmentViewModel
    {
        int AssignmentId { get; set; }
        string StudentRole { get; set; }
        int StudentId { get; set; }
        int TeacherId { get; set; }
        int CourseId { get; set; }
        int SubjectId { get; set; }
        string AssignmentName { get; set; }
        string Description { get; set; }
        DateTime DateAssigned { get; set; }
        DateTime DateDue { get; set; }
        string FilePath { get; set; }
        HttpPostedFileBase MediaContent { get; set; }
        string Select { get; set; }
        string Update { get; set; }
        string Create { get; set; }
        string Delete { get; set; }
    }

    public class AssignmentSelectAndDeleteViewModel :  IAssignmentViewModel
    {
        [Required(ErrorMessage = "Assignment Id Required!")]
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

    public class AssignmentUpdateViewModel:IAssignmentViewModel
    {
        [Required(ErrorMessage = "Assignment Id Required!")]
        public int AssignmentId { get; set; }
        [StudentIdOrRoleValidation(ErrorMessage = "Either Student Role or Student Id Required!")]
        public string StudentRole { get; set; }

        [StudentIdOrRoleValidation(ErrorMessage = "Either Student Role or Student Id Required!")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Assignment Name Required!")]
        public string AssignmentName { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Assigned Date Required!")]
        [DateTimeFormat(ErrorMessage = "Date Format is wrong and Time should be in 24 Hr clock")]
        public DateTime DateAssigned { get; set; }
        [Required(ErrorMessage = "Due Back Date Required!")]
        [DateTimeFormat(ErrorMessage = "Date Format is wrong and Time should be in 24 Hr clock")]
        public DateTime DateDue { get; set; }
        public string FilePath { get; set; }
        [Required(ErrorMessage = "Assignment File Required!")]
        public HttpPostedFileBase MediaContent { get; set; }
        public string Select { get; set; }
        public string Update { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }
    }

    public class AssignmentCreateViewModel:IAssignmentViewModel
    {
        public int AssignmentId { get; set; }
        [StudentIdOrRoleValidation(ErrorMessage = "Either Student Role or Student Id Required!")]
        public string StudentRole { get; set; }

        [StudentIdOrRoleValidation(ErrorMessage = "Either Student Role or Student Id Required!")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Assignment Name Required!")]
        public string AssignmentName { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Assigned Date Required!")]
        [DateTimeFormat(ErrorMessage = "Date Format is wrong and Time should be in 24 Hr clock")]
        public DateTime DateAssigned { get; set; }
        [Required(ErrorMessage = "Due Back Date Required!")]
        [DateTimeFormat(ErrorMessage = "Date Format is wrong and Time should be in 24 Hr clock")]
        public DateTime DateDue { get; set; }
        public string FilePath { get; set; }
        [Required(ErrorMessage = "Assignment File Required!")]
        public HttpPostedFileBase MediaContent { get; set; }
        public string Select { get; set; }
        public string Update { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }
    }
}