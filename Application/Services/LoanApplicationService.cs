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

        public async Task<(bool isEligible, decimal approvedAmount, string remarks)> EvaluateLoanAsync(int loanApplicationId)
        {
            var loanApplication = await _context.LoanApplications.FindAsync(loanApplicationId);
            var applicant = await _context.LoanApplicants.FindAsync(loanApplication.ApplicantId);

            if (loanApplication == null || applicant == null)
            {
                return (false, 0, "Loan application or applicant not found.");
            }

            var dti = CalculateDTI(loanApplication.MonthlyDebt, loanApplication.GrossIncome);

            var weightedScore = CalculateWeightedScore(dti, loanApplication.CreditScore);

            var (isEligible, approvedPercentage, remarks) = DetermineLoanEligibility(weightedScore);

            var approvedAmount = isEligible ? loanApplication.LoanAmountRequested * approvedPercentage : 0;

            return (isEligible, approvedAmount, remarks);
        }

        private decimal CalculateDTI(decimal monthlyDebt, decimal grossIncome)
        {
            if (grossIncome == 0) throw new DivideByZeroException("Gross income cannot be zero.");

            return monthlyDebt / grossIncome * 100;
        }

        private decimal CalculateWeightedScore(decimal dti, int creditScore)
        {
            const decimal dtiWeight = 0.4m;
            const decimal creditScoreWeight = 0.6m;

            decimal dtiScore = dti <= 36 ? 100 : dti <= 43 ? 70 : 40;

            decimal creditScorePercentage = (creditScore - 300) / (850 - 300) * 100;

            return (dtiScore * dtiWeight) + (creditScorePercentage * creditScoreWeight);
        }

        private (bool isEligible, decimal approvedPercentage, string remarks) DetermineLoanEligibility(decimal weightedScore)
        {
            if (weightedScore >= 85)
            {
                return (true, 1.0m, "Approved for 100% of the requested amount.");
            }

            else if (weightedScore >= 70)
            {
                return (true, 0.8m, "Approved for 80% of the requested amount.");
            }

            else if (weightedScore >= 50)
            {
                return (true, 0.5m, "Approved for 50% of the loan amount.");
            }

            else
            {
                return (false, 0, "Loan application denied due to low eligibility score.");
            }
        }
    }
}