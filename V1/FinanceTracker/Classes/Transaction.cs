namespace FinanceTracker.Classes
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Transaction(int id, string description, decimal amount, DateTime date)
        {
            Id = id;
            Description = description;
            Amount = amount;
            Date = date;
        }
    }
}
