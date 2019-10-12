using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public interface IStudentResourcesViewModel
    {
        int StudentResourceId { get; set; }
        string StudentResourceName { get; set; }
        HttpPostedFileBase MediaContent { get; set; }
        string FilePath { get; set; }
        int SubjectId { get; set; }
        int CourseId { get; set; }
        string RoleName { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }
    public class StudentResourcesSelectOrDeleteViewModel: IStudentResourcesViewModel
    {
        [Required(ErrorMessage = "Student Resource Id Required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Resource Id Required!")]
        public int StudentResourceId { get; set; }
        public string StudentResourceName { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string FilePath { get; set; }
        public int SubjectId { get; set; }
        public int CourseId { get; set; }
        public string RoleName { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class StudentResourcesCreateViewModel : IStudentResourcesViewModel
    {
        public int StudentResourceId { get; set; }
        [Required(ErrorMessage = "Resource Name Required")]
        public string StudentResourceName { get; set; }
        [Required(ErrorMessage = "Media Content Required")]
        public HttpPostedFileBase MediaContent { get; set; }
        public string FilePath { get; set; }
        [Required(ErrorMessage = "Subject Id Required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Course Id Required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Role Name Required")]
        public string RoleName { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class StudentResourcesUpdateViewModel
    {
        [Required(ErrorMessage = "Student Resource Id Required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Resource Id Required!")]
        public int StudentResourceId { get; set; }
        [Required(ErrorMessage = "Resource Name Required")]
        public string StudentResourceName { get; set; }
        [Required(ErrorMessage = "Media Content Required")]
        public HttpPostedFileBase MediaContent { get; set; }
        public string FilePath { get; set; }
        [Required(ErrorMessage = "Subject Id Required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Course Id Required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Role Name Required")]
        public string RoleName { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}