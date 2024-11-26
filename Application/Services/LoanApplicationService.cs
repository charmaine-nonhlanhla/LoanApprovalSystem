using Persistence;

namespace Application.Services
{
    public class LoanApplicationService
    {
        private readonly DataContext _context;
        public LoanApplicationService(DataContext context)
        {
            _context = context;
        }

        public async Task debtToIncomeRatio()
        {
            var dti = await _context.Applicants.FindAsync();
        }
    }
}