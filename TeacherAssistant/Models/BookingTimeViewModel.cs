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
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public BookingTime BookingTime{ get;set;}
    }
}