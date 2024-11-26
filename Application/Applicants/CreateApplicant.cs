using Domain;
using MediatR;
using Persistence;

namespace Application.Applicants
{
    public class CreateApplicant
    {
        public class Command : IRequest
        {
            public LoanApplicants LoanApplicant { get; set; }
        }
        public class Handler : IRequest<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.LoanApplicants.Add(request.LoanApplicant);

                await _context.SaveChangesAsync();
            }
        }
    }
}