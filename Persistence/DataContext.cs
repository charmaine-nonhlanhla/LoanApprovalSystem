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
        public DbSet<LoanApplications> LoanApplications {get; set;}
    }
}