using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManager.Data;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly ApplicationDbContext _context;

        public BorrowingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BorrowBookAsync(int bookId, string userId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.AvailableCopies <= 0)
                return false;

            book.AvailableCopies--;

            var borrowing = new Borrowing
            {
                BookId = bookId,
                UserId = userId,
                BorrowDate = System.DateTime.UtcNow
            };

            _context.Borrowings.Add(borrowing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int borrowId)
        {
            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.Id == borrowId);

            if (borrowing == null || borrowing.ReturnDate != null)
                return false;

            borrowing.ReturnDate = System.DateTime.UtcNow;
            borrowing.Book.AvailableCopies++;

            await _context.SaveChangesAsync();
            return true;
        }

        // Implement missing methods
        public async Task<List<Borrowing>> GetAllBorrowingsAsync()
        {
            return await _context.Borrowings.Include(b => b.Book).ToListAsync();
        }

        public async Task<List<Borrowing>> GetBorrowingsByUserAsync(string userId)
        {
            return await _context.Borrowings
                .Where(b => b.UserId == userId)
                .Include(b => b.Book)
                .ToListAsync();
        }
    }
}
