using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Classes
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
