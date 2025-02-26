using LibraryManager.Data;
using LibraryManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityUser> GetCurrentUserAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user != null ? await _userManager.GetUserAsync(user) : null;
        }

        public async Task<IEnumerable<Loan>> GetUserLoansAsync()
        {
            var user = await GetCurrentUserAsync();
            return user == null ? new List<Loan>() : await _context.Loans.Where(l => l.UserId == user.Id).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetUserFavoriteBooksAsync()
        {
            return new List<Book>();
        }
    }
}
