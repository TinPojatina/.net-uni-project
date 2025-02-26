using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManager.Models;

namespace LibraryManager.Services
{
    public interface IBorrowingService
    {
        Task<bool> BorrowBookAsync(int bookId, string userId);
        Task<bool> ReturnBookAsync(int borrowId);

        // Add missing method definitions
        Task<List<Borrowing>> GetAllBorrowingsAsync();
        Task<List<Borrowing>> GetBorrowingsByUserAsync(string userId);

    }
}
