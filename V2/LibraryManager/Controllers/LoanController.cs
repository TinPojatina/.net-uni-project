using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Services;
using LibraryManager.Models;
using System.Threading.Tasks;
using System;

namespace LibraryManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public async Task<IActionResult> Loans(string status, DateTime? dueDateStart, DateTime? dueDateEnd, string bookTitle, string userId)
        {
            var loans = await _loanService.FilterLoansAsync(status, dueDateStart, dueDateEnd, bookTitle, userId);
            return View("Loan", loans);
        }

        public async Task<IActionResult> Details(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null) return NotFound();
            return View(loan);
        }
    }
}
