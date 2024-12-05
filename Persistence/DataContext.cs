using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<LoanApplicants> LoanApplicants { get; set; }
        public DbSet<LoanApplications> LoanApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanApplications>(entity =>
            {
                entity.Property(l => l.MonthlyDebt)
                      .HasPrecision(18, 2);

                entity.Property(l => l.InterestRate)
                      .HasPrecision(18, 2);

                entity.Property(l => l.LoanAmountRequested)
                      .HasPrecision(18, 2);

                entity.Property(l => l.GrossIncome)
                      .HasPrecision(18, 2);
            });

                modelBuilder.Entity<LoanApplications>()
                      .HasOne(la => la.LoanApplicant)
                      .WithMany(lapp => lapp.LoanApplications)
                      .HasForeignKey(la => la.LoanApplicantId);

            base.OnModelCreating(modelBuilder);
        }
    }
}