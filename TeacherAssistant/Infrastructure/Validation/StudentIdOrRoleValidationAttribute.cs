using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeacherAssistant.Models;

namespace TeacherAssistant.Infrastructure.Validation
{
    public class StudentIdOrRoleValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var instance = validationContext.ObjectInstance;
                IAssignmentViewModel assignmentViewModel = instance is AssignmentUpdateViewModel ? validationContext.ObjectInstance as AssignmentUpdateViewModel :  null;
                assignmentViewModel = assignmentViewModel is null ||  instance is AssignmentCreateViewModel ? validationContext.ObjectInstance as AssignmentCreateViewModel : assignmentViewModel;

                if (assignmentViewModel.CourseId > 0 || assignmentViewModel.StudentId > 0) return ValidationResult.Success;

                return new ValidationResult("Either StudentId Or Role Is Required Or Both");
            }
            catch
            {
                return new ValidationResult("Either StudentId Or Role Is Required Or Both");
            }
        }
    }
}