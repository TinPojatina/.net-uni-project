using FinanceTrackerAPI.Data;
using FinanceTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _context.Transactions.Include(t => t.User).ToListAsync();
        }

        public async Task<Transaction?> GetTransactionById(int id)
        {
            return await _context.Transactions.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId)
        {
            return await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
