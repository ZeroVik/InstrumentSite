using InstrumentSite.Enums;
using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.User
{
    public class UserRoleUpdateDTO
    {
        [Required] // Ensure this field is required
        public UserRoleEnum? Role { get; set; }
    }
}
