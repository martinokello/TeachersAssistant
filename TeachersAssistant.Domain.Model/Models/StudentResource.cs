using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TeachersAssistant.Domain.Model.Models
{
    public class StudentResource
    {
        [Key]
        public int StudentResourceId { get; set; }
        public string FilePath { get; set; }
        public int SubjectId { get; set; }
        public string RoleName { get; set; }
        public int CourseId { get; set; }
        public string StudentResourceName { get; set; }
        public Subject Subject { get; set; }
    }
}
