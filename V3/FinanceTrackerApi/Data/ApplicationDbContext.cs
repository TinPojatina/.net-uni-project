using Microsoft.EntityFrameworkCore;
using FinanceTrackerAPI.Models;
using System;

namespace FinanceTrackerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Marko", Email = "marko@example.com" },
                new User { Id = 2, Name = "Ana", Email = "ana@example.com" }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    Amount = 100,
                    Category = "Food",
                    Date = new DateTime(2024, 1, 1, 12, 0, 0),
                    UserId = 1,
                    Description = "Groceries"
                },
                new Transaction
                {
                    Id = 2,
                    Amount = 500,
                    Category = "Rent",
                    Date = new DateTime(2024, 1, 5, 15, 30, 0),
                    UserId = 2,
                    Description = "Monthly rent"
                }
            );
        }
    }
}
