using Domain;
using MediatR;
using Persistence;

namespace Application.Applicants
{
    public class GetApplicant
    {
        public class Query : IRequest<LoanApplicants>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, LoanApplicants>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<LoanApplicants> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.LoanApplicants.FindAsync(request.Id);
            }
        }
    }
}