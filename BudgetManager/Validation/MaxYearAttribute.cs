using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Validation
{
    public class MaxYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            var currentYear = (int)value;           

            int maxYear = DateTime.Now.Year + 5;            

            if (currentYear > maxYear)
                return new ValidationResult("Invalid year");

            return ValidationResult.Success;
        }
    }
}
