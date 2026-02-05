namespace VideoGameCharacterApi.Domain.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Game { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? SecretMission { get; set; } = string.Empty; // Domain-specific property not exposed in DTOs
        public override string ToString() => $"{Name} from {Game} as {Role}";
    }
}
