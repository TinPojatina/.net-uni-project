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

        public async Task<IEnumerable<Book>> GetAllBooksAsync() => await _context.Books.ToListAsync();

        public async Task<Book> GetBookByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
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

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync() => await _context.Books.Where(b => b.IsAvailable).ToListAsync();

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(string genre) => await _context.Books.Where(b => b.Genre == genre).ToListAsync();

        public async Task<IEnumerable<Book>> FilterBooksAsync(bool? isAvailable, string genre, int? publicationYear, string author)
        {
            var query = _context.Books.AsQueryable();
            if (isAvailable.HasValue)
                query = query.Where(b => b.IsAvailable == isAvailable.Value);
            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genre == genre);
            if (publicationYear.HasValue)
                query = query.Where(b => b.PublicationYear == publicationYear.Value);
            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author.Contains(author));
            return await query.ToListAsync();
        }
    }
}
