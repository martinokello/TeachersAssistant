using System;
using System.ComponentModel.DataAnnotations;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{
    public interface ISubjectViewModel
    {
        int? SubjectId { get; set; }
        int CourseId { get; set; }
        string SubjectName { get; set; }
        int TeacherId { get; set; }
        Teacher Teacher { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }
    public class SubjectSelectOrDeleteViewModel:ISubjectViewModel
    {
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int? SubjectId { get; set; }
        public int CourseId { get; set; }
        public string SubjectName { get; set; } 
        public int TeacherId { get; set; } = -1;
        public Teacher Teacher { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class SubjectCreateViewModel : ISubjectViewModel
    {
        public int? SubjectId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Subject Name Required!")]
        public string SubjectName { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; } = -1;
        public Teacher Teacher { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class SubjectUpdateViewModel : ISubjectViewModel
    {
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int? SubjectId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Subject Name Required!")]
        public string SubjectName { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; } = -1;
        public Teacher Teacher { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}