using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManager.Models;
using LibraryManager.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISBN = "9780743273565",
                    PublicationYear = 1925,
                    Genre = "Classic",
                    Description = "A novel about the American dream and the roaring 1920s.",
                    CoverImageUrl = "https://example.com/gatsby.jpg",
                    IsAvailable = true,
                    UserId = "admin-user-id"
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    ISBN = "9780061120084",
                    PublicationYear = 1960,
                    Genre = "Fiction",
                    Description = "A novel about racial injustice in the Deep South.",
                    CoverImageUrl = "https://example.com/mockingbird.jpg",
                    IsAvailable = false,
                    UserId = "admin-user-id"
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    ISBN = "9780451524935",
                    PublicationYear = 1949,
                    Genre = "Dystopian",
                    Description = "A dystopian social science fiction novel and cautionary tale.",
                    CoverImageUrl = "https://example.com/1984.jpg",
                    IsAvailable = true,
                    UserId = "admin-user-id"
                },
                new Book
                {
                    Id = 4,
                    Title = "Moby-Dick",
                    Author = "Herman Melville",
                    ISBN = "9781503280786",
                    PublicationYear = 1851,
                    Genre = "Adventure",
                    Description = "A narrative of the adventures of the whaling ship Pequod.",
                    CoverImageUrl = "https://example.com/mobydick.jpg",
                    IsAvailable = true,
                    UserId = "admin-user-id"
                }
            );

            modelBuilder.Entity<Loan>().HasData(
                new Loan
                {
                    Id = 1,
                    BookId = 2,
                    UserId = "user1-id",
                    LoanDate = new DateTime(2024, 2, 16),
                    DueDate = new DateTime(2024, 3, 17),
                    ReturnDate = null,
                    IsReturned = false,
                    ExtensionCount = 0,
                    Notes = "First loaned book."
                }
            );
        }
    }
}
