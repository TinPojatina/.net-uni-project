using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<List<Book>> GetBooksByAuthorAsync(int authorId);
        Task<List<Book>> GetAvailableBooksAsync();
        Task<List<Book>> GetBooksByGenreAsync(string genre);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}