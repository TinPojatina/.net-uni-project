using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<Author> GetAuthorWithBooksAsync(int id);
        Task<List<Author>> GetAuthorsByNationalityAsync(string nationality);
        Task<List<Author>> GetAuthorsWithBookCountAsync();
        Task CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}