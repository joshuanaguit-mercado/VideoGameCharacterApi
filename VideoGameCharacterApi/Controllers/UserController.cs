using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Application.Dtos;
using VideoGameCharacterApi.Application.Interfaces;

namespace VideoGameCharacterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Username and password are required." });
        }

        TokenResponse? tokenResponse = await _userService.AuthenticateAsync(request, cancellationToken);
        if (tokenResponse is null)
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        return Ok(tokenResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.RegisterAsync(request, cancellationToken);

        if (!result.Success)
        {
            if (result.Error == "Username already exists.")
                return Conflict(new { message = result.Error });

            return BadRequest(new { message = result.Error });
        }

        return Created("", result.Data);
    }
}
