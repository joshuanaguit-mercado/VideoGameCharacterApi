using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VideoGameCharacterApi.Application.Dtos;
using VideoGameCharacterApi.Application.Interfaces;

namespace VideoGameCharacterApi.Infrastructure.Authentication;

public class JwtAuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly VideoGameCharacterApi.Application.Interfaces.IUserRepository _userRepository;

    public JwtAuthService(IConfiguration configuration, VideoGameCharacterApi.Application.Interfaces.IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public Task<TokenResponse?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        // Use IUserRepository to lookup the user and verify password
        // Passwords must be stored hashed. For demo we assume PasswordHash is a hex-encoded SHA256.
        return AuthenticateInternalAsync(request, cancellationToken);
    }

    private async Task<TokenResponse?> AuthenticateInternalAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (user is null) return null;

        var providedHash = ComputeSha256Hash(request.Password);
        if (!string.Equals(providedHash, user.PasswordHash, StringComparison.Ordinal)) return null;

        var jwtSection = _configuration.GetSection("Jwt");
        var issuer = jwtSection.GetValue<string>("Issuer");
        var audience = jwtSection.GetValue<string>("Audience");
        var key = jwtSection.GetValue<string>("Key");
        var expiryMinutes = jwtSection.GetValue<int>("TokenExpiryMinutes");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("role", user.Role)
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

    private static string ComputeSha256Hash(string raw)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
        return Convert.ToHexString(bytes);
    }
}
