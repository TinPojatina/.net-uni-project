using Microsoft.EntityFrameworkCore;
using FinanceTracker.Classes;

namespace FinanceTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FinanceTracker.Classes.Transaction> Transaction { get; set; } = default!;

        
        // Define your DbSets here
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}