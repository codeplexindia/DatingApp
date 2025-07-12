using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembersAsync()
        {
            var members = await _appDbContext.Users.ToListAsync();

            if (members == null || members.Count == 0)
                return NotFound("No members found.");

            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMemberByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid ID.");

            var member = await _appDbContext.Users.FindAsync(id);

            if (member == null)
                return NotFound($"Member with ID {id} not found.");

            return Ok(member);
        }
    }
}
