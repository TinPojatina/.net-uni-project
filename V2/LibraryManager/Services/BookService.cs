using LibraryManager.Data;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetAllBookNamesAsync()
        {
            return await _context.Books
                .Select(b => b.Title)
                .ToListAsync();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>> SearchBooksAsync(string query)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
                .ToListAsync();
        }

        public async Task<List<Book>> GetSortedBooksAsync(string sortBy, string order)
        {
            IQueryable<Book> books = _context.Books;

            switch (sortBy.ToLower())
            {
                case "author":
                    books = order == "asc" ? books.OrderBy(b => b.Author) : books.OrderByDescending(b => b.Author);
                    break;
                case "year":
                    books = order == "asc" ? books.OrderBy(b => b.PublishedYear) : books.OrderByDescending(b => b.PublishedYear);
                    break;
                default:
                    books = order == "asc" ? books.OrderBy(b => b.Title) : books.OrderByDescending(b => b.Title);
                    break;
            }

            return await books.ToListAsync();
        }
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
