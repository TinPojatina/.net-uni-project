using Microsoft.EntityFrameworkCore;
using FinanceTracker.Classes;

namespace FinanceTracker
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, Amount = 2000, Description = "Salary", Category = Category.Other, Date = new DateTime(2024, 2, 10), IsIncome = true, PaymentMethod = "Bank Transfer" },
                new Transaction { Id = 2, Amount = 500, Description = "Groceries", Category = Category.Food, Date = new DateTime(2024, 2, 15), IsIncome = false, PaymentMethod = "Credit Card" },
                new Transaction { Id = 3, Amount = 1200, Description = "Rent", Category = Category.Rent, Date = new DateTime(2024, 2, 13), IsIncome = false, PaymentMethod = "Bank Transfer" },
                new Transaction { Id = 4, Amount = 150, Description = "Electricity Bill", Category = Category.Utilities, Date = new DateTime(2024, 2, 17), IsIncome = false, PaymentMethod = "Debit Card" },
                new Transaction { Id = 5, Amount = 75, Description = "Movie Night", Category = Category.Entertainment, Date = new DateTime(2024, 2, 18), IsIncome = false, PaymentMethod = "Cash" }
            );
        }
    }
}