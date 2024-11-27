namespace Application.DTOs
{
    public class LoanApplicantDTO
    {
        public int Id {get; set;}

        public string IDNumber {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public DateOnly DateOfBirth {get; set;}

        public string PhoneNumber {get; set;}

        public string Email {get; set;}

        public string Address {get; set;}

        public DateTime RegistrationDate {get; set;}
    }
}