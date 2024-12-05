using Application.DTOs;
using MediatR;

namespace Application.Applications
{
    public class DeleteLoanApplication
    {
        public int Id {get; set;}

        public class Command : IRequest<LoanApplicationDTO> 
        {
            public class Handler : IRequestHandler<Command, LoanApplicationDTO>
            {
                public Task<LoanApplicationDTO> Handle(Command request, CancellationToken cancellationToken)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}