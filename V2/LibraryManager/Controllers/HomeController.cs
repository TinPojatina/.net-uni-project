using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManager.Services;
using LibraryManager.Models;

namespace LibraryManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync() ?? new List<Book>(); // Ensure it's never null
            return View(books);
        }
    }
}
