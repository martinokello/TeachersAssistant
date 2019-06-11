using System.ComponentModel.DataAnnotations;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{
    public class SubjectViewModel
    {
        [Required]
        public int CourseId { get; set; }
        public int? SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; } = "Subject Name Default";
        [Required]
        public int TeacherId { get; set; } = -1;
        public Teacher Teacher { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}