using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManager.Models;
using LibraryManager.Services;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private readonly ILoanService _loanService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService, ILoanService loanService)
        {
            _logger = logger;
            _bookService = bookService;
            _loanService = loanService;
        }

        public async Task<IActionResult> Index(string search, string genre, bool? isAvailable, string sortBy)
        {
            var books = await _bookService.GetAllBooksAsync();

            // Apply filters
            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         b.Author.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         b.ISBN.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre == genre).ToList();
            }
            if (isAvailable.HasValue)
            {
                books = books.Where(b => b.IsAvailable == isAvailable.Value).ToList();
            }

            // Apply sorting
            books = sortBy switch
            {
                "title_asc" => books.OrderBy(b => b.Title).ToList(),
                "title_desc" => books.OrderByDescending(b => b.Title).ToList(),
                "author_asc" => books.OrderBy(b => b.Author).ToList(),
                "author_desc" => books.OrderByDescending(b => b.Author).ToList(),
                "year_asc" => books.OrderBy(b => b.PublicationYear).ToList(),
                "year_desc" => books.OrderByDescending(b => b.PublicationYear).ToList(),
                _ => books
            };

            return View(books);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
