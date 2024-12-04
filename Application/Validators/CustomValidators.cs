using System.ComponentModel.DataAnnotations;

namespace Application.Validators
{
    public class CustomValidators
    {
        public static ValidationResult ValidateAdult(DateOnly dateOfBirth, ValidationContext context)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (dateOfBirth > today)
            {
                return new ValidationResult("Date of birth cannot be in the future.");
            }

            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.AddYears(age) > today)
            {
                age--;
            }

            if (age < 18)
            {
                return new ValidationResult("Applicant must be at least 18 years old.");
            }

            if (age > 120)
            {
                return new ValidationResult("Applicant's age is beyond a reasonable limit.");
            }
           
           return ValidationResult.Success;
        }
    }
}