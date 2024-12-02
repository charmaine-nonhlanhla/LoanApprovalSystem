using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Applicants
{
    public class UpdateApplicant
    {
        public class Command : IRequest
        {
            public int Id {get; set;}
            public LoanApplicantDTO LoanApplicant { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var existingApplicant = await _context.LoanApplicants.FindAsync(request.Id);
                if (existingApplicant == null)
                throw new KeyNotFoundException("Applicant not found.");

                var applicant = request.LoanApplicant;

                if (string.IsNullOrWhiteSpace(applicant.IDNumber) || !System.Text.RegularExpressions.Regex.IsMatch(applicant.IDNumber, @"^\d{13}$"))
                throw new ArgumentException("ID number must be exactly 13 numeric digits.");

                if(string.IsNullOrWhiteSpace(applicant.FirstName) || applicant.FirstName.Length > 50)
                throw new ArgumentException("First name is required and cannot exceed 50 characters.");

                if(string.IsNullOrWhiteSpace(applicant.LastName) || applicant.LastName.Length > 50)
                throw new ArgumentException("Last name is required and cannot exceed 50 characters.");

                if(applicant.DateOfBirth == default)
                throw new ArgumentException("Date of birth is required.");

                var today = DateOnly.FromDateTime(DateTime.Now);
                var age = today.Year - applicant.DateOfBirth.Year;
                if (applicant.DateOfBirth.AddYears(age) > today)
                age--;
                if (age < 18)
                throw new ArgumentException("Applicant must be at least 18 years old.");

                if (string.IsNullOrWhiteSpace(applicant.PhoneNumber) || !System.Text.RegularExpressions.Regex.IsMatch(applicant.PhoneNumber, @"^\+?\d{10,15}$"))
                throw new ArgumentException("Phone number is required and must be a valid format.");

                if(string.IsNullOrWhiteSpace(applicant.Email) || !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(applicant.Email))
                throw new ArgumentException("Email is required and must be in a valid format.");

                if(string.IsNullOrWhiteSpace(applicant.Address) || applicant.Address.Length > 250)
                throw new ArgumentException("Address is required and cannot exceed 250 characters.");


                var loanApplicant = await _context.LoanApplicants.FindAsync(request.LoanApplicant.Id);

                _mapper.Map(request.LoanApplicant, existingApplicant);

                await _context.SaveChangesAsync();
            }
        }
    }
}