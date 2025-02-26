using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Models;
using System;

namespace LibraryManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets for models
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure non-nullable fields have defaults
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.FirstName)
                .HasDefaultValue("");

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.LastName)
                .HasDefaultValue("");

            // Seed admin role and user
            string adminRoleId = "b1d4b06d-7a21-4c72-bd7a-123456789abc";
            string adminUserId = "f3a2c8e9-9b1e-4d83-8e2f-987654321def";

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 1, 1),
                RegisteredAt = DateTime.UtcNow,
                PasswordHash = hasher.HashPassword(null, "pass")
            };

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminUserId,
                RoleId = adminRoleId
            });

            // Seed books with non-nullable default values
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublishedYear = 1925, AvailableCopies = 5 },
                new Book { Id = 2, Title = "1984", Author = "George Orwell", PublishedYear = 1949, AvailableCopies = 3 },
                new Book { Id = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishedYear = 1960, AvailableCopies = 4 },
                new Book { Id = 4, Title = "Moby Dick", Author = "Herman Melville", PublishedYear = 1851, AvailableCopies = 6 },
                new Book { Id = 5, Title = "Pride and Prejudice", Author = "Jane Austen", PublishedYear = 1813, AvailableCopies = 7 },
                new Book { Id = 6, Title = "War and Peace", Author = "Leo Tolstoy", PublishedYear = 1869, AvailableCopies = 4 }
            );

            // Seed test borrowings
            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing { Id = 1, UserId = adminUserId, BookId = 1, BorrowDate = DateTime.UtcNow.AddDays(-10) },
                new Borrowing { Id = 2, UserId = adminUserId, BookId = 2, BorrowDate = DateTime.UtcNow.AddDays(-5) },
                new Borrowing { Id = 3, UserId = adminUserId, BookId = 4, BorrowDate = DateTime.UtcNow.AddDays(-7) },
                new Borrowing { Id = 4, UserId = adminUserId, BookId = 5, BorrowDate = DateTime.UtcNow.AddDays(-3) },
                new Borrowing { Id = 5, UserId = adminUserId, BookId = 6, BorrowDate = DateTime.UtcNow.AddDays(-1) }
            );
        }
    }
}
