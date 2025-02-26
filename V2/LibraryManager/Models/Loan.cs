using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public string UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public int ExtensionCount { get; set; } = 0;
        public string Notes { get; set; }
    }
}
