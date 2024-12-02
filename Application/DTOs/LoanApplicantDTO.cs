using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.DTOs
{
    public class LoanApplicantDTO
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "ID number is required.")]
        [RegularExpression(@"^\d(13)$", ErrorMessage = "ID number must be 13 numeric digits.")]
        public string IDNumber {get; set;}

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "First name can only contain letters, spaces, hyphens, or apostrophes.")]
        public string FirstName {get; set;}

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "First name can only contain letters, spaces, hyphens, or apostrophes.")]
        public string LastName {get; set;}

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CustomValidators), nameof(CustomValidators.ValidateAdult))]
        public DateOnly DateOfBirth {get; set;}

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber {get; set;}

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address {get; set;}

    }
}