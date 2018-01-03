using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{

    public class TeacherCalendarViewModel
    {
        public int? CalendarBookingId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public int StudentTypeId { get; set; }
        [Required]
        public int StudentId { get; set; }
        public BookingTime[] BookingTimes { get; set; }
        public string TeacherFullName { get; set; }
        public string StudentFullName { get; set; }
        [Required]
        public string Description { get; set; }
        public int ClassId { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}