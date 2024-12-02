using System.ComponentModel.DataAnnotations;

namespace Application.Validators
{
    public class CustomValidators
    {
        public static ValidationResult ValidateAdult(DateOnly dateOfBirth, ValidationContext context)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.AddYears(age) > today)
            {
                age--;
            }

            return age >= 18
            ? ValidationResult.Success
            : new ValidationResult("Applicant must be at least 18 years old.");
        }
    }
}