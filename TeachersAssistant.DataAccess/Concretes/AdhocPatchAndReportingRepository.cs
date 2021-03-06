﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Abstracts;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace TeachersAssistant.DataAccess.Concretes
{
    public class AdhocPatchAndReportingRepository : AbstractRepository<Object>, IAdhocPatchAndReportingMarker
    {
        public System.Data.Entity.Database DataBase()
        {
            return DbContextTeachersAssistant.Database;
        }
        public ReportingGroupSubmission[] GroupSubmissionsBySubjectRoleAndYear()
        {

            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupSubmissionsBySubjectRoleAndYear";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public ReportingGroupSubmission[] GroupSubmissionsBySubjectRoleAndYearAcrossSubjectContains()
        {
            throw new NotImplementedException();
        }

        public ReportingGroupSubmission[] GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn";

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        Grade = reader["Grade"] == DBNull.Value ? "" : (string)reader["Grade"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public ReportingGroupSubmission[] GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubject(int yearStart, int yearEnd, string subject, string studentRole)
        {
            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubject";

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        Grade = reader["Grade"] == DBNull.Value ? "" : (string)reader["Grade"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public ReportingGroupSubmission[] NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        Grade = reader["Grade"] == DBNull.Value ? "" : (string)reader["Grade"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public ReportingGroupSubmission[] GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, string studentRole)
        {
            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupSubmissionsBySubjectRoleAndYearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));

                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public ReportingGroupSubmission[] GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubject";

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        Grade = reader["Grade"] == DBNull.Value ? "" : (string)reader["Grade"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public ReportingGroupSubmission[] GroupSubmissionsBySubjectRoleAndYearBtwnYearsForParticularSubject(int yearStart, int yearEnd, int subjectId, string studentRole)
        {

            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupSubmissionsBySubjectRoleAndYearBtwnYearsBySubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public AverageGradeSubjectYear[] AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            var listItems = new List<AverageGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new AverageGradeSubjectYear
                    {
                        AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public ReportingGroupSubmission[] NumberOfStudentsGradedInSubjectAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {

            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        Grade = reader["Grade"] == DBNull.Value ? "" : (string)reader["Grade"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public ReportingGroupSubmission[] NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubject(int yearStart, int yearEnd, string subject)
        {

            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        Grade = reader["Grade"] == DBNull.Value ? "" : (string)reader["Grade"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public PercentileGradeSubjectYear[] PercentileGradeBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {

            var listItems = new List<PercentileGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.PercentileGroupedByGradeAndSubjectAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new PercentileGradeSubjectYear
                    {
                        Percentile = reader["Percentile"] == DBNull.Value ? 0 : (int)reader["Percentile"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public AverageGradeSubjectYear[] AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            var listItems = new List<AverageGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new AverageGradeSubjectYear
                    {
                        AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public AverageGradeSubjectYear[] AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            var listItems = new List<AverageGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new AverageGradeSubjectYear
                    {
                        AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public AverageGradeSubjectYear[] AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            var listItems = new List<AverageGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new AverageGradeSubjectYear
                    {
                        AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public AverageGradeSubjectYear[] AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubject(int yearStart, int yearEnd, string subject, string studentRole)
        {
            var listItems = new List<AverageGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new AverageGradeSubjectYear
                    {
                        AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public AverageMedianAttainedGrade[] AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole = "")
        {
            var listItems = new List<AverageMedianAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();
                var item = new AverageMedianAttainedGrade();
                
                if (reader.HasRows) {
                   if(reader.Read())
                    {
                        item.AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"];
                        item.StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"];
                        item.SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"];
                        item.YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"];
                    }
                    if (reader.NextResult())
                    {
                        if (reader.Read())
                        {
                            item.MedianGrade = reader["MedianGrade"] == DBNull.Value ? 0 : (decimal)reader["MedianGrade"];
                        }
                    }
                }
                listItems.Add(item);
            }
            return listItems.ToArray();
        }
        public AverageMedianAttainedGrade[] AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject(int yearStart, int yearEnd, string subject, string studentRole = "")
        {
            var listItems = new List<AverageMedianAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();
                var item = new AverageMedianAttainedGrade();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        item.AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"];
                        item.StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"];
                        item.SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"];
                        item.YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"];
                    }
                    if (reader.NextResult())
                    {
                        if (reader.Read())
                        {
                            item.MedianGrade = reader["MedianGrade"] == DBNull.Value ? 0 : (decimal)reader["MedianGrade"];
                        }
                    }
                }
                listItems.Add(item);
            }
            return listItems.ToArray();
        }
        public AverageMedianAttainedGrade[] AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole = "")
        {
            var listItems = new List<AverageMedianAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();
                var item = new AverageMedianAttainedGrade();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        item.AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (decimal)reader["AverageGrade"];
                        item.StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"];
                        item.SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"];
                        item.YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"];
                    }
                    if (reader.NextResult())
                    {
                        if (reader.Read())
                        {
                            item.MedianGrade = reader["MedianGrade"] == DBNull.Value ? 0 : (decimal)reader["MedianGrade"];
                        }
                    }
                }
                listItems.Add(item);
            }
            return listItems.ToArray();
        }
        public MedianGradeAttainedGrade[] MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {

            var listItems = new List<MedianGradeAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));

                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var type = reader["MedianGrade"].GetType();
                    listItems.Add(new MedianGradeAttainedGrade
                    {
                        MedianGrade = reader["MedianGrade"] == DBNull.Value ? 0 : (decimal)reader["MedianGrade"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public MedianGradeAttainedGrade[] MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {

            var listItems = new List<MedianGradeAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));

                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var type = reader["MedianGrade"].GetType();
                    listItems.Add(new MedianGradeAttainedGrade
                    {
                        MedianGrade = reader["MedianGrade"] == DBNull.Value ? 0 : (decimal)reader["MedianGrade"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }

        public MedianGradeAttainedGrade[] MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {

            var listItems = new List<MedianGradeAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subjectId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));

                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subjectId"].Value = subjectId;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var type = reader["MedianGrade"].GetType();
                    listItems.Add(new MedianGradeAttainedGrade
                    {
                        MedianGrade = reader["MedianGrade"] == DBNull.Value ? 0 : (decimal)reader["MedianGrade"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        //No Implementation of the below Required!!    
        public MedianGradeAttainedGrade[] MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubject(int yearStart, int yearEnd, string subject, string studentRole)
        {

            var listItems = new List<MedianGradeAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearBegin", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@YearEnd", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@subject", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@StudentRole", System.Data.SqlDbType.NVarChar));

                cmd.Parameters["@YearBegin"].Value = yearStart;
                cmd.Parameters["@YearEnd"].Value = yearEnd;
                cmd.Parameters["@subject"].Value = subject;
                cmd.Parameters["@StudentRole"].Value = studentRole;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var type = reader["MedianGrade"].GetType();
    listItems.Add(new MedianGradeAttainedGrade
                    {
                        MedianGrade = reader["MedianGrade"] == DBNull.Value? 0 : (decimal) reader["MedianGrade"],
                       StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                       SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                       YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public override object GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(object item)
        {
            throw new NotImplementedException();
        }
    }
}
