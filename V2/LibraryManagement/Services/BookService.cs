using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<List<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books
                .Where(b => b.IsAvailable)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByGenreAsync(string genre)
        {
            return await _context.Books
                .Where(b => b.Genre == genre)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task CreateBookAsync(Book book)
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
    }
}