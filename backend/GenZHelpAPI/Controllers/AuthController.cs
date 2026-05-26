using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GenZHelpAPI.Data;
using GenZHelpAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GenZHelpAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IConfiguration _configuration;

        public AuthController(
            ApplicationDbContext context,
            IConfiguration configuration
        )
        {
            _context = context;

            _configuration = configuration;
        }

        // REGISTER USER

        [HttpPost("register")]

        public async Task<IActionResult> Register(User user)
        {
            var existingUser =
                await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Email == user.Email
                );

            if(existingUser != null)
            {
                return BadRequest(
                    "User already exists"
                );
            }

            user.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    user.PasswordHash
                );

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("User Registered");
        }

        // LOGIN USER

        [HttpPost("login")]

        public async Task<IActionResult> Login(User loginUser)
        {
            var user =
                await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Email == loginUser.Email
                );

            if(user == null)
            {
                return Unauthorized(
                    "Invalid Email"
                );
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    loginUser.PasswordHash,
                    user.PasswordHash
                );

            if(!isPasswordValid)
            {
                return Unauthorized(
                    "Invalid Password"
                );
            }

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    user.Email
                )
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]
                )
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer:
                    _configuration["Jwt:Issuer"],

                audience:
                    _configuration["Jwt:Audience"],

                claims: claims,

                expires:
                    DateTime.Now.AddHours(2),

                signingCredentials: creds
            );

            var jwt =
                new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(jwt);
        }
    }
}