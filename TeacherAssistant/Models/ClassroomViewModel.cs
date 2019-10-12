using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{
    public interface IClassroomViewModel
    {
        int? ClassroomId { get; set; }
        int TeacherId { get; set; }
        Teacher Teacher { get; set; }
        int CalendarBookingId { get; set; } 
        string StudentType { get; set; }
        int SubjectId { get; set; }
        Subject Subject { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }

    public class ClassroomSelectOrDeleteViewModel: IClassroomViewModel
    {
        [Required(ErrorMessage = "Classroom Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Classroom Id Required!")]
        public int? ClassroomId { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int CalendarBookingId { get; set; } = -1;
        public string StudentType { get; set; } = "StatePrimary";
        public int SubjectId { get; set; } = -1;
        public Subject Subject { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class ClassroomCreateViewModel : IClassroomViewModel
    {
        public int? ClassroomId { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required(ErrorMessage = "Calendar Booking Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Calendar Booking Id Required!")]
        public int CalendarBookingId { get; set; } = -1;
        [Required(ErrorMessage = "Student Type Required!")]
        public string StudentType { get; set; } = "StatePrimary";
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; } = -1;
        public Subject Subject { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class ClassroomUpdateViewModel : IClassroomViewModel
    {
        [Required(ErrorMessage = "Classroom Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Classroom Id Required!")]
        public int? ClassroomId { get; set; }
        [Required(ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required(ErrorMessage = "Calendar Booking Id Required!")]
        public int CalendarBookingId { get; set; } = -1;
        [Required(ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; } = -1;
        [Required(ErrorMessage = "Student Type Required!")]
        public string StudentType { get; set; } = "StatePrimary";
        [Required(ErrorMessage = "Subject Id Required!")]
        public Subject Subject { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}