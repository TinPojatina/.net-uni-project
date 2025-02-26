using FinanceTrackerAPI.Models;
using FinanceTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FinanceTrackerAPI.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("get-all-transactions", Name = "GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            return Ok(await _transactionService.GetAllTransactions());
        }

        [HttpGet("get-transaction-by-id/{id}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);
            return transaction == null ? NotFound() : Ok(transaction);
        }

        [HttpGet("get-transactions-by-user/{userId}", Name = "GetTransactionsByUserId")]
        public async Task<IActionResult> GetTransactionsByUserId(int userId)
        {
            return Ok(await _transactionService.GetTransactionsByUserId(userId));
        }

        [HttpPost("add-transaction", Name = "AddTransaction")]
        public async Task<IActionResult> AddTransaction(Transaction transaction)
        {
            var createdTransaction = await _transactionService.AddTransaction(transaction);
            return CreatedAtRoute("GetTransactionById", new { id = createdTransaction.Id }, createdTransaction);
        }

        [HttpPut("update-transaction/{id}", Name = "UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id) return BadRequest();
            return await _transactionService.UpdateTransaction(transaction) ? NoContent() : NotFound();
        }

        [HttpDelete("delete-transaction/{id}", Name = "DeleteTransaction")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            return await _transactionService.DeleteTransaction(id) ? NoContent() : NotFound();
        }
    }
}
