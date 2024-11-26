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
            public LoanApplicants LoanApplicant { get; set; }
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
                var loanApplicant = await _context.LoanApplicants.FindAsync(request.LoanApplicant.Id);

                _mapper.Map(request.LoanApplicant, loanApplicant);

                await _context.SaveChangesAsync();
            }
        }
    }
}