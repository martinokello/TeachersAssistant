﻿using System;
using System.ComponentModel.DataAnnotations;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Models
{
    public interface IStudentViewModel
    {
        int CourseId { get; set; }
        int? StudentId { get; set; }
        string StudentFirsName { get; set; } 
        string StudentLastName { get; set; } 
        StudentTypes StudentType { get; set; } 
        string EmailAddress { get; set; }
        string Select { set; get; }
        string Create { set; get; }
        string Update { set; get; }
        string Delete { set; get; }
    }

    public class StudentSelectOrDeleteViewModel : IStudentViewModel
    {
        [Required(ErrorMessage = "Student Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Id Required!")]
        public int? StudentId { get; set; }
        public int CourseId { get; set; }
        public string StudentFirsName { get; set; } = "Student FirstName";
        public string StudentLastName { get; set; } = "Student LastName";
        public StudentTypes StudentType { get; set; } = StudentTypes.StatePrimary;
        public string EmailAddress { get; set; } = "student@martinlayooinc.co.uk";
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class StudentUpdateViewModel:IStudentViewModel
    {
        [Required(ErrorMessage = "Student Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Student Id Required!")]
        public int? StudentId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Student First Name Required!")]
        public string StudentFirsName { get; set; } = "Student FirstName";

        [Required(ErrorMessage = "Student Last NameRequired!")]
        public string StudentLastName { get; set; } = "Student LastName";
        public StudentTypes StudentType { get; set; } = StudentTypes.StatePrimary;

        [Required(ErrorMessage = "Email Address Required!")]
        public string EmailAddress { get; set; } = "student@martinlayooinc.co.uk";
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
    public class StudentCreateViewModel : IStudentViewModel
    {
        public int? StudentId { get; set; }
        [Required(ErrorMessage = "Course Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Course Id Required!")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Student First Name Required!")]
        public string StudentFirsName { get; set; } = "Student FirstName";

        [Required(ErrorMessage = "Student Last NameRequired!")]
        public string StudentLastName { get; set; } = "Student LastName";
        public StudentTypes StudentType { get; set; } = StudentTypes.StatePrimary;

        [Required(ErrorMessage = "Email Address Required!")]
        public string EmailAddress { get; set; } = "student@martinlayooinc.co.uk";
        public string Select { set; get; }
        public string Create { set; get; }
        public string Update { set; get; }
        public string Delete { set; get; }
    }
}