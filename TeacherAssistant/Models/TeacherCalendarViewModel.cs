using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;
using TeacherAssistant.Infrastructure.Validation;

namespace TeacherAssistant.Models
{
    public class BookingTimeString
    {
        
        public string StartTime { get; set; }
        
        public string EndTime { get; set; }

        public int? BookingTimeId { get; set; }
    }
    public interface ITeacherCalendarViewModel
    {
        int? CalendarBookingId { get; set; }
        int SubjectId { get; set; } 
        int TeacherId { get; set; }
        int StudentTypeId { get; set; }
        int StudentId { get; set; } 
        BookingTimeString[] BookingTimes { get; set; }
        string TeacherFullName { get; set; }
        string StudentFullName { get; set; }
        string Description { get; set; } 
        int ClassId { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }
    public class TeacherCalendarSelectOrDeleteViewModel:ITeacherCalendarViewModel
    {
        [Required(ErrorMessage ="Calendar Boopking Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Calendar Boopking Id Required!")]
        public int? CalendarBookingId { get; set; }
        public int SubjectId { get; set; } = -1;
        public int TeacherId { get; set; } = -1;
        public int StudentTypeId { get; set; }
        public int StudentId { get; set; } = -1;
        public BookingTimeString[] BookingTimes { get; set; }
        public string TeacherFullName { get; set; }
        public string StudentFullName { get; set; }
        public string Description { get; set; } = "Teacher Calendar Description Default";
        public int ClassId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class TeacherCalendarCreateViewModel : ITeacherCalendarViewModel
    {
        public int? CalendarBookingId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; } = -1;
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; } = -1;
        [Required(ErrorMessage = "Student Type Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Type Id Required!")]
        public int StudentTypeId { get; set; }
        [Required(ErrorMessage = "StudentId Id Required!")]
        public int StudentId { get; set; } = -1;
        public BookingTimeString[] BookingTimes { get; set; }
        [Required(ErrorMessage = "Teacher Name Required!")]
        public string TeacherFullName { get; set; }
        [Required(ErrorMessage = "Student Name Required!")]
        public string StudentFullName { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; } = "Teacher Calendar Description Default";
        [Required(ErrorMessage = "Class Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Class Id Required!")]
        public int ClassId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class TeacherCalendarUpdateViewModel : ITeacherCalendarViewModel
    {
        [Required(ErrorMessage = "Calendar Booking Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Calendar Boopking Id Required!")]
        public int? CalendarBookingId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; } = -1;
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; } = -1;
        [Required(ErrorMessage = "Student Type Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Type Id Required!")]
        public int StudentTypeId { get; set; }
        [Required(ErrorMessage = "StudentId Id Required!")]
        public int StudentId { get; set; } = -1;
        public BookingTimeString[] BookingTimes { get; set; }
        [Required(ErrorMessage = "Teacher Name Required!")]
        public string TeacherFullName { get; set; }
        [Required(ErrorMessage = "Student Name Required!")]
        public string StudentFullName { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; } = "Teacher Calendar Description Default";
        [Required(ErrorMessage = "Class Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Class Id Required!")]
        public int ClassId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}