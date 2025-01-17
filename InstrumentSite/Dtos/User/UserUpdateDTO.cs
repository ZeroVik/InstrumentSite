using InstrumentSite.Enums;
using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.User
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]

        [MaxLength(100)]
        public string LastName { get; set; }

        [MinLength(6)]
        public string? Password { get; set; } // Optional password update

        [Required]
        public UserRoleEnum? Role { get; set; }
    }
}
