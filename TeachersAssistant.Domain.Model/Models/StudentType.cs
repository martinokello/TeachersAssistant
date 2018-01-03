using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Domain.Model.Models
{
    public class StudentType
    {
        [Key]
        public int? StudentTypeId {get;set;}
        public string StudentTypeName { get; set; }
    }
}
