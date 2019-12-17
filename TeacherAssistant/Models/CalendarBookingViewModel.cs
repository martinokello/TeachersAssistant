using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{
    public interface ICalendarBookingViewModel
    {
        int CalendarBookingId { get; set; }
        BookingTime BookingTime { get; set; }
        int? ClassId { get; set; }
        int TeacherId { get; set; }
        int StudentId { get; set; }
        int SubjectId { get; set; }
        string StudentRole { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Description { get; set; }
        bool IsScheduled { get; set; }
        Teacher Teacher { get; set; }
        Subject Subject { get; set; }
        Student Student { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }
    public class CalendarBookingCreateViewModel : ICalendarBookingViewModel
    {
        public int CalendarBookingId { get; set; }
        [Required(ErrorMessage = "Booking Time Required!")]
        public BookingTime BookingTime { get; set; }
        [Required(ErrorMessage = "TeacherId Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Teacher/Tutor!")]
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student Id Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Subject!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        public string StudentRole { get; set; }
        [Required(ErrorMessage = "Start Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "Start Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "End Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "End Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public int? ClassId { get; set; }
        public bool IsScheduled { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class CalendarBookingSelectOrDeleteViewModel : ICalendarBookingViewModel
    {
        [Required(ErrorMessage = "Calendar Booking Id Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Calendar Booking!")]
        public int CalendarBookingId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string StudentRole { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool IsScheduled { get; set; }
        public int? ClassId { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
        public BookingTime BookingTime { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class CalendarBookingUpdateViewModel : ICalendarBookingViewModel
    {
        [Required(ErrorMessage = "Calendar Booking Id Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Calendar Booking!")]
        public int CalendarBookingId { get; set; }
        [Required(ErrorMessage = "TeacherId Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Teacher/Tutor!")]
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student Id Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Subject!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        public string StudentRole { get; set; }
        [Required(ErrorMessage = "Start Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "Start Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "End Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "End Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Booking Time Required!")]
        public BookingTime BookingTime { get; set; }
        public int? ClassId { get; set; }
        public bool IsScheduled { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}