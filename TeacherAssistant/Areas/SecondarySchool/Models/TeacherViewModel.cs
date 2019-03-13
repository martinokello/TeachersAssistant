using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Areas.SecondarySchool.TeacherAssistant.Models
{
    public class TeacherViewModel
    {
        public int? TeacherId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}