using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Classes;

namespace FinanceTracker.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly FinanceContext _context;

        public TransactionsModel(FinanceContext context)
        {
            _context = context;
        }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "desc";

        [BindProperty(SupportsGet = true)]
        public string FilterType { get; set; } = "all";

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "date";

        public async Task OnGetAsync()
        {
            IQueryable<Transaction> query = _context.Transactions;

            if (FilterType == "income")
                query = query.Where(t => t.IsIncome);
            else if (FilterType == "expense")
                query = query.Where(t => !t.IsIncome);

            query = SortBy switch
            {
                "amount" => (SortOrder == "desc") ? query.OrderByDescending(t => (double)t.Amount) : query.OrderBy(t => (double)t.Amount),
                _ => (SortOrder == "desc") ? query.OrderByDescending(t => t.Date) : query.OrderBy(t => t.Date),
            };

            Transactions = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateAsync(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToPage("Transactions");
        }
    }
}
