using System.ComponentModel.DataAnnotations;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Areas.Grammar11Plus.TeacherAssistant.Models
{
    public class StudentViewModel
    {
        public int? StudentId { get; set; }
        [Required]
        public string StudentFirsName { get; set; }
        [Required]
        public string StudentLastName { get; set; }
        public StudentTypes StudentType { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}