namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string Genre { get; set; }

        // Foreign key
        public int AuthorId { get; set; }

        // Navigation property
        public Author Author { get; set; }
    }
}
