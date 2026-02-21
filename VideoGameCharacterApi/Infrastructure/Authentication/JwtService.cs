using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VideoGameCharacterApi.Application.Dtos;
using VideoGameCharacterApi.Application.Interfaces;

namespace VideoGameCharacterApi.Infrastructure.Authentication;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TokenResponse?> CreateToken(TokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var issuer = jwtSection.GetValue<string>("Issuer");
        var audience = jwtSection.GetValue<string>("Audience");
        var key = jwtSection.GetValue<string>("Key");
        var expiryMinutes = jwtSection.GetValue<int>("TokenExpiryMinutes");

        // Guard against null or empty key to avoid passing null into Encoding.GetBytes
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new InvalidOperationException("JWT configuration error: 'Jwt:Key' is not configured or is empty. Please set Jwt:Key in configuration.");
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, tokenRequest.Username),
            new Claim("role", tokenRequest.Role)
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

        return new TokenResponse(tokenString, expiryMinutes);
    }
}
