using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersAssistant.Domain.Model.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public string StudentRole { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime DateDue { get; set; }
        public string FilePath { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
