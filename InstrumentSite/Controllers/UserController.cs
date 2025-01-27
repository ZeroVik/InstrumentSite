using InstrumentSite.Data;
using InstrumentSite.Dtos.User;
using InstrumentSite.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace InstrumentSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _dbContext.Users.Select(u => new
            {
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Role
            }).ToList();

            return Ok(users);
        }


        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // Log all claims
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Get the NameIdentifier claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User not authenticated" });
            }

            Console.WriteLine($"NameIdentifier Claim Value: {userIdClaim}");

            // Parse the user ID (handle as string if necessary)
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest(new { Message = "Invalid user ID in token" });
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role
            });
        }





        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Role
            });
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = "Admin")] // Allow only Admins to access this endpoint
        public IActionResult UpdateUserRole(int id, [FromBody] UserRoleUpdateDTO dto)
        {
            if (dto == null || !dto.Role.HasValue) // Check if Role is null or invalid
            {
                return BadRequest(new { Message = "The Role field is required and must match a valid value (Admin or User)." });
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            user.Role = dto.Role.Value; // Assign the non-nullable value
            _dbContext.SaveChanges();

            return Ok(new { Message = "User role updated successfully.", User = new { user.Id, user.Role } });
        }




        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO dto)
        {
            var requestingUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requestingUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(requestingUserId) || string.IsNullOrEmpty(requestingUserRole))
            {
                return Unauthorized(new { Message = "Authentication error." });
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // Admins can update roles
            if (requestingUserRole == "Admin" && dto.Role.HasValue)
            {
                user.Role = dto.Role.Value; // Update role only if requested by Admin
                Console.WriteLine($"Requesting User ID: {requestingUserId}, Role: {requestingUserRole}");

            }
            // Prevent non-admins from modifying other users
            else if (requestingUserId != user.Id.ToString())
            {
                return StatusCode(403, new { Message = "You are not authorized to modify other users." });
            }

            // Allow updates to personal fields
            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                user.FirstName = dto.FirstName;
            }

            if (!string.IsNullOrEmpty(dto.LastName))
            {
                user.LastName = dto.LastName;
            }

            if (!string.IsNullOrEmpty(dto.Password))
            {
                // Hash the password before storing it
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password); // Use BCrypt or a similar library
            }

            // Update the timestamp
            user.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();


            return Ok(new { Message = "User updated successfully." });
        }

    }

}
