using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Application.Dtos;
using VideoGameCharacterApi.Application.Interfaces;

namespace VideoGameCharacterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("token")]
    public async Task<ActionResult> GetToken([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Username and password are required." });
        }

        TokenResponse? tokenResponse = await _authService.AuthenticateAsync(request, cancellationToken);
        if (tokenResponse is null)
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        return Ok(tokenResponse);
    }
}
