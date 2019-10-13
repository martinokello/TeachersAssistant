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
        [Required(ErrorMessage= "TeacherId Required!")]
        [Range(minimum:1, maximum:int.MaxValue, ErrorMessage ="Choose Teacher/Tutor!")]
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student Id Required!")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Choose Subject!")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Subject Id Required!")]
        public string StudentRole { get; set; }
        [Required(ErrorMessage = "Start Date Required!")]
        [DataType( DataType.DateTime, ErrorMessage = "Start Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "End Date Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "End Date format wrong and Time should be in 24 Hr Clock")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; }
        public bool IsScheduled { get; set; }
    }
}