using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManager.Models;
using LibraryManager.Services;

namespace LibraryManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBorrowingService _borrowingService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BooksController(IBookService bookService, IBorrowingService borrowingService,
                               UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _bookService = bookService;
            _borrowingService = borrowingService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> BooksTable()
        {
            var books = await _bookService.GetAllBooksAsync();
            ViewBag.IsAdmin = false;
            ViewBag.IsLoggedIn = _signInManager.IsSignedIn(User);

            if (ViewBag.IsLoggedIn)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.IsAdmin = user != null && await _userManager.IsInRoleAsync(user, "Admin");
            }

            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookNames()
        {
            var bookNames = await _bookService.GetAllBookNamesAsync(); // Implement this in your service
            return Json(bookNames);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(BooksTable));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BorrowBookConfirm(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var success = await _borrowingService.BorrowBookAsync(bookId, user.Id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Book could not be borrowed.";
            }

            return RedirectToAction(nameof(BooksTable));
        }
    }
}
