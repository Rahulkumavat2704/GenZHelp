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
    }
}