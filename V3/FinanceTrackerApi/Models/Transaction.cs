using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinanceTrackerAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }

        [JsonIgnore] // Sprječava cikličku referencu
        public User? User { get; set; }
    }
}
