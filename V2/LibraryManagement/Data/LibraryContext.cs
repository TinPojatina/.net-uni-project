using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using System;

namespace LibraryManagement.Data
{
    public class LibraryContext : IdentityDbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "George", LastName = "Orwell", BirthDate = new DateTime(1903, 6, 25), Biography = "British writer famous for dystopian novels.", Nationality = "British" },
                new Author { AuthorId = 2, FirstName = "J.K.", LastName = "Rowling", BirthDate = new DateTime(1965, 7, 31), Biography = "British author, best known for the Harry Potter series.", Nationality = "British" },
                new Author { AuthorId = 3, FirstName = "J.R.R.", LastName = "Tolkien", BirthDate = new DateTime(1892, 1, 3), Biography = "English writer and professor, famous for The Lord of the Rings.", Nationality = "British" },
                new Author { AuthorId = 4, FirstName = "Agatha", LastName = "Christie", BirthDate = new DateTime(1890, 9, 15), Biography = "English writer known for detective novels.", Nationality = "British" },
                new Author { AuthorId = 5, FirstName = "Mark", LastName = "Twain", BirthDate = new DateTime(1835, 11, 30), Biography = "American writer and humorist.", Nationality = "American" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "1984", ISBN = "978-0451524935", PublicationYear = 1949, Price = 15.99m, IsAvailable = true, Genre = "Dystopian", AuthorId = 1 },
                new Book { BookId = 2, Title = "Animal Farm", ISBN = "978-0451526342", PublicationYear = 1945, Price = 9.99m, IsAvailable = true, Genre = "Political satire", AuthorId = 1 },
                new Book { BookId = 3, Title = "Harry Potter and the Sorcerer’s Stone", ISBN = "978-0439708180", PublicationYear = 1997, Price = 29.99m, IsAvailable = true, Genre = "Fantasy", AuthorId = 2 },
                new Book { BookId = 4, Title = "Harry Potter and the Chamber of Secrets", ISBN = "978-0439064873", PublicationYear = 1998, Price = 24.99m, IsAvailable = true, Genre = "Fantasy", AuthorId = 2 },
                new Book { BookId = 5, Title = "The Hobbit", ISBN = "978-0345339683", PublicationYear = 1937, Price = 19.99m, IsAvailable = true, Genre = "Fantasy", AuthorId = 3 },
                new Book { BookId = 6, Title = "The Lord of the Rings", ISBN = "978-0261102385", PublicationYear = 1954, Price = 49.99m, IsAvailable = true, Genre = "Fantasy", AuthorId = 3 },
                new Book { BookId = 7, Title = "Murder on the Orient Express", ISBN = "978-0062693662", PublicationYear = 1934, Price = 14.99m, IsAvailable = true, Genre = "Mystery", AuthorId = 4 },
                new Book { BookId = 8, Title = "The Adventures of Huckleberry Finn", ISBN = "978-0486280615", PublicationYear = 1885, Price = 12.99m, IsAvailable = true, Genre = "Adventure", AuthorId = 5 }
            );
        }
    }
}
