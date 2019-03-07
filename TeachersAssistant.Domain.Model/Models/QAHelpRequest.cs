using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeachersAssistant.Domain.Model.Models
{
    public class QAHelpRequest
    {
        [Key]
        public int QAHelpRequestId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string StudentRole { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public bool IsScheduled { get; set; }
    }
}