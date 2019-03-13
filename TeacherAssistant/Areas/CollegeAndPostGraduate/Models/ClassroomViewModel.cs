using System.ComponentModel.DataAnnotations.Schema;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Areas.CollegeAndPostGraduate.TeacherAssistant.Models
{
    public class ClassroomViewModel
    {
        public int? ClassroomId { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int CalendarId { get; set; }
        public CalendarBooking Calendar { get; set; }
        public StudentTypes StudentType { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}