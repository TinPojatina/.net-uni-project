using LibraryManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<IEnumerable<Book>> GetBooksByGenreAsync(string genre);
        Task<IEnumerable<Book>> FilterBooksAsync(bool? isAvailable, string genre, int? publicationYear, string author);
    }
}
