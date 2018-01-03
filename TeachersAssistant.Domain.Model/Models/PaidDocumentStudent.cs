using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Domain.Model.Models
{
    public class PaidDocumentStudent
    {
        [Key]
        public int? PaidDocumentStudentId { get; set; }
        [ForeignKey("PaidDocument")]
        [Required]
        public int PaidDocumentId { get; set; }
        public PaidDocument PaidDocument { get; set; }
        [ForeignKey("Student")]
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public StudentTypes StudentType { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
