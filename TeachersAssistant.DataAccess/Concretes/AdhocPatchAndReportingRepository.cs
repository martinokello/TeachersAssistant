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
        public ReportingGroupSubmission[] GroupSubmissionsBySubjectRoleAndYear() {

            var listItems = new List<ReportingGroupSubmission>();

            using(var con = DataBase().Connection)
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
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0: (int) reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)(int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public ReportingGroupSubmission[] GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd)
        {
            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupSubmissionsBySubjectRoleAndYearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("YearStart",System.Data.SqlDbType.Int).Value = yearStart);
                cmd.Parameters.Add(new SqlParameter("YearEnd", System.Data.SqlDbType.Int).Value = yearEnd);
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)(int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public ReportingGroupSubmission[] GroupSubmissionsBySubjectRoleAndYearBtwnYearsForParticularSubject(int yearStart, int yearEnd, int subjectId)
        {

            var listItems = new List<ReportingGroupSubmission>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.GroupSubmissionsBySubjectRoleAndYearBtwnYearsBySubject";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("YearStart", System.Data.SqlDbType.Int).Value = yearStart);
                cmd.Parameters.Add(new SqlParameter("YearEnd", System.Data.SqlDbType.Int).Value = yearEnd);
                cmd.Parameters.Add(new SqlParameter("subjectId", System.Data.SqlDbType.Int).Value = subjectId);
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new ReportingGroupSubmission
                    {
                        NumberOfStudents = reader["NumberOfStudents"] == DBNull.Value ? 0 : (int)reader["NumberOfStudents"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)(int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public PercentileGradeSubjectYear[] PercentileGradeBySubjectRoleAndYear()
        {

            var listItems = new List<PercentileGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.PercentileGroupedByGradeAndSubjectAndyear";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new PercentileGradeSubjectYear
                    {
                        Percentile = reader["Percentile"] == DBNull.Value ? 0 : (int)reader["Percentile"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)(int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public PercentileGradeSubjectYear[] PercentileGradeBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {

            var listItems = new List<PercentileGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.PercentileGroupedByGradeAndSubjectAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("YearStart", System.Data.SqlDbType.Int).Value = yearStart);
                cmd.Parameters.Add(new SqlParameter("YearEnd", System.Data.SqlDbType.Int).Value = yearEnd);
                cmd.Parameters.Add(new SqlParameter("subjectId", System.Data.SqlDbType.Int).Value = subjectId);
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new PercentileGradeSubjectYear
                    {
                        Percentile = reader["Percentile"] == DBNull.Value ? 0 : (int)reader["Percentile"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)(int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public AverageGradeSubjectYear[] AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            var listItems = new List<AverageGradeSubjectYear>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("YearStart", System.Data.SqlDbType.Int).Value = yearStart);
                cmd.Parameters.Add(new SqlParameter("YearEnd", System.Data.SqlDbType.Int).Value = yearEnd);
                cmd.Parameters.Add(new SqlParameter("subjectId", System.Data.SqlDbType.Int).Value = subjectId);
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new AverageGradeSubjectYear
                    {
                        AverageGrade = reader["AverageGrade"] == DBNull.Value ? 0 : (int)reader["AverageGrade"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)(int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        public MedianGradeAttainedGrade[] MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId)
        {

            var listItems = new List<MedianGradeAttainedGrade>();

            using (var con = DataBase().Connection)
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "dbo.PercentileGroupedByGradeAndSubjectAndyearBtwnYears";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("YearStart", System.Data.SqlDbType.Int).Value = yearStart);
                cmd.Parameters.Add(new SqlParameter("YearEnd", System.Data.SqlDbType.Int).Value = yearEnd);
                cmd.Parameters.Add(new SqlParameter("subjectId", System.Data.SqlDbType.Int).Value = subjectId);
                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listItems.Add(new MedianGradeAttainedGrade
                    {
                        MedianGrade = reader["MedianGrade"] == DBNull.Value ? 0 : (int)reader["MedianGrade"],
                        StudentRole = reader["StudentRole"] == DBNull.Value ? "" : (string)reader["StudentRole"],
                        SubjectName = reader["SubjectName"] == DBNull.Value ? "" : (string)reader["SubjectName"],
                        YearDue = reader["YearDue"] == DBNull.Value ? 0 : (int)(int)reader["YearDue"]
                    });
                }
            }
            return listItems.ToArray();
        }
        //No Implementation of the below Required!!
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
