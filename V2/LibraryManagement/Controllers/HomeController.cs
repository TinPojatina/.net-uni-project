using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IUserService _userService;

        public HomeController(IBookService bookService, IAuthorService authorService, IUserService userService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.IsAuthenticated = _userService.IsAuthenticated();
            ViewBag.UserName = _userService.GetUserName();

            var availableBooks = await _bookService.GetAvailableBooksAsync();
            ViewBag.AvailableBooksCount = availableBooks.Count;

            var allBooks = await _bookService.GetAllBooksAsync();
            ViewBag.TotalBooks = allBooks.Count;

            var authors = await _authorService.GetAllAuthorsAsync();
            ViewBag.TotalAuthors = authors.Count;

            return View();
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