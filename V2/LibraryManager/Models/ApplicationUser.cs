using Microsoft.AspNetCore.Identity;
using System;

namespace LibraryManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty; // Default value added
        public string LastName { get; set; } = string.Empty; // Default value added
        public DateTime DateOfBirth { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
