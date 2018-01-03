using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Domain.Model.Models
{
    public class Teacher
    {
        [Key]
        public int? TeacherId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
