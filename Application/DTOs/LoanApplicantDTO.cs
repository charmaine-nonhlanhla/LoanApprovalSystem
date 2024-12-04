using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.DTOs
{
    public class LoanApplicantDTO
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "ID number is required.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ID number must be 13 numeric digits.")]
        public string IDNumber {get; set;}

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$", ErrorMessage = "First name can only contain letters, single spaces, hyphens, or apostrophes.")]
        public string FirstName {get; set;}

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$", ErrorMessage = "Last name can only contain letters, single spaces, hyphens, or apostrophes.")]
        public string LastName {get; set;}

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CustomValidators), nameof(CustomValidators.ValidateAdult))]
        public DateOnly DateOfBirth {get; set;}

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits, and cannot contain spaces or special characters.")]
        public string PhoneNumber {get; set;}

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 250 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s,.'-]+$", ErrorMessage = "Address contains invalid characters.")]
        public string Address {get; set;}

    }
}