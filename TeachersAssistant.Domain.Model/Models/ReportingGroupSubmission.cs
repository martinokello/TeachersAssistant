using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersAssistant.Domain.Model.Models
{
    public class ReportingGroupSubmission
    {
        public string StudentRole { get; set; }
        public string SubjectName { get; set; }
        public int NumberOfStudents { get; set; }
        public int YearDue { get; set; }
    }

    public class ReportingGroupSubmissionBtwnYears: ReportingGroupSubmission
    {
        public int YearStart { get; set; }
        public int YearEnd { get; set; }
    }

    public class PercentileGradeSubjectYear
    {
        public string StudentRole { get; set; }
        public string SubjectName { get; set; }
        public int Percentile { get; set; }
        public int YearDue { get; set; }
    }
    public class PercentileGradeSubjectYearBtwnYears: PercentileGradeSubjectYear
    {
        public int YearStart { get; set; }
        public int YearEnd { get; set; }
    }
    public class AverageGradeSubjectYear
    {

        public string StudentRole { get; set; }
        public string SubjectName { get; set; }
        public decimal AverageGrade { get; set; }
        public int YearDue { get; set; }
    }
    public class MedianGradeAttainedGrade
    {
        public string StudentRole { get; set; }
        public string SubjectName { get; set; }
        public decimal MedianGrade { get; set; }
        public int YearDue { get; set; }
    }
}
