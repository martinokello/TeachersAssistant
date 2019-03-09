﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public class AssignmentSubmissionViewModel
    {
        public int AssignmentSubmissionId { get; set; }
        public string AssignmentName { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string FilePath { get; set; }
        public HttpPostedFileBase MediaContent { get; set; }
        public string StudentRole { get; set; }
        public bool IsSubmitted { get; set; }
        public string Grade { get; set; }
    }
}