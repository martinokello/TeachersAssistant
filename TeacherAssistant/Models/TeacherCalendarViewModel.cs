using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;
using TeacherAssistant.Infrastructure;

namespace TeacherAssistant.Models
{
    public class BookingTimeString
    {
        [DateTimeFormat()]
        public string StartTime { get; set; }

        [DateTimeFormat()]
        public string EndTime { get; set; }

        public int? BookingTimeId { get; set; }
    }
    public class TeacherCalendarViewModel
    {
        public int? CalendarBookingId { get; set; }
        [Required]
        public int SubjectId { get; set; } = -1;
        [Required]
        public int TeacherId { get; set; } = -1;
        public int StudentTypeId { get; set; }
        [Required]
        public int StudentId { get; set; } = -1;
        public BookingTimeString[] BookingTimes { get; set; }
        public string TeacherFullName { get; set; }
        public string StudentFullName { get; set; }
        [Required]
        public string Description { get; set; } = "Teacher Calendar Description Default";
        public int ClassId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}