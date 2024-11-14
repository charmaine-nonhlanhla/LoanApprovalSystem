using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Applicants> Applicants {get; set;}
        public DbSet<LoanApplications> LoanApplicationss {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<LoanApplications>()
            .Property(l => l.CurrentExpenses)
            .HasColumnType("decimal(18,2)"); 

        modelBuilder.Entity<LoanApplications>()
            .Property(l => l.CurrentIncome)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<LoanApplications>()
            .Property(l => l.InterestRate)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<LoanApplications>()
            .Property(l => l.LoanAmountRequested)
            .HasColumnType("decimal(18,2)");
        }
    }
}