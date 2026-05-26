using Microsoft.AspNetCore.Mvc;
using GenZHelpAPI.Data;
using GenZHelpAPI.Models;

namespace GenZHelpAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        [HttpPost("register")]

        public async Task<ActionResult<User>> Register(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}