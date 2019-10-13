using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TeacherAssistant.Infrastructure.Validation
{
    public class DateTimeFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime.ParseExact(value.ToString(),"yyyy-MM-dd HH:mm", provider);
                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Date Format is incorrect");
            }
        }
    }
}