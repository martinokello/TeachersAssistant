using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public interface ICourseViewModel
    {
        int CourseId { get; set; }
        string CourseName { get; set; }
        string CourseDescription { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }

    public class CourseSelectOrDeleteViewModel: ICourseViewModel
    {
        [Required(ErrorMessage="Course Id Required!")]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class CourseCreateViewModel : ICourseViewModel
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course Name Required!")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Course Description Required!")]
        public string CourseDescription { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class CourseUpdateViewModel : ICourseViewModel
    {
        [Required(ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course Name Required!")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Course Description Required!")]
        public string CourseDescription { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}