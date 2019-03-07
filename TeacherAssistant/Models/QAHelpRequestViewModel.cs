using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string Description { get; set; }
        public bool IsScheduled { get; set; }
    }
}