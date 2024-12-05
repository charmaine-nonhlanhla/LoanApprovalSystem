using Domain;
using MediatR;

namespace Application.Applications
{
    public class GetLoanApplication
    {
        public class Query : IRequest<List<LoanApplications>>
        {
            public class Handler : IRequestHandler<Query, List<LoanApplications>>
            {
                public Task<List<LoanApplications>> Handle(Query request, CancellationToken cancellationToken)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}