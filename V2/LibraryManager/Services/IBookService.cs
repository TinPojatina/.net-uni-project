using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManager.Models;

namespace LibraryManager.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> SearchBooksAsync(string query);
        Task<List<Book>> GetSortedBooksAsync(string sortBy, string order); // ✅ Add this method
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<List<string>> GetAllBookNamesAsync();
    }
}
