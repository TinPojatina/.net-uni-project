using LibraryManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface IUserService
    {
        Task<IdentityUser> GetCurrentUserAsync();
        Task<IEnumerable<Loan>> GetUserLoansAsync();
        Task<IEnumerable<Book>> GetUserFavoriteBooksAsync();
    }
}
