using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Classes;
using FinanceTracker.Data;

namespace FinanceTracker.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly FinanceTracker.Data.ApplicationDbContext _context;

        public TransactionsModel(FinanceTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Transaction = await _context.Transaction.ToListAsync();
        }
    }
}
