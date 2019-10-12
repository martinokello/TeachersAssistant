using System;
using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Models
{
    public interface ITeacherViewModel
    {
        int? TeacherId { get; set; }
        string FirsName { get; set; }
        string LastName { get; set; } 
        string EmailAddress { get; set; } 
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }
    public class TeacherSelectOrDeleteViewModel: ITeacherViewModel
    {
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Calendar Boopking Id Required!")]
        public int? TeacherId { get; set; }
        public string FirsName { get; set; } = "Teacher FirstName";
        public string LastName { get; set; } = "Teacher LastName";
        public string EmailAddress { get; set; } = "Teacher@martinlayooinc.co.uk";
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class TeacherCreateViewModel : ITeacherViewModel
    {
        public int? TeacherId { get; set; }
        [Required(ErrorMessage = "FirstName Required!")]
        public string FirsName { get; set; } = "Teacher FirstName";
        [Required(ErrorMessage = "Last Name Required!")]
        public string LastName { get; set; } = "Teacher LastName";
        [Required(ErrorMessage = "Email Address Required!")]
        public string EmailAddress { get; set; } = "Teacher@martinlayooinc.co.uk";
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class TeacherUpdateViewModel : ITeacherViewModel
    {
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Calendar Boopking Id Required!")]
        public int? TeacherId { get; set; }
        [Required(ErrorMessage = "FirstName Required!")]
        public string FirsName { get; set; } = "Teacher FirstName";
        [Required(ErrorMessage = "Last Name Required!")]
        public string LastName { get; set; } = "Teacher LastName";
        [Required(ErrorMessage = "Email Address Required!")]
        public string EmailAddress { get; set; } = "Teacher@martinlayooinc.co.uk";
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}