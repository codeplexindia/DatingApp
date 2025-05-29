using DatingAppBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppBackend.Controllers
{
    public class UserController(DatingAppContext context) : BaseController
    {
        private readonly DatingAppContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id); // Await the ValueTask properly
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
