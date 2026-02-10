using VideoGameCharacterApi.Domain.Constants;

namespace VideoGameCharacterApi.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // store hashed password
    public string Role { get; set; } = Roles.User; // Default to "User" role for simplicity
}
