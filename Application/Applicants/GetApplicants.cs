using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Applicants
{
    public class GetApplicants
    {
        public class Query : IRequest<List<LoanApplicants>> { }

        public class Handler : IRequestHandler<Query, List<LoanApplicants>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<LoanApplicants>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.LoanApplicants.ToListAsync();
            }
        }
    }
}