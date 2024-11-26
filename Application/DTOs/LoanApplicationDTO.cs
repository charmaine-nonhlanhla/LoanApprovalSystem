namespace Application.DTOs
{
    public class LoanApplicationDTO
    {
        public int Id {get; set;}

        public int CustomerID {get; set;}

        public int CreditScore {get; set;}

        public int LoanTermMonths {get; set;}

        public DateOnly ApplicationDate {get; set;}

        public decimal LoanAmountRequested {get; set;}

        public decimal InterestRate {get; set;}

        public decimal MonthlyDebt {get; set;}

        public decimal GrossIncome {get; set;}

        public string LoanType {get; set;}

        public string Status {get; set;}

        public string Remarks {get; set;} 
    }
}