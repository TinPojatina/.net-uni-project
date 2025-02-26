using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManager.Services;
using LibraryManager.Models;

namespace LibraryManager.Controllers
{
    [Authorize]
    public class BorrowingController : Controller
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var borrowings = await _borrowingService.GetAllBorrowingsAsync() ?? new List<Borrowing>();
            return View(borrowings);
        }

        public async Task<IActionResult> UserBorrowings()
        {
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User must be logged in to view borrowed books.");
            }

            var borrowings = await _borrowingService.GetBorrowingsByUserAsync(userId) ?? new List<Borrowing>();
            return View(borrowings);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(int borrowId)
        {
            if (borrowId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid borrow ID.";
                return RedirectToAction(nameof(UserBorrowings));
            }

            var success = await _borrowingService.ReturnBookAsync(borrowId);
            if (!success)
            {
                TempData["ErrorMessage"] = "Error returning the book.";
            }
            return RedirectToAction(nameof(UserBorrowings));
        }
    }
}
