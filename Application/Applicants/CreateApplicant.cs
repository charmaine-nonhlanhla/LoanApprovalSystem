using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
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
                var loanApplicant = _mapper.Map<LoanApplicants>(request.LoanApplicant);
                _context.LoanApplicants.Add(loanApplicant);

                await _context.SaveChangesAsync();
            }
        }
    }
}