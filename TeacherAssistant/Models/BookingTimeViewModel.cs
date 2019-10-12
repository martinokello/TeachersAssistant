using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{
    public class BookingTimeViewModel
    {
        [Required(ErrorMessage = "Teacher Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Teacher Id Required!")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Subject Id Required!")]
        public int SubjectId { get; set; }
        public BookingTime BookingTime{ get;set;}
    }
}