using Application.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Application.Applicants
{
    public class UpdateApplicant
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
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

                if (string.IsNullOrWhiteSpace(applicant.IDNumber) || !Regex.IsMatch(applicant.IDNumber, @"^\d{13}$"))
                    throw new ArgumentException("ID number must be exactly 13 numeric digits.");

                if (await _context.LoanApplicants.AnyAsync(a => a.IDNumber == applicant.IDNumber && a.Id != request.Id, cancellationToken))
                    throw new ArgumentException("An applicant with this ID number already exists.");

                if (string.IsNullOrWhiteSpace(applicant.FirstName) || applicant.FirstName.Length < 2 || applicant.FirstName.Length > 50 || !Regex.IsMatch(applicant.FirstName, @"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$"))
                    throw new ArgumentException("First name is required and must be between 2 and 50 characters and must only contain letters, single spaces, hyphens, or apostrophes.");

                if (string.IsNullOrWhiteSpace(applicant.LastName) || applicant.LastName.Length < 2 || applicant.LastName.Length > 50 || !Regex.IsMatch(applicant.LastName, @"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$"))
                    throw new ArgumentException("Last name is required and must be between 2 and 50 characters and must only contain letters, single spaces, hyphens, or apostrophes.");

                if (applicant.DateOfBirth == default)
                    throw new ArgumentException("Date of birth is required.");

                var today = DateOnly.FromDateTime(DateTime.Now);
                var age = today.Year - applicant.DateOfBirth.Year;
                if (applicant.DateOfBirth.AddYears(age) > today)
                    age--;
                if (age < 18)
                    throw new ArgumentException("Applicant must be at least 18 years old.");

                if (age > 120)
                    throw new ArgumentException("Applicant's age is beyond reasonable limit.");

                if (string.IsNullOrWhiteSpace(applicant.PhoneNumber) || !Regex.IsMatch(applicant.PhoneNumber, @"^\+?\d{10,15}$"))
                    throw new ArgumentException("Phone number is required and must be a valid format.");

                if (await _context.LoanApplicants.AnyAsync(a => a.PhoneNumber == applicant.PhoneNumber && a.Id != request.Id, cancellationToken))
                    throw new ArgumentException("A loan applicant with this phone number already exists.");

                if (string.IsNullOrWhiteSpace(applicant.Email) || !new EmailAddressAttribute().IsValid(applicant.Email))
                    throw new ArgumentException("Email is required and must be in a valid format.");

                if (await _context.LoanApplicants.AnyAsync(a => a.Email == applicant.Email && a.Id != request.Id, cancellationToken))
                    throw new ArgumentException("A loan applicant with this email address already exists.");

                if (string.IsNullOrWhiteSpace(applicant.Address) || applicant.Address.Length > 250)
                    throw new ArgumentException("Address is required and cannot exceed 250 characters.");

                _mapper.Map(applicant, existingApplicant);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}