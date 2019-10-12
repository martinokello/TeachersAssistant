using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeacherAssistant.Infrastructure.Validation;

namespace TeacherAssistant.Models
{
    public class QAHelpRequestViewModel
    {
        public int QAHelpRequestId { get; set; }
        [Required(ErrorMessage="Required!")]
        [Range(minimum:1, maximum:int.MaxValue, ErrorMessage ="Choose Teacher/Tutor!")]
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Subject!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string StudentRole { get; set; }
        [DateTimeFormat(ErrorMessage = "Start Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime StartTime { get; set; }
        [DateTimeFormat(ErrorMessage = "End Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string Description { get; set; }
        public bool IsScheduled { get; set; }
    }
}