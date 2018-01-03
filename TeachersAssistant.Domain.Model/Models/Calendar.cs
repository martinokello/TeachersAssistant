using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Domain.Model.Models
{
    public enum StudentTypes { Grammar11Plus, StatePrimary, StateJunior}
    public class CalendarBooking
    {
        [Key]
        public int? CalendarBookingId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public string StudentTypeId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ForeignKey("BookingTime")]
        public int BookingTimeId { get; set; }
        public BookingTime BookingTime { get; set; }
        public string TeacherFullName { get; set; }
        public string StudentFullName { get; set; }
        [Required]
        public string Description { get; set; }
        public int ClassId { get; set; }
    }
}
