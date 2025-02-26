using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        public int PublicationYear { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string UserId { get; set; }
    }
}