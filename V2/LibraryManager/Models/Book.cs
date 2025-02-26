namespace LibraryManager.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Default value added
        public string Author { get; set; } = string.Empty; // Default value added
        public int PublishedYear { get; set; }
        public int AvailableCopies { get; set; }
    }
}
