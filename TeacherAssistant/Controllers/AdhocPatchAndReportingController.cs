﻿using System;
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
        public JsonResult GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYearBtwnYears(yearStart, yearEnd);
            }


            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GroupSubmissionsBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            ReportingGroupSubmission[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.GroupSubmissionsBySubjectRoleAndYearBtwnYearsForParticularSubject(yearStart, yearEnd, subjectId);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult PercentileGradeBySubjectRoleAndYear()
        {
            PercentileGradeSubjectYear[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.PercentileGradeBySubjectRoleAndYear();
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult PercentileGradeBySubjectRoleAndYearBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            PercentileGradeSubjectYear[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.PercentileGradeBySubjectRoleAndYearBtwnYears(yearStart, yearEnd, subjectId);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            AverageGradeSubjectYear[] reportSubmissions = null;

            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.AverageGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subjectId);
            }
            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(int yearStart, int yearEnd, int subjectId)
        {
            MedianGradeAttainedGrade[] reportSubmissions = null;
            using (var dbContext = new TeachersAssistantDbContext())
            {
                adhocPatchAndReportingRepository.DbContextTeachersAssistant = dbContext;
                reportSubmissions = adhocPatchAndReportingRepository.MedianGradeAttainedGradeBySubjectRoleAndYearForParticualarSubjectBtwnYears(yearStart, yearEnd, subjectId);
            }

            return Json(reportSubmissions, JsonRequestBehavior.AllowGet);
        }
        // GET: AdhocPatchAndReporting
        public ActionResult Index()
        {
            return View();
        }
    }
}