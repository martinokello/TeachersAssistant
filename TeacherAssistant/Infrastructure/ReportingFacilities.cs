using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Infrastructure
{
    public class ReportingFacilities
    {
        public static MemoryStream ReportMedianGradeAttainedGradeBySubjectRoleAndYear(string fileName, MedianGradeAttainedGrade[] reportSubmissions)
        {
            var fileInfo = new MemoryStream();
            var indexOfRow = 0;

            using (var exPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wsht = null;

                if (exPackage.Workbook.Worksheets.Any())
                    wsht = exPackage.Workbook.Worksheets.First(p => p.Name == "Median Grades Attained by Subject Role and Year");
                if (wsht == null)
                {
                    wsht = exPackage.Workbook.Worksheets.Add("MedianGrades");
                }
                if (indexOfRow == 0)
                {
                    if (wsht == null) wsht = exPackage.Workbook.Worksheets.Add("MedianGrades");
                    indexOfRow++;

                    var titles = wsht.Cells["A1:I1"];
                    titles[string.Format("A{0}", indexOfRow)].Value = "Student Role";
                    titles[string.Format("B{0}", indexOfRow)].Value = "Subject Name";
                    titles[string.Format("C{0}", indexOfRow)].Value = "Median Grade";
                    titles[string.Format("D{0}", indexOfRow)].Value = "Year Attained";
                    indexOfRow++;
                }

                for (var n = 0; n < reportSubmissions.Length; n++, indexOfRow++)
                {
                    wsht.Cells[string.Format("A{0}", indexOfRow)].Value = reportSubmissions[n].StudentRole;
                    wsht.Cells[string.Format("B{0}", indexOfRow)].Value = reportSubmissions[n].SubjectName;
                    wsht.Cells[string.Format("C{0}", indexOfRow)].Value = reportSubmissions[n].MedianGrade;
                    wsht.Cells[string.Format("D{0}", indexOfRow)].Value = reportSubmissions[n].YearDue;
                }
                exPackage.Save();
            }
            fileInfo.Position = 0;
            return fileInfo;
        }
        public static MemoryStream ReportAverageGradeAttainedGradeBySubjectRoleAndYear(string fileName, AverageGradeSubjectYear[] reportSubmissions)
        {
            var fileInfo = new MemoryStream();
            var indexOfRow = 0;

            using (var exPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wsht = null;

                if (exPackage.Workbook.Worksheets.Any())
                    wsht = exPackage.Workbook.Worksheets.First(p => p.Name == "Average Grades Attained by Subject Role and Year");
                if (wsht == null)
                {
                    wsht = exPackage.Workbook.Worksheets.Add("AverageGrades");
                }
                if (indexOfRow == 0)
                {
                    if (wsht == null) wsht = exPackage.Workbook.Worksheets.Add("AverageGrades");
                    indexOfRow++;

                    var titles = wsht.Cells["A1:I1"];
                    titles[string.Format("A{0}", indexOfRow)].Value = "Student Role";
                    titles[string.Format("B{0}", indexOfRow)].Value = "Subject Name";
                    titles[string.Format("C{0}", indexOfRow)].Value = "Average Grade";
                    titles[string.Format("D{0}", indexOfRow)].Value = "Year Attained";
                    indexOfRow++;
                }

                for (var n = 0; n < reportSubmissions.Length; n++, indexOfRow++)
                {
                    wsht.Cells[string.Format("A{0}", indexOfRow)].Value = reportSubmissions[n].StudentRole;
                    wsht.Cells[string.Format("B{0}", indexOfRow)].Value = reportSubmissions[n].SubjectName;
                    wsht.Cells[string.Format("C{0}", indexOfRow)].Value = reportSubmissions[n].AverageGrade;
                    wsht.Cells[string.Format("D{0}", indexOfRow)].Value = reportSubmissions[n].YearDue;
                }
                exPackage.Save();
            }
            fileInfo.Position = 0;
            return fileInfo;
        }
        public static MemoryStream ReportNumberOfStudentsReceivedGradeBySubjectRoleAndYear(string fileName, ReportingGroupSubmission[] reportSubmissions)
        {
            var fileInfo = new MemoryStream();
            var indexOfRow = 0;

            using (var exPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wsht = null;

                if (exPackage.Workbook.Worksheets.Any())
                    wsht = exPackage.Workbook.Worksheets.First(p => p.Name == "Number Of Students Attained Grades by Subject Role and Year");
                if (wsht == null)
                {
                    wsht = exPackage.Workbook.Worksheets.Add("MedianGrades");
                }
                if (indexOfRow == 0)
                {
                    if (wsht == null) wsht = exPackage.Workbook.Worksheets.Add("MedianGrades");
                    indexOfRow++;

                    var titles = wsht.Cells["A1:I1"];
                    titles[string.Format("A{0}", indexOfRow)].Value = "Subject Name";
                    titles[string.Format("B{0}", indexOfRow)].Value = "Grade";
                    titles[string.Format("C{0}", indexOfRow)].Value = "Number Of Students";
                    titles[string.Format("D{0}", indexOfRow)].Value = "Year Attained";
                    indexOfRow++;
                }

                for (var n = 0; n < reportSubmissions.Length; n++, indexOfRow++)
                {
                    wsht.Cells[string.Format("A{0}", indexOfRow)].Value = reportSubmissions[n].SubjectName;
                    wsht.Cells[string.Format("B{0}", indexOfRow)].Value = reportSubmissions[n].Grade;
                    wsht.Cells[string.Format("C{0}", indexOfRow)].Value = reportSubmissions[n].NumberOfStudents;
                    wsht.Cells[string.Format("D{0}", indexOfRow)].Value = reportSubmissions[n].YearDue;
                }
                exPackage.Save();
            }
            fileInfo.Position = 0;
            return fileInfo;
        }
        

        public static MemoryStream ReportMedianGradeAttainedGradeBySubjectRoleAndYearAcrossAllRoles(string fileName, MedianGradeAttainedGrade[] reportSubmissions)
        {
            var fileInfo = new MemoryStream();
            var indexOfRow = 0;

            using (var exPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wsht = null;

                if (exPackage.Workbook.Worksheets.Any())
                    wsht = exPackage.Workbook.Worksheets.First(p => p.Name == "Median Grades Attained by Subject Role and Year");
                if (wsht == null)
                {
                    wsht = exPackage.Workbook.Worksheets.Add("MedianGrades");
                }
                if (indexOfRow == 0)
                {
                    if (wsht == null) wsht = exPackage.Workbook.Worksheets.Add("MedianGrades");
                    indexOfRow++;

                    var titles = wsht.Cells["A1:I1"];
                    titles[string.Format("A{0}", indexOfRow)].Value = "Student Role";
                    titles[string.Format("B{0}", indexOfRow)].Value = "Subject Name";
                    titles[string.Format("C{0}", indexOfRow)].Value = "Median Grade";
                    titles[string.Format("D{0}", indexOfRow)].Value = "Year Attained";
                    indexOfRow++;
                }

                for (var n = 0; n < reportSubmissions.Length; n++, indexOfRow++)
                {
                    wsht.Cells[string.Format("A{0}", indexOfRow)].Value = reportSubmissions[n].StudentRole;
                    wsht.Cells[string.Format("B{0}", indexOfRow)].Value = reportSubmissions[n].SubjectName;
                    wsht.Cells[string.Format("C{0}", indexOfRow)].Value = reportSubmissions[n].MedianGrade;
                    wsht.Cells[string.Format("D{0}", indexOfRow)].Value = reportSubmissions[n].YearDue;
                }
                exPackage.Save();
            }
            fileInfo.Position = 0;
            return fileInfo;
        }
        public static MemoryStream ReportAverageGradeAttainedGradeBySubjectRoleAndYearAcrossAllRoles(string fileName, AverageGradeSubjectYear[] reportSubmissions)
        {
            var fileInfo = new MemoryStream();
            var indexOfRow = 0;

            using (var exPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wsht = null;

                if (exPackage.Workbook.Worksheets.Any())
                    wsht = exPackage.Workbook.Worksheets.First(p => p.Name == "Average Grades Attained by Subject Role and Year");
                if (wsht == null)
                {
                    wsht = exPackage.Workbook.Worksheets.Add("AverageGrades");
                }
                if (indexOfRow == 0)
                {
                    if (wsht == null) wsht = exPackage.Workbook.Worksheets.Add("AverageGrades");
                    indexOfRow++;

                    var titles = wsht.Cells["A1:I1"];
                    titles[string.Format("A{0}", indexOfRow)].Value = "Student Role";
                    titles[string.Format("B{0}", indexOfRow)].Value = "Subject Name";
                    titles[string.Format("C{0}", indexOfRow)].Value = "Average Grade";
                    titles[string.Format("D{0}", indexOfRow)].Value = "Year Attained";
                    indexOfRow++;
                }

                for (var n = 0; n < reportSubmissions.Length; n++, indexOfRow++)
                {
                    wsht.Cells[string.Format("A{0}", indexOfRow)].Value = reportSubmissions[n].StudentRole;
                    wsht.Cells[string.Format("B{0}", indexOfRow)].Value = reportSubmissions[n].SubjectName;
                    wsht.Cells[string.Format("C{0}", indexOfRow)].Value = reportSubmissions[n].AverageGrade;
                    wsht.Cells[string.Format("D{0}", indexOfRow)].Value = reportSubmissions[n].YearDue;
                }
                exPackage.Save();
            }
            fileInfo.Position = 0;
            return fileInfo;
        }

        public static MemoryStream ReportAverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(string fileName, AverageMedianAttainedGrade[] reportSubmissions)
        {
            var fileInfo = new MemoryStream();
            var indexOfRow = 0;

            using (var exPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wsht = null;

                if (exPackage.Workbook.Worksheets.Any())
                    wsht = exPackage.Workbook.Worksheets.First(p => p.Name == "Average And Median Grades Attained by Subject Role and Year");
                if (wsht == null)
                {
                    wsht = exPackage.Workbook.Worksheets.Add("AverageGrades");
                }
                if (indexOfRow == 0)
                {
                    if (wsht == null) wsht = exPackage.Workbook.Worksheets.Add("AverageGrades");
                    indexOfRow++;

                    var titles = wsht.Cells["A1:I1"];
                    titles[string.Format("A{0}", indexOfRow)].Value = "Student Role";
                    titles[string.Format("B{0}", indexOfRow)].Value = "Subject Name";
                    titles[string.Format("C{0}", indexOfRow)].Value = "Average Grade";
                    titles[string.Format("D{0}", indexOfRow)].Value = "Median Grade";
                    titles[string.Format("E{0}", indexOfRow)].Value = "Year Attained";
                    indexOfRow++;
                }

                for (var n = 0; n < reportSubmissions.Length; n++, indexOfRow++)
                {
                    wsht.Cells[string.Format("A{0}", indexOfRow)].Value = reportSubmissions[n].StudentRole;
                    wsht.Cells[string.Format("B{0}", indexOfRow)].Value = reportSubmissions[n].SubjectName;
                    wsht.Cells[string.Format("C{0}", indexOfRow)].Value = reportSubmissions[n].AverageGrade;
                    wsht.Cells[string.Format("D{0}", indexOfRow)].Value = reportSubmissions[n].MedianGrade;
                    wsht.Cells[string.Format("E{0}", indexOfRow)].Value = reportSubmissions[n].YearDue;
                }
                exPackage.Save();
            }
            fileInfo.Position = 0;
            return fileInfo;
        }
    }
}