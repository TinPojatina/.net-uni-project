using FinanceTrackerAPI.Models;

namespace FinanceTrackerAPI.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<Transaction?> GetTransactionById(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId);
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<bool> UpdateTransaction(Transaction transaction);
        Task<bool> DeleteTransaction(int id);
    }
}
