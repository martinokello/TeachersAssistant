﻿using System.ComponentModel.DataAnnotations;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Areas.Grammar11Plus.TeacherAssistant.Models
{
    public class SubjectViewModel
    {
        public int? SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}