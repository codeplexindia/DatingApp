using DatingAppBackend.Data;
using DatingAppBackend.Interface;
using DatingAppBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppBackend.Controllers
{
    public class UserController(DatingAppContext context) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await context.Users.FindAsync(id); // Await the ValueTask properly
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
