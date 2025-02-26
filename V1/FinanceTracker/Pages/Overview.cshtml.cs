using FinanceTracker.Classes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTracker.Pages
{
    public class OverviewModel : PageModel
    {
        private readonly FinanceContext _context;

        public OverviewModel(FinanceContext context)
        {
            _context = context;
        }

        public List<decimal> ChartData { get; set; } = new();
        public decimal TotalIncome { get; set; }

        public async Task OnGetAsync()
        {
            var transactions = await _context.Transactions.ToListAsync();

            TotalIncome = transactions
                .Where(t => t.IsIncome)
                .Sum(t => t.Amount);

            ChartData = new List<decimal>
            {
                transactions.Where(t => !t.IsIncome && t.Category == Category.Food).Sum(t => t.Amount),
                transactions.Where(t => !t.IsIncome && t.Category == Category.Rent).Sum(t => t.Amount),
                transactions.Where(t => !t.IsIncome && t.Category == Category.Utilities).Sum(t => t.Amount),
                transactions.Where(t => !t.IsIncome && t.Category == Category.Entertainment).Sum(t => t.Amount),
                transactions.Where(t => !t.IsIncome && t.Category == Category.Other).Sum(t => t.Amount)
            };
        }
    }
}