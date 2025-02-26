using System;
using LibraryManager.Models;

namespace LibraryManager.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // Default value added
        public ApplicationUser User { get; set; } = null!; // Nullable removed
        public int BookId { get; set; }
        public Book Book { get; set; } = null!; // Nullable removed
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}