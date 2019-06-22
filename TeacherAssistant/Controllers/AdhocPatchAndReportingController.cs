using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Infrastructure;
using TeachersAssistant.DataAccess;
using TeachersAssistant.DataAccess.Concretes;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Controllers
{
    public class AdhocPatchAndReportingController : Controller
    {
        private AdhocPatchAndReportingRepository adhocPatchAndReportingRepository;
        public AdhocPatchAndReportingController(IAdhocPatchAndReportingMarker adhocPatchAndReportingMarker)
        {
            adhocPatchAndReportingRepository = adhocPatchAndReportingMarker as AdhocPatchAndReportingRepository;
        }

        [HttpGet]
        public JsonResult ReportingGroupSubmission()
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYear();
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, string studentRole)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYearBtwnYears(yearStart, yearEnd, studentRole);
            }


            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYearBtwnYearsForParticularSubject(yearStart, yearEnd, subjectId, studentRole);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult NumberOfStudentsGradedInSubjectAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            ReportingGroupSubmission[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.NumberOfStudentsGradedInSubjectAndyearBtwnYears(yearStart, yearEnd, subjectId);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            ReportingGroupSubmission[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubjectContains(yearStart, yearEnd, subject);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileResult ReportNumberOfStudentsGradedInSubjectAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            ReportingGroupSubmission[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.NumberOfStudentsGradedInSubjectAndyearBtwnYears(yearStart, yearEnd, subjectId);
            }

            var fileName = string.Format("NumberOfStudentsReceivedGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));

            return File(ReportingFacilities.ReportNumberOfStudentsReceivedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public FileResult ReportNumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            ReportingGroupSubmission[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubject(yearStart, yearEnd, subject);
            }

            var fileName = string.Format("NumberOfStudentsReceivedGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));

            return File(ReportingFacilities.ReportNumberOfStudentsReceivedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public JsonResult PercentileGradeBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            PercentileGradeSubjectYear[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.PercentileGradeBySubjectRoleAndYearBtwnYears(yearStart, yearEnd, subjectId, studentRole);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        
       [HttpGet]
        public JsonResult GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn(yearStart, yearEnd, subjectId, studentRole);
            }
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnSubjectContains(yearStart, yearEnd, subject, studentRole);
            }
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileResult ReportGroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn(yearStart, yearEnd, subjectId, studentRole);
            }
            var fileName = string.Format("NumberOfStudentsGroupReceivedGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));

            return File(ReportingFacilities.ReportNumberOfStudentsReceivedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
        }
        [HttpGet]
        public FileResult ReportGroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubject(yearStart, yearEnd, subject, studentRole);
            }
            var fileName = string.Format("NumberOfStudentsGroupReceivedGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));

            return File(ReportingFacilities.ReportNumberOfStudentsReceivedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public JsonResult AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subjectId, studentRole);
            }
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubjectContains(yearStart, yearEnd, subject, studentRole);
            }
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileResult ReportsAverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subjectId, studentRole);
            }
            var fileName = string.Format("AverageGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportAverageGradeAttainedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public FileResult ReportsAverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubject(yearStart, yearEnd, subject, studentRole);
            }
            var fileName = string.Format("AverageGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportAverageGradeAttainedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public JsonResult MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subjectId, studentRole);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubject(yearStart, yearEnd, subject, studentRole);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileResult ReportMedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subjectId, studentRole);
            }
            
            var fileName = string.Format("MedaianGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd,DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportMedianGradeAttainedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public FileResult ReportMedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYearsAcrossSubject(yearStart, yearEnd, subject, studentRole);
            }

            var fileName = string.Format("MedaianGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportMedianGradeAttainedGradeBySubjectRoleAndYear(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        // GET: AdhocPatchAndReporting
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public FileResult ReportAverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(yearStart, yearEnd, subjectId);
            }
            var fileName = string.Format("AverageGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportAverageGradeAttainedGradeBySubjectRoleAndYearAcrossAllRoles(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public FileResult ReportAverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(yearStart, yearEnd, subject);
            }
            var fileName = string.Format("AverageGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportAverageGradeAttainedGradeBySubjectRoleAndYearAcrossAllRoles(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public FileResult ReportMedianGradeAttainedGradeBySubjectAcrossAllRolesAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(yearStart, yearEnd, subjectId);
            }

            var fileName = string.Format("MedaianGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportMedianGradeAttainedGradeBySubjectRoleAndYearAcrossAllRoles(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public FileResult ReportMedianGradeAttainedGradeBySubjectAcrossAllRolesAndYearForParticualarSubjectBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(yearStart, yearEnd, subject);
            }

            var fileName = string.Format("MedaianGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportMedianGradeAttainedGradeBySubjectRoleAndYearAcrossAllRoles(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public JsonResult AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(yearStart, yearEnd, subjectId);
            }
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(yearStart, yearEnd, subject);
            }
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(yearStart, yearEnd, subjectId);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(yearStart, yearEnd, subject);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole = "")
        {
            AverageMedianAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(yearStart, yearEnd, subjectId, studentRole);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole = "")
        {
            AverageMedianAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(yearStart, yearEnd, subject, studentRole);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileResult ReportAverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(int yearStart, int yearEnd, int subjectId, string studentRole = "")
        {
            AverageMedianAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(yearStart, yearEnd, subjectId, studentRole);
            }

            var fileName = string.Format("MedaianGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportAverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public FileResult ReportAverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubjectContains(int yearStart, int yearEnd, string subject, string studentRole = "")
        {
            AverageMedianAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject(yearStart, yearEnd, subject, studentRole);
            }

            var fileName = string.Format("MedaianGrades_{0}_{1}_{2}.xlsx", yearStart, yearEnd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            return File(ReportingFacilities.ReportAverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(fileName, reportSubmissions), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}