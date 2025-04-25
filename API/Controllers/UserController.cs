using Microsoft.AspNetCore.Mvc;

namespace Namespace.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUser")]
    public IActionResult Get()
    {
        return Ok(new { Name = "John Doe", Age = 30 });
    }
}