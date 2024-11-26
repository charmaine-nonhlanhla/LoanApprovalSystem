using MediatR;
using Persistence;

namespace Application.Applicants
{
    public class DeleteApplicant
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var loanApplicant = await _context.LoanApplicants.FindAsync(request.Id);

                _context.Remove(loanApplicant);

                await _context.SaveChangesAsync();
            }
        }
    }
}