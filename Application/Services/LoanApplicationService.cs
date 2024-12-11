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

        public async Task<(bool isEligible, decimal approvedAmount, decimal interestRate, string remarks)> EvaluateLoanAsync(int loanApplicationId)
        {
            var loanApplication = await _context.LoanApplications.FindAsync(loanApplicationId);
            var applicant = await _context.LoanApplicants.FindAsync(loanApplication.ApplicantId);

            if (loanApplication == null || applicant == null)
            {
                return (false, 0, 0, "Loan application or applicant not found.");
            }

            var dti = CalculateDTI(loanApplication.MonthlyDebt, loanApplication.GrossIncome);

            if (dti >= 43)
            {
                return (false, 0, 0, "Rejected due to jigh debt-to-income ratio (DTI).");
            }

            var weightedScore = CalculateWeightedScore(dti, loanApplication.CreditScore);

            var (isEligible, approvedPercentage, remarks) = DetermineLoanEligibility(weightedScore);

            var approvedAmount = isEligible ? loanApplication.LoanAmountRequested * approvedPercentage : 0;

            var interestRate = CalculateInterestRate(weightedScore, loanApplication.LoanTermMonths);

            return (isEligible, approvedAmount, interestRate, remarks);
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

            decimal dtiScore = dti <= 36 ? 100 :
                dti <= 38 ? 90 :
                dti <= 40 ? 80 :
                dti <= 43 ? 70 : 0;

            decimal creditScorePercentage = (creditScore - 300) / (850 - 300) * 100;

            return (dtiScore * dtiWeight) + (creditScorePercentage * creditScoreWeight);
        }

        private (bool isEligible, decimal approvedPercentage, string remarks) DetermineLoanEligibility(decimal weightedScore)
        {
            if (weightedScore >= 90)
            {
                return (true, 1.0m, "Approved for 100% of the requested amount.");
            }

            else if (weightedScore >= 80)
            {
                return (true, 0.9m, "Approved for 90% of the requested amount.");
            }

            else if (weightedScore >= 70)
            {
                return (true, 0.8m, "Approved for 80% of the loan amount.");
            }

            else if (weightedScore >= 70)
            {
                return (true, 0.7m, "Approved for 70% of the loan amount.");
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

        private decimal CalculateInterestRate(decimal weightedScore, int loanTermMonths)
        {
            const decimal baseRate = 17m;
            const decimal maxRate = 36m;

            decimal interestRate = baseRate;

            if (weightedScore >= 90)
            {
                interestRate -= 5m;
            }

            else if (weightedScore >= 80)
            {
                interestRate -= 3;
            }

            else if (weightedScore >= 70)
            {
                interestRate = baseRate;
            }

            else if (weightedScore >= 60)
            {
                interestRate += 3m;
            }

            else if (weightedScore >= 50)
            {
                interestRate += 5m;
            }

            else
            {
                interestRate = maxRate;
            }

            if (loanTermMonths > 72)
            {
                interestRate += 4m;
            }

            else if (loanTermMonths > 60)
            {
                interestRate += 3m;
            }

            else if (loanTermMonths > 48)
            {
                interestRate += 2.5m;
            }

            else if (loanTermMonths > 36)
            {
                interestRate += 2m;
            }

            else if (loanTermMonths > 24)
            {
                interestRate += 1.5m;
            }

            else if (loanTermMonths <= 12)
            {
                interestRate -= 2m;
            }

            else if (loanTermMonths <= 24)
            {
                interestRate -= 1m;
            }

            return Math.Clamp(interestRate, baseRate, maxRate);
        }

    }
}