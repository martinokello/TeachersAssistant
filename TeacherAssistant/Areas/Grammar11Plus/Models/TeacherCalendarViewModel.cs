using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Areas.Grammar11Plus.TeacherAssistant.Models
{

    public class TeacherCalendarViewModel
    {
        public int? CalendarBookingId { get; set; }
        public int SubjectId { get; set; }
        public string TeacherId { get; set; }
        public string StudentTypeId { get; set; }
        public int StudentId { get; set; }
        public BookingTime[] BookingTimes { get; set; }
        [Required]
        public string TeacherFullName { get; set; }
        [Required]
        public string StudentFullName { get; set; }
        [Required]
        public string Description { get; set; }
        public int ClassId { get; set; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}