namespace Domain
{
    public class LoanApplications
    {
        public int Id {get; set;}

        public int CustomerID {get; set;}

        public int CreditScore {get; set;}

        public int LoanTermMonths {get; set;}

        public DateOnly ApplicationDate {get; set;}

        public decimal LoanAmountRequested {get; set;}

        public decimal InterestRate {get; set;}

        public decimal CurrentExpenses {get; set;}

        public decimal CurrentIncome {get; set;}

        public string LoanType {get; set;}

        public string Status {get; set;}

        public string Remarks {get; set;} 
    }
}