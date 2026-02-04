using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace VideoGameCharacterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Simple login endpoint for example purposes only. In real apps validate credentials properly.
    [HttpPost("token")]
    public IActionResult GetToken([FromBody] LoginRequest request)
    {
        if (request is null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest();
        }

        // For demonstration accept a single hard-coded user. Replace with real user validation.
        if (request.Username != "test" || request.Password != "password")
        {
            return Unauthorized();
        }

        var jwtSection = _configuration.GetSection("Jwt");
        var issuer = jwtSection.GetValue<string>("Issuer");
        var audience = jwtSection.GetValue<string>("Audience");
        var key = jwtSection.GetValue<string>("Key");
        var expiryMinutes = jwtSection.GetValue<int>("TokenExpiryMinutes");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim("role", "User")
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { access_token = tokenString });
    }

    public record LoginRequest(string Username, string Password);
}
