using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Domain.Model.Models
{
    public class FreeVideoStudent
    {
        [Key]
        public int? FreeVideoStudentId { get; set; }
        [ForeignKey("FreeVideo")]
        [Required]
        public int FreeVideoId { get; set; }
        public FreeVideo FreeVideo { get; set; }
        [ForeignKey("Student")]
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Subject")]
        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public StudentTypes StudentType { get; set; }
    }
}
