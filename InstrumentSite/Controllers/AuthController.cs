using InstrumentSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InstrumentSite.Services;
using InstrumentSite.Dtos.User;
using InstrumentSite.Data;
using InstrumentSite.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using InstrumentSite.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InstrumentSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly JwtTokenGeneratorUtil _tokenService;

        public AuthController(AppDbContext dbContext, JwtTokenGeneratorUtil tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "User not authenticated" });
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest(new { Message = $"Invalid user ID in token: {userIdClaim.Value}" });
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
                Role = user.Role.ToString() // Convert enum to string
            });
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the email already exists
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (existingUser != null)
            {
                return BadRequest(new { Message = "Email is already registered." });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User 
            { 
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = UserRoleEnum.User
            };
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when ((ex.InnerException as PostgresException)?.SqlState == "23505")
            {
                return BadRequest(new { Message = "Email is already registered." });
            }

            return Ok(new { Message = "Registration successful!" });
        }


        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO dto)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new { Token = token, UserId = user.Id });
        }
    }
}
