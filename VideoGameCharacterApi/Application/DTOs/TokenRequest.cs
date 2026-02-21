using VideoGameCharacterApi.Domain.Constants;

namespace VideoGameCharacterApi.Application.Dtos;

public class TokenRequest
{
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.User; // Default to "User" role for simplicity
}