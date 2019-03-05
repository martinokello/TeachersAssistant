using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TeacherAssistant.Infrastructure
{
    public class DateTimeFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           try
            {
                DateTime.ParseExact(value.ToString(),"yyyy-MM-dd HH:mm", new DateTimeFormatInfo { FullDateTimePattern= "yyyy-MM-dd HH:mm" });
                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Date Format is incorrect");
            }
        }
    }
}