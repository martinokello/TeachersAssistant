using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{
    public class CalendarBookingViewModel
    {
        public Teacher Teacher { get; set; }
        public string TeacherFirsName { get; set; }
        public string TeacherLastName { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }

        public string FirsName { get; set; }
        public string LastName { get; set; }
        public BookingTime BookingTime { get; set; }

        public int? ClassroomId { get; set; }
    }
}