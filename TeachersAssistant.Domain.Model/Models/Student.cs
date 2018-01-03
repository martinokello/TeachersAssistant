using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Domain.Model.Models
{
    public class Student
    {
        [Key]
        public int? StudentId { get; set; }
        public string StudentFirsName { get; set; }
        public string StudentLastName { get; set; }
        public StudentTypes StudentType { get; set; }
        public string EmailAddress { get; set; }
    }
}
