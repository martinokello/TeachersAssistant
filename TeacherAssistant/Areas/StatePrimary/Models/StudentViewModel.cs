using System.ComponentModel.DataAnnotations;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Areas.StatePrimary.TeacherAssistant.Models
{
    public class StudentViewModel
    {
        public int? StudentId { get; set; }
        public string StudentFirsName { get; set; }
        public string StudentLastName { get; set; }
        public StudentTypes StudentType { get; set; }
        public string EmailAddress { get; set; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}