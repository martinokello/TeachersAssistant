using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Areas.StateJunior.TeachersAssistant.Models
{
    public class BookingTimeViewModel
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        [Required]
        public BookingTime BookingTime { get; set; }
    }
}