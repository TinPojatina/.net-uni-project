namespace LibraryManagement.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Biography { get; set; }
        public string Nationality { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
