using LibraryManager.Data;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;

        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync() => await _context.Loans.Include(l => l.Book).ToListAsync();

        public async Task<Loan> GetLoanByIdAsync(int id) => await _context.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == id);

        public async Task AddLoanAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoanAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync() => await _context.Loans.Where(l => !l.IsReturned).ToListAsync();

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync() => await _context.Loans.Where(l => !l.IsReturned && l.DueDate < DateTime.Now).ToListAsync();

        public async Task<IEnumerable<Loan>> GetLoansByUserAsync(string userId) => await _context.Loans.Where(l => l.UserId == userId).ToListAsync();

        public async Task<IEnumerable<Loan>> FilterLoansAsync(string status, DateTime? dueDateStart, DateTime? dueDateEnd, string bookTitle, string userId)
        {
            var query = _context.Loans.Include(l => l.Book).AsQueryable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(l => (status == "Active" && !l.IsReturned) || (status == "Returned" && l.IsReturned) || (status == "Overdue" && !l.IsReturned && l.DueDate < DateTime.Now));
            if (dueDateStart.HasValue)
                query = query.Where(l => l.DueDate >= dueDateStart.Value);
            if (dueDateEnd.HasValue)
                query = query.Where(l => l.DueDate <= dueDateEnd.Value);
            if (!string.IsNullOrEmpty(bookTitle))
                query = query.Where(l => l.Book.Title.Contains(bookTitle));
            if (!string.IsNullOrEmpty(userId))
                query = query.Where(l => l.UserId == userId);
            return await query.ToListAsync();
        }
    }
}
