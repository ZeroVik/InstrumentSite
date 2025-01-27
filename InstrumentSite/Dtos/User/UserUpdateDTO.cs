using InstrumentSite.Enums;
using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.User
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } // Optional

        [MaxLength(100)]
        public string LastName { get; set; } // Optional

        [MinLength(6)]
        public string? Password { get; set; } // Optional password update

        public UserRoleEnum? Role { get; set; } // Optional role update
    }
}
