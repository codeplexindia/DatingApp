using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Namespace.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> Get()
    {
        var users = await context.Users.ToListAsync();
        if (users == null || !users.Any())
        {
            return NotFound("No users found.");
        }
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> Get(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound($"User with ID {id} not found.");
        }
        return Ok(user);
    }
}