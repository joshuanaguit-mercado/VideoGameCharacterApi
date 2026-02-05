using VideoGameCharacterApi.Application.Dtos;

namespace VideoGameCharacterApi.Application.Interfaces
{
    public interface IVideoGameCharacterService
    {
        Task<List<CharacterResponse>> GetAllCharacterAsync(CancellationToken cancellationToken);
        Task<CharacterResponse?> GetCharacterByIdAsync(int id, CancellationToken cancellationToken);
        Task<CharacterResponse> AddCharacterAsync(CreateCharacterRequest character, CancellationToken cancellationToken);
        Task<bool> UpdateCharacterAsync(int id, UpdateCharacterRequest character, CancellationToken cancellationToken);
        Task<bool> DeleteCharacterAsync(int id, CancellationToken cancellationToken);
    }
}