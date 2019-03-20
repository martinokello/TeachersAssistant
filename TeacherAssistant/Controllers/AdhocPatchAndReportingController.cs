using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            adhocPatchAndReportingRepository = adhocPatchAndReportingMarker as AdhocPatchAndReportingRepository;
        }

        [HttpGet]
        public JsonResult ReportingGroupSubmission()
        {
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            ReportingGroupSubmission[] reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYear();

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd)
        {
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            ReportingGroupSubmission[] reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYearBtwnYears(yearStart, yearEnd);

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, string subject)
        {
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            ReportingGroupSubmission[] reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYearBtwnYearsForParticularSubject(yearStart, yearEnd, subject);

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult PercentileGradeBySubjectRoleAndYear()
        {
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            PercentileGradeSubjectYear[] reportSubmissions = adhocPatchAndReportingRepository.PercentileGradeBySubjectRoleAndYear();

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult PercentileGradeBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, string subject)
        {
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            PercentileGradeSubjectYear[] reportSubmissions = adhocPatchAndReportingRepository.PercentileGradeBySubjectRoleAndYearBtwnYears(yearStart, yearEnd, subject);
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, string subject)
        {
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            AverageGradeSubjectYear[] reportSubmissions = adhocPatchAndReportingRepository.AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subject);
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, string subject)
        {
            var dbContext = new TeachersAssistantDbContext();
            adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
            MedianGradeAttainedGrade[] reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subject);

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        // GET: AdhocPatchAndReporting
        public ActionResult Index()
        {
            return View();
        }
    }
}