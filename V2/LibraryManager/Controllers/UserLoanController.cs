using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Services;
using System.Threading.Tasks;
using LibraryManager.Models;

namespace LibraryManager.Controllers
{
    [Authorize]
    public class UserLoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;

        public UserLoanController(ILoanService loanService, IUserService userService)
        {
            _loanService = loanService;
            _userService = userService;
        }

        public async Task<IActionResult> MyLoans(string status)
        {
            var currentUser = await _userService.GetCurrentUserAsync();
            var loans = await _loanService.GetLoansByUserAsync(currentUser.Id);

            // Filter by status if provided
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "Active")
                    loans = loans.Where(l => !l.IsReturned);
                else if (status == "Returned")
                    loans = loans.Where(l => l.IsReturned);
                else if (status == "Overdue")
                    loans = loans.Where(l => !l.IsReturned && l.DueDate < DateTime.Now);
            }

            return View(loans);
        }

        public async Task<IActionResult> CreateLoan(int bookId)
        {
            var currentUser = await _userService.GetCurrentUserAsync();

            // Create a new loan for the selected book
            var loan = new Loan
            {
                BookId = bookId,
                UserId = currentUser.Id,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                IsReturned = false
            };

            await _loanService.AddLoanAsync(loan);
            return RedirectToAction(nameof(MyLoans));
        }

        public async Task<IActionResult> ReturnBook(int loanId)
        {
            var loan = await _loanService.GetLoanByIdAsync(loanId);
            if (loan == null) return NotFound();

            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;

            await _loanService.UpdateLoanAsync(loan);
            return RedirectToAction(nameof(MyLoans));
        }

        public async Task<IActionResult> ExtendLoan(int loanId)
        {
            var loan = await _loanService.GetLoanByIdAsync(loanId);
            if (loan == null) return NotFound();

            loan.DueDate = loan.DueDate.AddDays(14);
            loan.ExtensionCount++;

            await _loanService.UpdateLoanAsync(loan);
            return RedirectToAction(nameof(MyLoans));
        }
    }
}