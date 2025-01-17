using InstrumentSite.Enums;
using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; } // Store hashed password, not plain text

        [Required]
        public UserRoleEnum Role { get; set; } // Enum for roles (e.g., Admin, User)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
