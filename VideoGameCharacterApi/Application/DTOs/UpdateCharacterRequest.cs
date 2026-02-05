namespace VideoGameCharacterApi.Application.Dtos;

public class UpdateCharacterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Game { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}