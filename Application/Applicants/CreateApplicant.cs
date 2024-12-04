using System.Text.RegularExpressions;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Applicants
{
    public class CreateApplicant
    {
        public class Command : IRequest
        {
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

                var applicant = request.LoanApplicant;

                if(string.IsNullOrWhiteSpace(applicant.IDNumber) || !Regex.IsMatch(applicant.IDNumber, @"^\d{13}$"))
                throw new ArgumentException("ID number must be exactly 13 numeric digits.");

                if (await _context.LoanApplicants.AnyAsync(a => a.IDNumber == applicant.IDNumber, cancellationToken))
                throw new ArgumentException("An applicant with this ID number already exists.");

                if(string.IsNullOrWhiteSpace(applicant.FirstName) || applicant.FirstName.Length < 2 || applicant.FirstName.Length > 50 || !Regex.IsMatch(applicant.FirstName, @"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$"))
                throw new ArgumentException("First name is required and  must be between 2 and 50 characters and must only contain letters, single spaces, hyphens, or apostrophes.");

                if(string.IsNullOrWhiteSpace(applicant.LastName) || applicant.LastName.Length < 2 || applicant.LastName.Length > 50 || !Regex.IsMatch(applicant.LastName, @"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$"))
                throw new ArgumentException("Last name is required and must be between 2 and 50 characters and must only contain letters, single spaces, hypens or apostrophes.");

                if (applicant.DateOfBirth == default)
                throw new ArgumentException("Date of birth is required.");

                var today = DateOnly.FromDateTime(DateTime.Now);

                if(applicant.DateOfBirth > today)
                throw new ArgumentException("Date of birth cannot be in the future.");

                var age = today.Year - applicant.DateOfBirth.Year;
                if (applicant.DateOfBirth.AddYears(age) > today)
                age--;
                if (age < 18)
                throw new ArgumentException("Applicant must be at least 18 years old.");

                if (age > 120)
                throw new ArgumentException("Applicant's age is beyond reasonable limit.");

                if (string.IsNullOrWhiteSpace(applicant.PhoneNumber) || !Regex.IsMatch(applicant.PhoneNumber, @"^\+?\d{10,15}$"))
                throw new ArgumentException("Phone number is required and must be a valid format.");

                if(await _context.LoanApplicants.AnyAsync(a => a.PhoneNumber == applicant.PhoneNumber, cancellationToken))
                throw new ArgumentException("A loan applicant with this number already exists.");

                if(string.IsNullOrWhiteSpace(applicant.Email) || !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(applicant.Email))
                throw new ArgumentException("Email address is required and must be in a valid format.");

                if(await _context.LoanApplicants.AnyAsync(a => a.Email == applicant.Email, cancellationToken))
                throw new ArgumentException("A loan applicant with this email address already exists.");

                if(string.IsNullOrWhiteSpace(applicant.Address) || applicant.Address.Length > 250)
                throw new ArgumentException("Address is required and cannot exceed 250 characters.");


                var loanApplicant = _mapper.Map<LoanApplicants>(applicant);
                loanApplicant.RegistrationDate = DateTime.Now;
                _context.LoanApplicants.Add(loanApplicant);

                await _context.SaveChangesAsync();
            }
        }
    }
}