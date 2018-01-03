using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Models
{
    public class TeacherViewModel
    {
        public int? TeacherId { get; set; }
        [Required]
        public string FirsName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}